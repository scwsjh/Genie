using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ToolController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Setting()
        {
            return View();
        }
    }
}