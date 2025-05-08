using Microsoft.AspNetCore.Mvc;
using Service.Core;

namespace Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class TrialController : AdminController
	{
		private readonly IGenieTrialService trialService;

		public TrialController(IGenieTrialService trialService)
		{
			this.trialService = trialService;
		}

		public async Task<IActionResult> Index()
		{
			var data = await trialService.GetTrialDataAll();
			return View(data);
		}

		public async Task<IActionResult> Edit(int id)
		{
			genie_trial data = new genie_trial();
			if (id != 0)
			{
				data = await trialService.GetTrialInfo(id);
			}
			return View(data);
		}

		[HttpPost]
		public async Task<IActionResult> EditOk([FromForm] int id, [FromForm] string name, [FromForm] int lev, [FromForm] string team, [FromForm] int exp, [FromForm] int vigor, [FromForm] string award, [FromForm] int status)
		{
			genie_trial add = new genie_trial();
			add.id = id;
			add.name = name;
			add.lev = lev;
			add.team = team;
			add.exp = exp;
			add.award = award;
			add.status = status;
			add.vigor = vigor;
			if (await trialService.SaveTrial(add))
			{
				return MessageHelper.MsgPage(this, "操作成功!", "/Admin/Trial/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "操作失败!", "/Admin/Trial/Index");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			bool result = await trialService.DeleteTrial(id);
			if (result)
			{
				return MessageHelper.MsgPage(this, "删除成功!", "/Admin/Trial/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "删除失败!", "/Admin/Trial/Index");
			}
		}
	}
}