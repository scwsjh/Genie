using Microsoft.AspNetCore.Mvc;
using TencentCloud.Cbs.V20170312.Models;

namespace Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MallController : AdminController
	{
		private readonly IGenieMallService mallService;
		private readonly IGenieGoodsService goodsService;

		public MallController(IGenieMallService _mallService, IGenieGoodsService _goodsService)
		{
			mallService = _mallService;
			goodsService = _goodsService;
		}

		public async Task<IActionResult> Index()
		{
			var mallList = await mallService.GetGameMallData();
			return View(mallList);
		}

		public async Task<IActionResult> Edit(string id)
		{
			if (!string.IsNullOrEmpty(id))
			{
				var mallInfo = await mallService.GetMallInfo(id);
				ViewBag.mallInfo = mallInfo;
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> EditMall(string id, string name, string goodsId, decimal price, string payType, string remark, int sort, string addTime, string endTime)
		{
			var mallInfo = await mallService.GetMallInfo(id);
			if (mallInfo == null)
			{
				genie_mall mall = new genie_mall();
				mall.mallId = StringHelper.NewGuid;
				mall.name = name;
				mall.type = "Goods";
				mall.goodsId = goodsId;
				mall.payType = payType;
				mall.price = price;
				mall.remark = remark;
				mall.sort = sort;
				mall.addTime = Convert.ToDateTime(addTime);
				mall.endTime = Convert.ToDateTime(endTime);
				if (await mallService.AddMall(mall))
				{
					return MessageHelper.MsgPage(this, "添加成功!", "/Admin/Mall/Index");
				}
				else
				{
					return MessageHelper.MsgPage(this, "添加失败!", "/Admin/Mall/Index");
				}
			}
			else
			{
				mallInfo.name = name;
				mallInfo.goodsId = goodsId;
				mallInfo.payType = payType;
				mallInfo.price = price;
				mallInfo.remark = remark;
				mallInfo.sort = sort;
				mallInfo.addTime = Convert.ToDateTime(addTime);
				mallInfo.endTime = Convert.ToDateTime(endTime);
				if (await mallService.UpdateMall(mallInfo))
				{
					return MessageHelper.MsgPage(this, "修改成功!", "/Admin/Mall/Index");
				}
				else
				{
					return MessageHelper.MsgPage(this, "修改失败!", "/Admin/Mall/Index");
				}
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			bool result = await mallService.Delete(id);
			if (result)
			{
				return MessageHelper.MsgPage(this, "删除成功!", "/Admin/Mall/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "添加失败!", "/Admin/Mall/Index");
			}
		}
	}
}