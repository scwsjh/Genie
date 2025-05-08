using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MonsterController : AdminController
	{
		private readonly IGenieService genieService;

		public MonsterController(IGenieService genieService)
		{
			this.genieService = genieService;
		}

		public async Task<IActionResult> Index()
		{
			var data = await genieService.GetMonsterData();
			return View(data);
		}

		public async Task<IActionResult> Edit(string id)
		{
			genie_monster data = new genie_monster();
			if (!string.IsNullOrEmpty(id))
			{
				data = await genieService.GetMonsterInfo(id);
			}
			return View(data);
		}

		[HttpPost]
		public async Task<IActionResult> EditOk([FromForm] string id, [FromForm] string name, [FromForm] int lev, [FromForm] int start, [FromForm] int charm, [FromForm] int blood, [FromForm] string skill, [FromForm] string remark)
		{
			genie_monster add = new genie_monster();
			add.id = id;
			add.name = name;
			add.lev = lev;
			add.start = start;
			add.charm = charm;
			add.blood = blood;
			add.skill = skill;
			add.remark = remark;
			if (await genieService.SaveMonster(add))
			{
				return MessageHelper.MsgPage(this, "操作成功!", "/Admin/Monster/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "操作失败!", "/Admin/Monster/Index");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			bool result = await genieService.DeleteMonster(id);
			if (result)
			{
				return MessageHelper.MsgPage(this, "删除成功!", "/Admin/Monster/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "删除失败!", "/Admin/Monster/Index");
			}
		}
	}
}