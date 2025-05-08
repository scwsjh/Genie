using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IndexController : AdminController
    {
        public IActionResult Index()
        {
            ViewBag.Title = "后台管理";
            return View();
        }
    }
}