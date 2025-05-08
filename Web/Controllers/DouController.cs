using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class DouController : GameController
    {
        private readonly IGenieDouService douService;
        private readonly IGenieGoodsService goodsService;
        private readonly IGenieDicService dicService;
        private readonly IGenieThingService thingService;
        private readonly IGenieUserService userService;
        private readonly IGenieAttrService attrService;

        public DouController(IGenieDouService douService, IGenieGoodsService goodsService, IGenieDicService dicService, IGenieThingService thingService, IGenieUserService userService, IGenieAttrService attrService)
        {
            this.douService = douService;
            this.goodsService = goodsService;
            this.dicService = dicService;
            this.thingService = thingService;
            this.userService = userService;
            this.attrService = attrService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "乐斗";
            int uid = StateHelper.userNo(this);
            var douInfo = await douService.GetDouInfo(uid);

            List<DouTemp> users = JsonConvert.DeserializeObject<List<DouTemp>>(douInfo.users);

            int goodsCount = await goodsService.GetUserGoodsCount(uid, GameConfig.DouGoods);

            ViewBag.OkCount = users.Count(it => it.status == 1);
            ViewBag.Count = users.Count;
            ViewBag.GoodsCount = goodsCount;
            ViewBag.Users = users;
            ViewBag.PageToken = await PageHelper.SetPageToken(this);

            //乐斗奖励
            var dicInfo = await dicService.GetDicInfo(DicEnum.Code.DouAward.ToString());
            var pack = JsonUtil.DeserializeJsonToObject<game_pack>(dicInfo.sign);

            ViewBag.Award = pack.items;

            return View(douInfo);
        }

        public async Task<IActionResult> Rest()
        {
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid("/Dou/Index", ViewBag.MySid));
            }
            int uid = StateHelper.userNo(this);
            int goodsCount = await goodsService.GetUserGoodsCount(uid, GameConfig.DouGoods);
            if (goodsCount < 1)
            {
                return MessageHelper.Msg(this, "重置乐斗", "暂无该道具!", "/Dou/Index");
            }
            if (await goodsService.UpdateUserGoods(uid, 0, GameConfig.DouGoods, 1, "使用"))
            {
                if (await douService.RestDouInfo(uid))
                {
                    await thingService.AddGenieThing(uid, "刷新了精灵大乐斗,将要大杀四方哦!");

                    return MessageHelper.Msg(this, "重置乐斗", "刷新成功!", "/Dou/Index");
                }
                else
                {
                    return MessageHelper.Msg(this, "重置乐斗", "重置失败,请稍后尝试!", "/Dou/Index");
                }
            }
            else
            {
                return MessageHelper.Msg(this, "重置乐斗", "重置失败,请稍后尝试!", "/Dou/Index");
            }
        }

        public async Task<IActionResult> PkOk(int u)
        {
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid("/Dou/Index", ViewBag.MySid));
            }
            int uid = StateHelper.userNo(this);
            var douInfo = await douService.GetDouInfo(uid);
            List<DouTemp> users = JsonConvert.DeserializeObject<List<DouTemp>>(douInfo.users);
            if (!users.Any(it => it.userId == u))
            {
                return MessageHelper.Msg(this, "大乐斗", "您不可挑战Ta哦!", "/Dou/Index");
            }
            var onUserIndex = users.FindIndex(it => it.userId == u);
            if (users[onUserIndex].status == 1)
            {
                return MessageHelper.Msg(this, "大乐斗", "您已经挑战过她啦!", "/Dou/Index");
            }
            //判断元气
            var vigorInfo = await userService.GetUserVigor(uid);
            if (vigorInfo.vigor < 1)
            {
                return MessageHelper.Msg(this, "大乐斗", "元气不足,恢复一下吧!", "/Dou/Index");
            }
            FightTemp atk = await attrService.GetUserFightTemp(uid);
            if (atk.genie.Count == 0)
            {
                return MessageHelper.Msg(this, "大乐斗", "先进行编队,再来挑战吧!", "/Team/Index");
            }
            if (await userService.UpdateUserVigor(uid, 0, 1))
            {
                //获取奖励
                var dicInfo = await dicService.GetDicInfo(DicEnum.Code.DouAward.ToString());
                var propPack = JsonUtil.DeserializeJsonToObject<game_pack>(dicInfo.sign);
                string opPar = string.Format("Dou_{0}", uid);
                List<TowerGet> ResGet = GameTool.CreatePropByPack(uid, GameEnum.RandomBusOpName.pack.ToString(), opPar, propPack);

                FightTemp def = await attrService.GetUserFightTemp(u, FightEnum.Camp.Def.ToString());
                var fight = await FightCompute.Compute(atk, def, FightEnum.FightType.Person.ToString(), ResGet);
                if (fight.isOk)
                {
                    //更新这个人的状态
                    users[onUserIndex].status = 1;
                    douInfo.users = JsonConvert.SerializeObject(users);
                    if (await douService.UpdateDouInfo(douInfo))
                    {
                        if (fight.winId == uid)
                        {
                            //发放奖励
                            await GameTool.PutPropBatch(uid, 1, ResGet, "乐斗");
                            string _retips = GameTool.GetPropTips(ResGet);
                            //事件
                            string things = string.Format("在[精灵大乐斗中]获胜,获得:{0}.[{1}]", _retips, GameTool.FightUbb(fight.fightId));
                            await thingService.AddGenieThing(uid, things);
                        }
                        else
                        {
                            string things = string.Format("在[精灵大乐斗中]挑战失败,再接再厉哦.[{0}]", GameTool.FightUbb(fight.fightId));
                            await thingService.AddGenieThing(uid, things);
                        }
                        return Redirect(UnitTool.UrlToSid(string.Format("/Fight/FightLog?log={0}", fight.fightId), ViewBag.MySid));
                    }
                    else
                    {
                        return MessageHelper.Msg(this, "大乐斗", "挑战失败,请稍后尝试!", "/Dou/Index");
                    }
                }
                else
                {
                    return MessageHelper.Msg(this, "大乐斗", "挑战失败,请稍后尝试!", "/Dou/Index");
                }
            }
            else
            {
                return MessageHelper.Msg(this, "大乐斗", "挑战失败,请稍后尝试!", "/Dou/Index");
            }
        }
    }
}