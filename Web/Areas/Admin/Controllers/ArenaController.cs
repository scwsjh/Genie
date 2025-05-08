using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ArenaController : AdminController
	{
		private readonly IGenieArenaService arenaService;

		public ArenaController(IGenieArenaService arenaService)
		{
			this.arenaService = arenaService;
		}

		public async Task<IActionResult> Index()
		{
			var awardData = await arenaService.GetAreaAwardData();
			return View(awardData);
		}

		public async Task<IActionResult> Edit(string id)
		{
			genie_arena_award award = new genie_arena_award();
			if (!string.IsNullOrEmpty(id))
			{
				award = await arenaService.GetAreaAwardInfo(id);
			}
			return View(award);
		}

		[HttpPost]
		public async Task<IActionResult> EditOk([FromForm] string id, [FromForm] int count, [FromForm] string award, [FromForm] int isPut)
		{
			if (string.IsNullOrEmpty(id))
			{
				return MessageHelper.MsgPage(this, "ID不能为空!", "/Admin/Arena/Edit?id=" + id);
			}
			genie_arena_award add = new genie_arena_award();
			add.id = id;
			add.count = count;
			add.award = award;
			add.isPut = isPut;

			if (await arenaService.SaveAreaAward(add))
			{
				return MessageHelper.MsgPage(this, "操作成功!", "/Admin/Arena/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "操作失败!", "/Admin/Arena/Index");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			bool result = await arenaService.DelAreaAward(id);
			if (result)
			{
				return MessageHelper.MsgPage(this, "删除成功!", "/Admin/Arena/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "添加失败!", "/Admin/Arena/Index");
			}
		}
	}
}