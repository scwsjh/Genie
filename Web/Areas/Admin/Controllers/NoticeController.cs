using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class NoticeController : AdminController
	{
		private readonly IGenieNoticeService noticeService;

		public NoticeController(IGenieNoticeService noticeService)
		{
			this.noticeService = noticeService;
		}

		public async Task<IActionResult> Index()
		{
			var noticeData = await noticeService.GetGenieNoticeData();
			return View(noticeData);
		}

		public async Task<IActionResult> Edit(string id)
		{
			genie_notice notice = new genie_notice();
			if (!string.IsNullOrEmpty(id))
			{
				notice = await noticeService.GetNoticeInfo(id);
			}
			else
			{
				notice.addTime = TimeHelper.GetDateTimeYMD(0);
				notice.endTime = notice.addTime;
			}
			return View(notice);
		}

		[HttpPost]
		public async Task<IActionResult> EditOk([FromForm] genie_notice notice)
		{
			if (string.IsNullOrEmpty(notice.noticeId))
			{
				notice.noticeId = StringHelper.NewGuid;
			}

			if (await noticeService.SaveNotice(notice))
			{
				return MessageHelper.MsgPage(this, "操作成功!", "/Admin/Notice/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "操作失败!", "/Admin/Notice/Index");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			bool result = await noticeService.DelNotice(id);
			if (result)
			{
				return MessageHelper.MsgPage(this, "删除成功!", "/Admin/Notice/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "添加失败!", "/Admin/Notice/Index");
			}
		}
	}
}