using Microsoft.AspNetCore.Mvc;
using Service.Core.Enum;
using Service.Enties;
using SiteBridg.Service;
using TencentCloud.Ame.V20190916.Models;

namespace Web.Controllers
{
    public class ArenaController : GameController
    {
        private readonly IGenieArenaService arenaService;
        private readonly IGenieThingService thingService;
        private readonly IGenieGoodsService goodsService;
        private readonly IGenieAttrService attrService;
        private readonly IWapUserService wapUserService;
        private readonly IGenieChatService chatService;

        public ArenaController(IGenieArenaService arenaService, IGenieThingService thingService, IGenieGoodsService goodsService, IGenieAttrService attrService, IWapUserService wapUserService, IGenieChatService chatService)
        {
            this.arenaService = arenaService;
            this.thingService = thingService;
            this.goodsService = goodsService;
            this.attrService = attrService;
            this.wapUserService = wapUserService;
            this.chatService = chatService;
        }

        public async Task<IActionResult> Index()
        {
            int uid = StateHelper.userNo(this);
            var arenaUser = await arenaService.GetArenaUserInfo(uid);
            var data = await arenaService.GetArenaDataOne();
            var awards = await arenaService.GetAreaAwardData();
            List<TowerGet> award = new List<TowerGet>();
            var awardTemp = awards.Find(it => it.count == data.count);
            if (awardTemp != null)
            {
                var temp = JsonConvert.DeserializeObject<List<TowerGet>>(awardTemp.award);
                award.AddRange(temp);
            }
            ViewBag.Things = await thingService.GetGenieThing(ThingEnum.Type.Arena.ToString(), 5);
            ViewBag.Award = award;
            ViewBag.ArenaUser = arenaUser;
            ViewBag.GoodsCount = await goodsService.GetUserGoodsCount(uid, GameConfig.ArenaGoods);
            return View(data);
        }

        public async Task<IActionResult> ArenaOk()
        {
            string url = "/Arena/Index";
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            int uid = StateHelper.userNo(this);
            var arenaUser = await arenaService.GetArenaUserInfo(uid);
            if (arenaUser.isOn != 0)
            {
                return MessageHelper.Msg(this, "擂台", "您今天已经上过擂台啦!", url);
            }
            var arenaInfo = await arenaService.GetArenaDataOne();
            if (arenaInfo.userId == 0)
            {
                //直接更换
                if (await arenaService.UpdateArenaUser(arenaInfo.id, uid))
                {
                    await thingService.AddGenieThing(uid, "成功占领了擂台!赶快来挑战吧!", ThingEnum.Type.Arena.ToString());

                    return MessageHelper.Msg(this, "擂台", "挑战成功!", url);
                }
                else
                {
                    return MessageHelper.Msg(this, "擂台", "挑战失败,请稍后尝试!", url);
                }
            }
            if (arenaInfo.userId == uid)
            {
                return MessageHelper.Msg(this, "擂台", "您已经在擂台啦!", url);
            }

            bool CanPk = false;
            if (arenaUser.isPk == 0)
            {
                CanPk = true;
                await arenaService.SetArenaUserIsPk(uid);
            }
            if (CanPk == false)
            {
                var MyGoods = await goodsService.GetUserGoodsCount(uid, GameConfig.ArenaGoods);
                if (MyGoods < 1)
                {
                    return MessageHelper.Msg(this, "擂台", "挑战令不足!", url);
                }
                CanPk = await goodsService.UpdateUserGoods(uid, 0, GameConfig.ArenaGoods, 1, "擂台使用");
            }

            if (CanPk)
            {
                FightTemp atk = await attrService.GetUserFightTemp(uid);
                if (atk.genie.Count == 0)
                {
                    return MessageHelper.Msg(this, "提示", "先进行编队,再来挑战吧!", "/Team/Index");
                }
                FightTemp def = await attrService.GetUserFightTemp((int)arenaInfo.userId, FightEnum.Camp.Def.ToString());
                var fight = await FightCompute.Compute(atk, def);
                if (fight.isOk)
                {
                    var atkUser = await wapUserService.GetUserInfo(uid);
                    var defUser = await wapUserService.GetUserInfo((int)arenaInfo.userId);
                    if (fight.winId == uid)
                    {
                        //处理下擂台
                        await arenaService.UpdateArenaUser(arenaInfo.id, uid);
                        string chatMsg = $"您的擂主易位,您已完成了{arenaInfo.count}连胜.";
                        var awards = await arenaService.GetAreaAwardData();
                        List<TowerGet> award = new List<TowerGet>();
                        var awardTemp = awards.Find(it => it.count == arenaInfo.count);
                        if (awardTemp != null)
                        {
                            if (awardTemp.isPut == 1)
                            {
                                var temp = JsonConvert.DeserializeObject<List<TowerGet>>(awardTemp.award);
                                award.AddRange(temp);
                            }
                        }
                        if (award.Count > 0)
                        {
                            chatMsg += "获得:" + GameTool.GetPropTips(award);
                            await GameTool.PutPropBatch((int)arenaInfo.userId, 1, award, "擂台赛");
                        }
                        await chatService.AddChat(0, "", (int)arenaInfo.userId, ChatEnum.code.User.ToString(), chatMsg);

                        //事件
                        string things = string.Format("在擂台赛挑战成功,成为新的擂主,快来挑战Ta吧！[{0}]", GameTool.FightUbb(fight.fightId));
                        await thingService.AddGenieThing(uid, things, ThingEnum.Type.Arena.ToString());
                    }
                    else
                    {
                        //判断是否连胜10次
                        var awards = await arenaService.GetAreaAwardData();
                        var awardTemp = awards.LastOrDefault();
                        if (awardTemp != null)
                        {
                            if (arenaInfo.count + 1 >= awardTemp.count)
                            {
                                //强制下擂台
                                await arenaService.UpdateArenaUser(arenaInfo.id, 0);
                                string chatMsg = $"恭喜,您已完成了{arenaInfo.count + 1}连胜.";
                                List<TowerGet> award = new List<TowerGet>();
                                if (awardTemp != null)
                                {
                                    var temp = JsonConvert.DeserializeObject<List<TowerGet>>(awardTemp.award);
                                    award.AddRange(temp);
                                }
                                if (award.Count > 0)
                                {
                                    chatMsg += "获得:" + GameTool.GetPropTips(award);
                                    await GameTool.PutPropBatch((int)arenaInfo.userId, 1, award, "擂台赛");
                                }
                                await chatService.AddChat(0, "", (int)arenaInfo.userId, ChatEnum.code.User.ToString(), chatMsg);
                            }
                            else
                            {
                                //更新连胜次数
                                await arenaService.UpdateArenaCount(arenaInfo.id, 1);
                            }
                        }
                        else
                        {
                            //更新连胜次数
                            await arenaService.UpdateArenaCount(arenaInfo.id, 1);
                        }

                        string things = string.Format("已成功守擂{0}连胜了,太厉害啦![{1}]", arenaInfo.count + 1, GameTool.FightUbb(fight.fightId));
                        await thingService.AddGenieThing((int)arenaInfo.userId, things, ThingEnum.Type.Arena.ToString());
                    }
                    return Redirect(UnitTool.UrlToSid(string.Format("/Fight/FightLog?log={0}", fight.fightId), ViewBag.MySid));
                }
                else
                {
                    return MessageHelper.Msg(this, "擂台", "挑战失败,请稍后尝试!", url);
                }
            }
            else
            {
                return MessageHelper.Msg(this, "擂台", "挑战异常,请稍后尝试!", url);
            }
        }
    }
}