using Microsoft.AspNetCore.Mvc;
using Service.Core;
using Service.Enties;

namespace Web.Controllers
{
    public class MatchController : GameController
    {
        private readonly IGenieMatchService matchService;
        private readonly IGenieUserService userService;
        private readonly IGenieAttrService attrService;
        private readonly IGenieThingService thingService;
        private readonly IGenieDicService dicService;

        public MatchController(IGenieMatchService matchService, IGenieUserService userService, IGenieAttrService attrService, IGenieThingService thingService, IGenieDicService dicService)
        {
            this.matchService = matchService;
            this.userService = userService;
            this.attrService = attrService;
            this.thingService = thingService;
            this.dicService = dicService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "精灵争霸";
            int uid = StateHelper.userNo(this);
            var GenieUser = await userService.GetGenieUserInfo(uid);

            //元气
            var vigorInfo = await userService.GetUserVigor(uid);
            ViewBag.GenieUser = GenieUser;
            ViewBag.VigorInfo = vigorInfo;

            var MyMatch = await matchService.GetUserMatch(uid);

            //或取可挑战的玩家
            var users = await matchService.GetMatchUser(MyMatch.id);
            users = users.OrderBy(it => it.id).ToList();
            ViewBag.MatchUser = users;
            ViewBag.Uid = uid;
            ViewBag.PageToken = await PageHelper.SetPageToken(this);
            return View(MyMatch);
        }

        public async Task<IActionResult> MatchOk(int id)
        {
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid("/Match/Index", ViewBag.MySid));
            }
            if (id < 1)
            {
                return Redirect(UnitTool.UrlToSid("/Match/Index", ViewBag.MySid));
            }
            int timeNo = TimeHelper.GetHourTimeNumber;
            if (timeNo < 80000 || timeNo > 210000)
            {
                return MessageHelper.Msg(this, "提示", "精灵争霸赛每天8:00~21:00开放!", "/Match/Index");
            }
            int uid = StateHelper.userNo(this);
            var MyMatch = await matchService.GetUserMatch(uid);

            if (id >= MyMatch.id)
            {
                return Redirect(UnitTool.UrlToSid("/Match/Index", ViewBag.MySid));
            }
            if (id < (MyMatch.id - 9))
            {
                return MessageHelper.Msg(this, "提示", "只可以挑战高于自身10名的玩家哦!", "/Match/Index");
            }
            var otMatch = await matchService.GetUserMatchByRank((int)id);
            if (otMatch == null)
            {
                return Redirect(UnitTool.UrlToSid("/Match/Index", ViewBag.MySid));
            }
            //判断元气
            var vigorInfo = await userService.GetUserVigor(uid);
            if (vigorInfo.vigor < 1)
            {
                return MessageHelper.Msg(this, "提示", "元气不足,恢复一下吧!", "/Match/Index");
            }
            FightTemp atk = await attrService.GetUserFightTemp(uid);
            if (atk.genie.Count == 0)
            {
                return MessageHelper.Msg(this, "提示", "先进行编队,再来挑战吧!", "/Team/Index");
            }
            if (await userService.UpdateUserVigor(uid, 0, 1))
            {
                FightTemp def = await attrService.GetUserFightTemp((int)otMatch.uid, FightEnum.Camp.Def.ToString());
                var fight = await FightCompute.Compute(atk, def);
                if (fight.isOk)
                {
                    if (fight.winId == uid)
                    {
                        //调整排名
                        await matchService.UpdateMatchRankUser(MyMatch.id, (int)otMatch.uid);
                        await matchService.UpdateMatchRankUser(otMatch.id, uid);

                        //事件
                        string things = string.Format("在[精灵争霸赛中]获胜,名次提升至第{0}名.[{1}]", otMatch.id, GameTool.FightUbb(fight.fightId));
                        await thingService.AddGenieThing(uid, things);
                    }
                    else
                    {
                        string things = string.Format("在[精灵争霸赛中]挑战失败,再接再厉哦.[{1}]", otMatch.id, GameTool.FightUbb(fight.fightId));
                        await thingService.AddGenieThing(uid, things);
                    }
                    return Redirect(UnitTool.UrlToSid(string.Format("/Fight/FightLog?log={0}", fight.fightId), ViewBag.MySid));
                }
                else
                {
                    return MessageHelper.Msg(this, "争霸赛", "挑战失败,请稍后尝试!", "/Match/Index");
                }
            }
            else
            {
                return MessageHelper.Msg(this, "争霸赛", "挑战失败,请稍后尝试!", "/Match/Index");
            }
        }

        public async Task<IActionResult> GetMatchAward()
        {
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid("/Match/Index", ViewBag.MySid));
            }
            int timeNo = TimeHelper.GetHourTimeNumber;
            if (timeNo < 210000 || timeNo > 235959)
            {
                return MessageHelper.Msg(this, "提示", "精灵争霸赛奖励每天21:00~23:59领取哦!", "/Match/Index");
            }
            var dicInfo = await dicService.GetDicInfo(DicEnum.Code.MatchAward.ToString());
            if (dicInfo == null)
            {
                return MessageHelper.Msg(this, "领奖", "暂无可领取的奖励!", "/Match/Index");
            }
            List<RankTemp> award = JsonUtil.DeserializeJsonToList<RankTemp>(dicInfo.sign);
            if (award.Count == 0)
            {
                return MessageHelper.Msg(this, "领奖", "暂无可领取的奖励!", "/Match/Index");
            }
            int uid = StateHelper.userNo(this);
            if (await matchService.CheakIsGetAward(uid))
            {
                return MessageHelper.Msg(this, "领奖", "奖励已经领取过啦!", "/Match/Index");
            }
            var MyMatch = await matchService.GetUserMatch(uid);
            RankTemp item = award.Find(it => it.min <= MyMatch.id && MyMatch.id <= it.max);
            if (item == null)
            {
                return MessageHelper.Msg(this, "领奖", "暂未上榜!", "/Match/Index");
            }
            if (await matchService.UpdateUserAwardLog(uid))
            {
                if (await GameTool.PutPropBatch(uid, 1, item.award, "争霸赛"))
                {
                    string _retips = GameTool.GetPropTips(item.award);
                    string thing = $"领取【精灵争霸赛】第{MyMatch.id}名奖励,获得:{_retips}";
                    await thingService.AddGenieThing(uid, thing);
                    return MessageHelper.Msg(this, "领奖", $"领取成功,获得:{_retips}", "/Match/Index");
                }
                else
                {
                    return MessageHelper.Msg(this, "领奖", "领取失败,请联系管理员!", "/Match/Index");
                }
            }
            else
            {
                return MessageHelper.Msg(this, "领奖", "领取失败,请稍后尝试!", "/Match/Index");
            }
        }
    }
}