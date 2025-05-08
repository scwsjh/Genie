using Microsoft.AspNetCore.Mvc;
using Service.Core;

namespace Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CallController : AdminController
	{
		private readonly IGenieCallService callService;

		public CallController(IGenieCallService callService)
		{
			this.callService = callService;
		}

		public async Task<IActionResult> Index()
		{
			var awardData = await callService.GetCallData();
			return View(awardData);
		}

		public async Task<IActionResult> Edit(int id)
		{
			genie_call award = new genie_call();
			if (id != 0)
			{
				award = await callService.GetCallInfo(id);
			}
			return View(award);
		}

		[HttpPost]
		public async Task<IActionResult> EditOk([FromForm] int id, [FromForm] string name, [FromForm] string img, [FromForm] int lev, [FromForm] int cool, [FromForm] string needData, [FromForm] string getData)
		{
			genie_call add = new genie_call();
			add.id = id;
			add.name = name;
			add.img = img;
			add.lev = lev;
			add.cool = cool;
			add.needData = needData;
			add.getData = getData;
			if (await callService.SaveCall(add))
			{
				return MessageHelper.MsgPage(this, "操作成功!", "/Admin/Call/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "操作失败!", "/Admin/Call/Index");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			bool result = await callService.DelCall(id);
			if (result)
			{
				return MessageHelper.MsgPage(this, "删除成功!", "/Admin/Call/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "删除失败!", "/Admin/Call/Index");
			}
		}
	}
}