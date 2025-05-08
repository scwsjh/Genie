using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class WelfareController : GameController
    {
        private readonly IGenieExchangeService exchangeService;
        private readonly IGenieThingService thingService;

        public WelfareController(IGenieExchangeService exchangeService, IGenieThingService thingService)
        {
            this.exchangeService = exchangeService;
            this.thingService = thingService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await exchangeService.GetExChangeList();
            return View(data);
        }

        public async Task<IActionResult> Info(int id, int c)
        {
            var exInfo = await exchangeService.GetExChangeInfo(id);
            if (exInfo == null)
            {
                return Redirect(UnitTool.UrlToSid("/Welfare/Index", ViewBag.MySid));
            }
            c = c < 1 ? 1 : c;
            int uid = StateHelper.userNo(this);

            var data = JsonUtil.DeserializeJsonToList<TowerNeed>(exInfo.needData);
            var CheakData = await GameTool.CheakNeed(uid, data, ViewBag.MySid, c);
            ViewBag.Tips = CheakData.tips;
            var GetData = JsonUtil.DeserializeJsonToList<TowerGet>(exInfo.getData);
            ViewBag.GetTips = GameTool.GetPropTips(GetData, 1, c, ViewBag.MySid);

            ViewBag.OnCount = await exchangeService.GetUserExChangeCount(uid, id);
            ViewBag.Count = c;
            return View(exInfo);
        }

        [HttpPost]
        public async Task<IActionResult> ExChangeOk(int ex, int count)
        {
            int uid = StateHelper.userNo(this);
            string url = string.Format("/Welfare/Info/{0}?c={1}", ex, count);
            count = count < 1 ? 1 : count;
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            var exInfo = await exchangeService.GetExChangeInfo(ex);
            if (exInfo == null)
            {
                return Redirect(UnitTool.UrlToSid("/Welfare/Index", ViewBag.MySid));
            }
            if (exInfo.endTime < DateTime.Now || exInfo.startTime > DateTime.Now)
            {
                return Redirect(UnitTool.UrlToSid("/Welfare/Index", ViewBag.MySid));
            }
            var myCount = await exchangeService.GetUserExChangeCount(uid, ex) + count;
            if (myCount > exInfo.count)
            {
                return MessageHelper.MsgPage(this, "兑换次数超出上限!", url);
            }
            //计算鉴定要求
            var data = JsonUtil.DeserializeJsonToList<TowerNeed>(exInfo.needData);
            var CheakData = await GameTool.CheakNeed(uid, data, ViewBag.MySid, count);
            if (CheakData.result == true)
            {
                if (await GameTool.UpdateBagBatch(uid, 0, data, "兑换物品", false, count))
                {
                    if (await exchangeService.InsertUserExChangeLog(uid, count, exInfo))
                    {
                        var GetData = JsonUtil.DeserializeJsonToList<TowerGet>(exInfo.getData);
                        if (await GameTool.PutPropBatch(uid, 1, GetData, "兑换物品获得", count))
                        {
                            string goodsStr = GameTool.GetPropTips(GetData, 0, count);
                            string thing = $"在福利社兑换【{exInfo.name}】{count}次,获得:{goodsStr}!";
                            await thingService.AddGenieThing(uid, thing);
                            string _tips = string.Format("*共计兑换{0}次,获得:{1}!", count, goodsStr);
                            return MessageHelper.MsgPage(this, _tips, url);
                        }
                        else
                        {
                            return MessageHelper.MsgPage(this, "兑换失败,请稍后尝试!", url);
                        }
                    }
                    else
                    {
                        return MessageHelper.MsgPage(this, "兑换失败,请稍后尝试!", url);
                    }
                }
                else
                {
                    return MessageHelper.MsgPage(this, "物品扣除失败!", url);
                }
            }
            else
            {
                return MessageHelper.MsgPage(this, "不满足兑换要求!", url);
            }
        }
    }
}