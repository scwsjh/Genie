using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class TreasureController : GameController
    {
        private readonly IGenieTreasureService treasureService;
        private readonly IGenieThingService thingService;

        public TreasureController(IGenieTreasureService treasureService, IGenieThingService thingService)
        {
            this.treasureService = treasureService;
            this.thingService = thingService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await treasureService.GetTreasureData();
            return View(data);
        }

        public async Task<IActionResult> Info(int id, int c)
        {
            var exInfo = await treasureService.GetTreasureInfo(id);
            if (exInfo == null)
            {
                return Redirect(UnitTool.UrlToSid("/Treasure/Index", ViewBag.MySid));
            }
            c = c < 1 ? 1 : c;
            int uid = StateHelper.userNo(this);

            var data = JsonUtil.DeserializeJsonToList<TowerNeed>(exInfo.needData);
            var CheakData = await GameTool.CheakNeed(uid, data, ViewBag.MySid, c);
            ViewBag.Tips = CheakData.tips;

            ViewBag.Count = c;
            return View(exInfo);
        }

        [HttpPost]
        public async Task<IActionResult> TreasureOk(int ex, int count)
        {
            int uid = StateHelper.userNo(this);
            string url = string.Format("/Treasure/Info/{0}?c={1}", ex, count);
            count = count < 1 ? 1 : count;
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            var exInfo = await treasureService.GetTreasureInfo(ex);
            if (exInfo == null)
            {
                return Redirect(UnitTool.UrlToSid("/Treasure/Index", ViewBag.MySid));
            }
            if (exInfo.endTime < DateTime.Now || exInfo.addTime > DateTime.Now)
            {
                return Redirect(UnitTool.UrlToSid("/Treasure/Index", ViewBag.MySid));
            }
            //计算鉴定要求
            var data = JsonUtil.DeserializeJsonToList<TowerNeed>(exInfo.needData);
            var CheakData = await GameTool.CheakNeed(uid, data, ViewBag.MySid, count);
            if (CheakData.result == true)
            {
                if (await GameTool.UpdateBagBatch(uid, 0, data, "藏宝阁", false, count))
                {
                    var propPack = JsonUtil.DeserializeJsonToObject<game_pack>(exInfo.pack);
                    List<TowerGet> ResGet = GameTool.CreatePropByPack(uid, GameEnum.RandomBusOpName.pack.ToString(), exInfo.id.ToString(), propPack, count);
                    if (ResGet.Count > 0)
                    {
                        if (await GameTool.PutPropBatch(uid, 1, ResGet, "藏宝阁"))
                        {
                            string _retips = GameTool.GetPropTips(ResGet);
                            //发送广播
                            string bord = string.Format("开启了{0}次【{1}】,获得:{2}", count, exInfo.name, _retips);
                            await thingService.AddGenieThing(uid, bord);
                            return MessageHelper.MsgPage(this, bord, url);
                        }
                        else
                        {
                            return MessageHelper.MsgPage(this, "打开失败,请稍后尝试!", url);
                        }
                    }
                    else
                    {
                        return MessageHelper.MsgPage(this, "空空如也，什么也没有得到!", url);
                    }
                }
                else
                {
                    return MessageHelper.MsgPage(this, "物品扣除失败!", url);
                }
            }
            else
            {
                return MessageHelper.MsgPage(this, "不满足开启要求!", url);
            }
        }
    }
}