using Microsoft.AspNetCore.Mvc;
using Service.Core;
using System.Diagnostics.Metrics;

namespace Web.Controllers
{
    public class GoodsController : GameController
    {
        private readonly IGenieGoodsService goodsService;
        private readonly IGenieUserService userService;
        private readonly IGenieThingService thingService;

        public GoodsController(IGenieGoodsService goodsService, IGenieUserService userService, IGenieThingService thingService)
        {
            this.goodsService = goodsService;
            this.userService = userService;
            this.thingService = thingService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "我的储物箱";
            int PageIndex = StringHelper.ConvertPage(Request.Query["p"]);
            int PageSize = 10;
            RefAsync<int> Total = 0;

            int uid = StateHelper.userNo(this);
            var data = await goodsService.GetUserGoodsData(uid, PageIndex, PageSize, Total);

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.Total = Total;
            return View(data);
        }

        public async Task<IActionResult> Info(int id)
        {
            int uid = StateHelper.userNo(this);
            var GoodsInfo = await goodsService.GetGoodsInfo(id);
            if (GoodsInfo == null)
            {
                return Redirect(UnitTool.UrlToSid("/Goods/Index", ViewBag.MySid));
            }
            var userGoods = await goodsService.GetUserGoodsInfo(uid, id);
            int count = 0;
            if (userGoods != null)
            {
                count = (int)userGoods.count;
            }
            ViewBag.MyCount = count;
            ViewBag.PageToken = await PageHelper.SetPageToken(this);
            return View(GoodsInfo);
        }

