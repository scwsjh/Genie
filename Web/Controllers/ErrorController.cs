namespace Web.Controllers
{
    public class ErrorController : WebController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NoPower()
        {
            ViewBag.Title = "暂无权限";
            if (ViewBag.IsOnLine == false)
            {
                return Redirect(ViewBag.LoginUrl);
            }
            return View();
        }

        [HttpGet]
        public IActionResult LimitRqu()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NoPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            ViewBag.ErrMsg = Request.Query["msg"];
            return View();
        }
    }
}