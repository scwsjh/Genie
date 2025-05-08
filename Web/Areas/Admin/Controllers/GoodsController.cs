using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GoodsController : AdminController
    {
        private readonly IGenieGoodsService goodsService;

        public GoodsController(IGenieGoodsService _goodsService)
        {
            goodsService = _goodsService;
        }

        public async Task<IActionResult> Index()
        {
            var goodsList = await goodsService.GetGoodsList();
            return View(goodsList);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id != 0)
            {
                var goodsInfo = await goodsService.GetGoodsInfo(id);
                ViewBag.goodsInfo = goodsInfo;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditGoods(int id, string name, int lev, string code, string tips, string local, string content)
        {
            var goodsInfo = await goodsService.GetGoodsInfo(id);
            if (goodsInfo == null)
            {
                genie_goods goods = new genie_goods();
                goods.goodsId = id;
                goods.goodsName = name;
                goods.lev = lev;
                goods.code = code;
                goods.tips = tips;
                goods.local = local;
                goods.content = content;
                if (await goodsService.AddGenieGoods(goods))
                {
                    return MessageHelper.MsgPage(this, "添加成功!", "/Admin/Goods/Index");
                }
                else
                {
                    return MessageHelper.MsgPage(this, "添加失败!", "/Admin/Goods/Index");
                }
            }
            else
            {
                goodsInfo.goodsName = name;
                goodsInfo.lev = lev;
                goodsInfo.code = code;
                goodsInfo.tips = tips;
                goodsInfo.local = local;
                goodsInfo.content = content;
                if (await goodsService.UpdateGenieGoods(goodsInfo))
                {
                    return MessageHelper.MsgPage(this, "修改成功!", "/Admin/Goods/Index");
                }
                else
                {
                    return MessageHelper.MsgPage(this, "修改失败!", "/Admin/Goods/Index");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await goodsService.DeleteGoods(id);
            if (result)
            {
                return MessageHelper.MsgPage(this, "删除成功!", "/Admin/Goods/Index");
            }
            else
            {
                return MessageHelper.MsgPage(this, "添加失败!", "/Admin/Goods/Index");
            }
        }
    }
}