using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DicController : AdminController
	{
		private readonly IGenieDicService dicService;

		public DicController(IGenieDicService dicService)
		{
			this.dicService = dicService;
		}

		public async Task<IActionResult> Index()
		{
			var awardData = await dicService.GetDicData();
			return View(awardData);
		}

		public async Task<IActionResult> Edit(string id)
		{
			var data = await dicService.GetDicInfo(id);
			if (data == null)
			{
				return MessageHelper.MsgPage(this, "配置不存在!", "/Admin/Dic/Index");
			}
			return View(data);
		}

		[HttpPost]
		public async Task<IActionResult> EditOk([FromForm] string code, [FromForm] string sign, [FromForm] string remark)
		{
			genie_dic add = new genie_dic();
			add.code = code;
			add.sign = sign;
			add.remark = remark;
			if (await dicService.UpdateDic(add))
			{
				return MessageHelper.MsgPage(this, "操作成功!", "/Admin/Dic/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "操作失败!", "/Admin/Dic/Index");
			}
		}
	}
}