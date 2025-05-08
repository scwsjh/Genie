using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class NoticeController : GameController
    {
        private readonly IGenieNoticeService noticeService;

        public NoticeController(IGenieNoticeService noticeService)
        {
            this.noticeService = noticeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(string nt)
        {
            var info = await noticeService.GetNoticeInfo(nt);
            if (info == null)
            {
                return Redirect(UnitTool.UrlToSid("/Index/Index", ViewBag.MySid));
            }
            return View(info);
        }
    }
}