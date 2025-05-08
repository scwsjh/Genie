using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class GenieController : AdminController
	{
		private readonly IGenieService genieService;

		public GenieController(IGenieService genieService)
		{
			this.genieService = genieService;
		}

		public async Task<IActionResult> Index()
		{
			var awardData = await genieService.GetGenieData();
			return View(awardData);
		}

		public async Task<IActionResult> Edit(int id)
		{
			genie_genie award = new genie_genie();
			if (id != 0)
			{
				award = await genieService.GetGenieInfo(id);
			}
			return View(award);
		}

		[HttpPost]
		public async Task<IActionResult> EditOk([FromForm] int genieId, [FromForm] string name, [FromForm] int type, [FromForm] string img, [FromForm] int charm, [FromForm] int blood, [FromForm] string remark, [FromForm] string skill, [FromForm] string upNeed, [FromForm] string mapAward)
		{
			genie_genie add = new genie_genie();
			add.genieId = genieId;
			add.name = name;
			add.type = type;
			add.img = img;
			add.charm = charm;
			add.blood = blood;
			add.skill = skill;
			add.remark = remark;
			add.upNeed = upNeed;
			add.mapAward = mapAward;
			if (await genieService.SaveGenie(add))
			{
				return MessageHelper.MsgPage(this, "操作成功!", "/Admin/Genie/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "操作失败!", "/Admin/Genie/Index");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			bool result = await genieService.DeleteGenie(id);
			if (result)
			{
				return MessageHelper.MsgPage(this, "删除成功!", "/Admin/Genie/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "删除失败!", "/Admin/Genie/Index");
			}
		}
	}
}