        [HttpPost]
        public async Task<IActionResult> UseVigor(int g, int count)
        {
            string url = String.Format("/Goods/Info/{0}", g);
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            int uid = StateHelper.userNo(this);
            if (count < 1)
            {
                return MessageHelper.Msg(this, "使用道具", "使用数量不能小于0!", url);
            }
            var myCount = await goodsService.GetUserGoodsCount(uid, g);
            if (myCount < count)
            {
                return MessageHelper.Msg(this, "使用道具", "物品数量不足!", url);
            }
            var goods = await goodsService.GetGoodsInfo(g);
            if (goods.code != GoodsEnum.Code.Vigor.ToString())
            {
                return MessageHelper.Msg(this, "使用道具", "该物品无法使用!", url);
            }
            if (await goodsService.UpdateUserGoods(uid, 0, g, count, "使用物品"))
            {
                int addW = Convert.ToInt32(goods.content) * count;
                if (await userService.UpdateUserVigor(uid, 1, addW))
                {
                    return MessageHelper.Msg(this, "使用道具", string.Format("*成功使用{0}个;元气+{1}!", count, addW), url);
                }
                else
                {
                    return MessageHelper.Msg(this, "使用道具", "使用失败,请稍后尝试!", url);
                }
            }
            else
            {
                return MessageHelper.Msg(this, "使用道具", "使用失败,请稍后尝试!", url);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UsePack(int g, int count)
        {
            string url = String.Format("/Goods/Info/{0}", g);
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            int uid = StateHelper.userNo(this);
            if (count < 1)
            {
                return MessageHelper.Msg(this, "礼包", "打开数量不能小于0!", url);
            }
            var myCount = await goodsService.GetUserGoodsCount(uid, g);
            if (myCount < count)
            {
                return MessageHelper.Msg(this, "礼包", "物品数量不足!", url);
            }
            var goods = await goodsService.GetGoodsInfo(g);
            if (goods.code != GoodsEnum.Code.Pack.ToString())
            {
                return MessageHelper.Msg(this, "礼包", "该物品无法打开!", url);
            }
            var genieUser = await userService.GetGenieUserInfo(uid);
            int myLev = (int)genieUser.lev;
            if (myLev < goods.lev)
            {
                return MessageHelper.Msg(this, "礼包", String.Format("*该物品需要达到{0}级才可以打开哦!", goods.lev), url);
            }

            if (await goodsService.UpdateUserGoods(uid, 0, g, count, "打开物品/礼包"))
            {
                string resTips = string.Format("共打开{0}个【{1}】,获得:<br />", count, goods.goodsName);
                var propPack = JsonUtil.DeserializeJsonToObject<game_pack>(goods.content);
                List<TowerGet> ResGet = GameTool.CreatePropByPack(uid, GameEnum.RandomBusOpName.pack.ToString(), goods.goodsId.ToString(), propPack, count);
                if (ResGet.Count > 0)
                {
                    if (await GameTool.PutPropBatch(uid, 1, ResGet, "打开物品/礼包获得"))
                    {
                        string _retips = GameTool.GetPropTips(ResGet);
                        //发送广播
                        string bord = string.Format("打开了{0}个【{1}】,获得:{2}", count, goods.goodsName, _retips);
                        await thingService.AddGenieThing(uid, bord);

                        return MessageHelper.Msg(this, "礼包", resTips + _retips, url);
                    }
                    else
                    {
                        return MessageHelper.Msg(this, "礼包", "打开失败,请稍后尝试!", url);
                    }
                }
                else
                {
                    return MessageHelper.Msg(this, "礼包", "空空如也，什么也没有得到!", url);
                }
            }
            else
            {
                return MessageHelper.Msg(this, "礼包", "打开失败,请稍后尝试!", url);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UseState(int g, int count)
        {
            string url = String.Format("/Goods/Info/{0}", g);
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            int uid = StateHelper.userNo(this);
            if (count < 1)
            {
                return MessageHelper.Msg(this, "使用道具", "使用数量不能小于0!", url);
            }
            var myCount = await goodsService.GetUserGoodsCount(uid, g);
            if (myCount < count)
            {
                return MessageHelper.Msg(this, "使用道具", "物品数量不足!", url);
            }
            var goods = await goodsService.GetGoodsInfo(g);
            if (goods.code != GoodsEnum.Code.State.ToString())
            {
                return MessageHelper.Msg(this, "使用道具", "该物品无法使用!", url);
            }
            if (await goodsService.UpdateUserGoods(uid, 0, g, count, "使用物品"))
            {
                var goodsBuff = JsonUtil.DeserializeJsonToObject<GoodsBuff>(goods.content);
                if (await userService.AddUserState(uid, goodsBuff, count))
                {
                    return MessageHelper.Msg(this, "使用道具", string.Format("*成功使用{0}个{1}!", count, goods.goodsName), url);
                }
                else
                {
                    return MessageHelper.Msg(this, "使用道具", "使用失败,请稍后尝试!", url);
                }
            }
            else
            {
                return MessageHelper.Msg(this, "使用道具", "使用失败,请稍后尝试!", url);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UseExp(int g, int count)
        {
            string url = String.Format("/Goods/Info/{0}", g);
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            int uid = StateHelper.userNo(this);
            if (count < 1)
            {
                return MessageHelper.Msg(this, "使用道具", "使用数量不能小于0!", url);
            }
            var myCount = await goodsService.GetUserGoodsCount(uid, g);
            if (myCount < count)
            {
                return MessageHelper.Msg(this, "使用道具", "物品数量不足!", url);
            }
            var goods = await goodsService.GetGoodsInfo(g);
            if (goods.code != GoodsEnum.Code.Exp.ToString())
            {
                return MessageHelper.Msg(this, "使用道具", "该物品无法使用!", url);
            }
            if (await goodsService.UpdateUserGoods(uid, 0, g, count, "使用物品"))
            {
                int addW = Convert.ToInt32(goods.content) * count;
                if (await userService.UpdateUserExp(uid, addW))
                {
                    return MessageHelper.Msg(this, "使用道具", string.Format("*成功使用{0}个;经验+{1}!", count, addW), url);
                }
                else
                {
                    return MessageHelper.Msg(this, "使用道具", "使用失败,请稍后尝试!", url);
                }
            }
            else
            {
                return MessageHelper.Msg(this, "使用道具", "使用失败,请稍后尝试!", url);
            }
        }
    }
}