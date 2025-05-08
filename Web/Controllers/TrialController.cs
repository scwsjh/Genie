using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.WireProtocol.Messages.Encoders.BinaryEncoders;

namespace Web.Controllers
{
    public class TrialController : GameController
    {
        private readonly IGenieService genieService;
        private readonly IGenieTrialService trialService;
        private readonly IGenieUserService userService;
        private readonly IGenieAttrService attrService;
        private readonly IGenieThingService thingService;

        public TrialController(IGenieService genieService, IGenieTrialService trialService, IGenieUserService userService, IGenieAttrService attrService, IGenieThingService thingService)
        {
            this.genieService = genieService;
            this.trialService = trialService;
            this.userService = userService;
            this.attrService = attrService;
            this.thingService = thingService;
        }

        public async Task<IActionResult> Index()
        {
            int uid = StateHelper.userNo(this);
            var genieUserInfo = await userService.GetGenieUserInfo(uid);
            var data = await trialService.GetTrialData();

            ViewBag.UserInfo = genieUserInfo;
            return View(data);
        }

        public async Task<IActionResult> Info(int id)
        {
            int uid = StateHelper.userNo(this);
            var trialInfo = await trialService.GetTrialInfo(id);
            if (trialInfo == null)
            {
                return Redirect(UnitTool.UrlToSid("/Trial/Index", ViewBag.MySid));
            }
            ViewBag.MonsAttr = await trialService.GetTeamGenieAttr(trialInfo.team);
            ViewBag.Pack = JsonConvert.DeserializeObject<game_pack>(trialInfo.award);
            ViewBag.MyVigor = await userService.GetUserVigor(uid);
            ViewBag.MyInfo = await userService.GetGenieUserInfo(uid);
            return View(trialInfo);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var data = await genieService.GetMonsterInfo(id);
            if (data == null)
            {
                return Redirect(UnitTool.UrlToSid("/Trial/Index", ViewBag.MySid));
            }
            return View(data);
        }

        public async Task<IActionResult> TrialOk(int id)
        {
            string url = $"/Trial/Info/{id}";
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            var trialInfo = await trialService.GetTrialInfo(id);
            if (trialInfo == null)
            {
                return Redirect(UnitTool.UrlToSid("/Trial/Index", ViewBag.MySid));
            }
            if (trialInfo.status != 1)
            {
                return MessageHelper.MsgPage(this, "试炼已关闭!", url);
            }
            int uid = StateHelper.userNo(this);
            var myInfo = await userService.GetGenieUserInfo(uid);
            if (myInfo.lev < trialInfo.lev)
            {
                return MessageHelper.MsgPage(this, "等级不足,无法挑战!", url);
            }
            if (myInfo.poin < (GameConfig.MaxPoin * 0.6M))
            {
                return MessageHelper.MsgPage(this, "精灵坞温馨值低于60,您得精灵无心战斗呀!", url);
            }

            var myVigor = await userService.GetUserVigor(uid);
            if (myVigor.vigor < trialInfo.vigor)
            {
                return MessageHelper.MsgPage(this, "元气不足,无法挑战!", url);
            }
            FightTemp atk = await attrService.GetUserFightTemp(uid);
            if (atk.genie.Count == 0)
            {
                return MessageHelper.Msg(this, "大乐斗", "先进行编队,再来挑战吧!", "/Team/Index");
            }
            if (await userService.UpdateUserVigor(uid, 0, (int)trialInfo.vigor))
            {
                //获取奖励
                var propPack = JsonUtil.DeserializeJsonToObject<game_pack>(trialInfo.award);
                string opPar = string.Format("Trial_{0}", id);
                List<TowerGet> ResGet = GameTool.CreatePropByPack(uid, GameEnum.RandomBusOpName.pack.ToString(), opPar, propPack);
                //计算经验
                decimal expAdd = 1 + await userService.GetUserBuff(uid, BuffEnum.BuffName.Exp.ToString());
                decimal GetExp = (decimal)trialInfo.exp * expAdd;
                TowerGet exp = new TowerGet();
                exp.code = GameEnum.PropCode.Exp.ToString();
                exp.name = "经验";
                exp.parameter = "";
                exp.count = Convert.ToInt64(GetExp);
                ResGet.Add(exp);

                FightTemp def = await trialService.GetMonsterFightTemp(id, trialInfo.team, trialInfo.name);
                var fight = await FightCompute.Compute(atk, def, FightEnum.FightType.Monster.ToString(), ResGet);
                if (fight.isOk)
                {
                    //扣除经验BUFF次数
                    await userService.UpdateUserBuffCount(uid, BuffEnum.BuffName.Exp.ToString(), 1);
                    if (fight.winId == uid)
                    {
                        //发放奖励
                        await GameTool.PutPropBatch(uid, 1, ResGet, "试炼");
                        string _retips = GameTool.GetPropTips(ResGet);
                        //事件
                        string things = string.Format("在[{2}]试炼中挑战成功,获得:{0}.[{1}]", _retips, GameTool.FightUbb(fight.fightId), trialInfo.name);
                        await thingService.AddGenieThing(uid, things);
                    }
                    else
                    {
                        string things = string.Format("在[{1}]试炼中挑战失败,再接再厉哦.[{0}]", GameTool.FightUbb(fight.fightId), trialInfo.name);
                        await thingService.AddGenieThing(uid, things);
                    }
                    return Redirect(UnitTool.UrlToSid(string.Format("/Fight/FightLog?log={0}", fight.fightId), ViewBag.MySid));
                }
                else
                {
                    return MessageHelper.Msg(this, "试炼", "挑战失败,请稍后尝试!", url);
                }
            }
            else
            {
                return MessageHelper.MsgPage(this, "挑战失败,请稍后尝试!", url);
            }
        }
    }
}