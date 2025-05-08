using Microsoft.AspNetCore.Mvc;
using Service.Core;

namespace Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class TreasureController : AdminController
	{
		private readonly IGenieTreasureService treasureService;

		public TreasureController(IGenieTreasureService treasureService)
		{
			this.treasureService = treasureService;
		}

		public async Task<IActionResult> Index()
		{
			var data = await treasureService.GetTreasureDataAll();
			return View(data);
		}

		public async Task<IActionResult> Edit(int id)
		{
			genie_treasure data = await treasureService.GetTreasureInfo(id);
			if (data == null)
			{
				data = new genie_treasure();
				data.addTime = DateTime.Now;
				data.endTime = DateTime.Now;
			}
			return View(data);
		}

		[HttpPost]
		public async Task<IActionResult> EditOk([FromForm] int id, [FromForm] string name, [FromForm] string tips, [FromForm] string needData, [FromForm] string pack, [FromForm] string addTime, [FromForm] string endTime)
		{
			genie_treasure add = new genie_treasure();
			add.id = id;
			add.name = name;
			add.needData = needData;
			add.pack = pack;
			add.addTime = Convert.ToDateTime(addTime);
			add.endTime = Convert.ToDateTime(endTime);
			add.tips = tips;

			if (await treasureService.SaveTreasure(add))
			{
				return MessageHelper.MsgPage(this, "操作成功!", "/Admin/Treasure/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "操作失败!", "/Admin/Treasure/Index");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			bool result = await treasureService.DeleteTreasure(id);
			if (result)
			{
				return MessageHelper.MsgPage(this, "删除成功!", "/Admin/Treasure/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "删除失败!", "/Admin/Treasure/Index");
			}
		}
	}
}