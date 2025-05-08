using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ExChangeController : AdminController
	{
		private readonly IGenieExchangeService exchangeService;

		public ExChangeController(IGenieExchangeService exchangeService)
		{
			this.exchangeService = exchangeService;
		}

		public async Task<IActionResult> Index()
		{
			var data = await exchangeService.GetExChangeData();
			return View(data);
		}

		public async Task<IActionResult> Edit(int id)
		{
			genie_exchange data = await exchangeService.GetExChangeInfo(id);
			if (data == null)
			{
				data = new genie_exchange();
				data.startTime = DateTime.Now;
				data.endTime = DateTime.Now;
			}
			return View(data);
		}

		[HttpPost]
		public async Task<IActionResult> EditOk([FromForm] int exId, [FromForm] string name, [FromForm] int count, [FromForm] string tips, [FromForm] string needData, [FromForm] string getData, [FromForm] string startTime, [FromForm] string endTime)
		{
			genie_exchange add = new genie_exchange();
			add.exId = exId;
			add.name = name;
			add.count = count;
			add.needData = needData;
			add.getData = getData;
			add.startTime = Convert.ToDateTime(startTime);
			add.endTime = Convert.ToDateTime(endTime);
			add.tips = tips;

			if (await exchangeService.SaveExChange(add))
			{
				return MessageHelper.MsgPage(this, "操作成功!", "/Admin/ExChange/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "操作失败!", "/Admin/ExChange/Index");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			bool result = await exchangeService.DelExChange(id);
			if (result)
			{
				return MessageHelper.MsgPage(this, "删除成功!", "/Admin/ExChange/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "删除失败!", "/Admin/ExChange/Index");
			}
		}
	}
}