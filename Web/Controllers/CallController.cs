using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CallController : GameController
    {
        private readonly IGenieCallService callService;
        private readonly IGenieUserService userService;
        private readonly IGenieThingService thingService;
        private readonly IGenieService genieService;

        public CallController(IGenieCallService callService, IGenieUserService userService, IGenieThingService thingService, IGenieService genieService)
        {
            this.callService = callService;
            this.userService = userService;
            this.thingService = thingService;
            this.genieService = genieService;
        }

        public async Task<IActionResult> Index()
        {
            int uid = StateHelper.userNo(this);
            var genieUserInfo = await userService.GetGenieUserInfo(uid);
            var callData = await callService.GetCallData();
            List<CallTemp> CallRes = new List<CallTemp>();
            foreach (var item in callData)
            {
                CallTemp temp = new CallTemp();
                temp.call = item;
                temp.time = await callService.GetUserCallCoolTime(uid, item.id);
                CallRes.Add(temp);
            }
            ViewBag.UserInfo = genieUserInfo;
            return View(CallRes);
        }

        public async Task<IActionResult> Info(int id)
        {
            int uid = StateHelper.userNo(this);
            var genieUser = await userService.GetGenieUserInfo(uid);

            var data = await callService.GetCallInfo(id);
            if (data == null)
            {
                return Redirect(UnitTool.UrlToSid("/Index/Index", ViewBag.MySid));
            }

            ViewBag.CoolTime = Convert.ToInt32(await callService.GetUserCallCoolTime(uid, id));
            ViewBag.Pack = JsonConvert.DeserializeObject<game_pack>(data.getData);

            var NeedData = JsonConvert.DeserializeObject<List<TowerNeed>>(data.needData);
            var CheakData = await GameTool.CheakNeed(uid, NeedData, ViewBag.MySid);
            ViewBag.CheakData = CheakData;
            ViewBag.GenieUser = genieUser;
            ViewBag.Title = string.Format("秘境召唤-{0}", data.name);

            return View(data);
        }

        public async Task<IActionResult> CallOk(int id)
        {
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid($"/Call/Info/{id}", ViewBag.MySid));
            }
            int uid = StateHelper.userNo(this);
            var genieUser = await userService.GetGenieUserInfo(uid);
            if (genieUser.poin < GameConfig.MaxPoin)
            {
                return MessageHelper.Msg(this, "提示", "精灵坞正在恢复中无法召唤!", $"/Call/Info/{id}");
            }
            var data = await callService.GetCallInfo(id);
            if (data == null)
            {
                return Redirect(UnitTool.UrlToSid("/Index/Index", ViewBag.MySid));
            }
            if (genieUser.lev < data.lev)
            {
                return MessageHelper.Msg(this, "提示", "不满足秘境等级要求哦!", $"/Call/Info/{id}");
            }
            int maxCount = GameTool.GetMaxGenieCount((int)genieUser.lev);
            var onCount = await genieService.GetUserGenieCount(uid);
            if (onCount >= maxCount)
            {
                return MessageHelper.Msg(this, "提示", "当前拥有的精灵已达到上限,赶紧提升精灵坞等级吧!", $"/Call/Info/{id}");
            }
            int coolTime = Convert.ToInt32(await callService.GetUserCallCoolTime(uid, id));
            if (coolTime >= 1)
            {
                return MessageHelper.Msg(this, "提示", "秘境冷却中!", $"/Call/Info/{id}");
            }
            var NeedData = JsonConvert.DeserializeObject<List<TowerNeed>>(data.needData);
            if (NeedData.Count == 0)
            {
                return MessageHelper.Msg(this, "提示", "不满足召唤条件!", $"/Call/Info/{id}");
            }
            var CheakData = await GameTool.CheakNeed(uid, NeedData, ViewBag.MySid);
            if (CheakData.result)
            {
                var propPack = JsonConvert.DeserializeObject<game_pack>(data.getData);
                List<TowerGet> ResGet = GameTool.CreatePropByPack(uid, "Call", id.ToString(), propPack, 1);
                if (ResGet.Count > 0)
                {
                    if (await GameTool.UpdateBagBatch(uid, 0, NeedData, "召唤"))
                    {
                        if (await GameTool.PutPropBatch(uid, 1, ResGet, "秘境召唤"))
                        {
                            //添加冷却
                            await callService.UpdateUserCallTime(uid, id, (int)data.cool);
                            string retTip = GameTool.GetPropTips(ResGet);
                            string _retips = string.Format("成功召唤:{0}", retTip);

                            //添加时间
                            string thing = string.Format("在【秘境·{0}】获得:{1}!", data.name, retTip);
                            await thingService.AddGenieThing(uid, thing);
                            return MessageHelper.MsgPage(this, _retips, string.Format("/Call/Info/{0}", id));
                        }
                        else
                        {
                            return MessageHelper.MsgPage(this, "召唤失败,请稍后尝试!", string.Format("/Call/Info/{0}", id));
                        }
                    }
                    else
                    {
                        return MessageHelper.MsgPage(this, "物品扣除失败,请稍后尝试!", string.Format("/Call/Info/{0}", id));
                    }
                }
                else
                {
                    return MessageHelper.MsgPage(this, "未召唤到任何东西,继续加油哦!", string.Format("/Call/Info/{0}", id));
                }
            }
            else
            {
                return MessageHelper.MsgPage(this, "不满足召唤条件!", string.Format("/Call/Info/{0}", id));
            }
        }
    }
}