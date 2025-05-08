using System.Web;

namespace Web.Controllers
{
    public class MsgController : Controller
    {
        private readonly IMemoryCache cache;

        public MsgController(IMemoryCache _cache)
        {
            cache = _cache;
        }

        public async Task<IActionResult> Index()
        {
            string sid = Request.Query["sid"];
            string key = string.Format("GAME_MSG:UNIT:{0}", sid);
            var data = await cache.GetAsync(key);
            if (string.IsNullOrEmpty(data))
            {
                return Redirect(UnitTool.UrlToSid("/Index/Index", sid));
            }
            ResultData result = new ResultData();
            result.FromJson(data);
            if (result != null)
            {
                ViewBag.Title = result.GetValue("title");
                ViewBag.Msg = result.GetValue("msg");
                ViewBag.Url = result.GetValue("url");
                ViewBag.isToUrl = result.GetValue("isToUrl");
                ViewBag.ButtonName = result.GetValue("button");
                ViewBag.MsgStatus = true;
            }
            else
            {
                ViewBag.MsgStatus = false;
            }
            //删除
            await cache.DelAsync(key);
            ViewBag.MySid = sid;
            return View();
        }

        public IActionResult Confirm(string name, string msg, string url, string noUrl = "")
        {
            string sid = Request.Query["sid"];
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(msg) || string.IsNullOrEmpty(url))
            {
                return Redirect(UnitTool.UrlToSid("/Index/", sid));
            }
            else
            {
                ViewBag.Title = name;
                ViewBag.Msg = msg;
                ViewBag.yesUrl = HttpUtility.UrlDecode(url);
                ViewBag.noUrl = noUrl;
                ViewBag.MySid = sid;
            }
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmMsg()
        {
            ResultJson result = new ResultJson();
            try
            {
                string msg = Request.Form["msg"].ToString();
                string yesUrl = Request.Form["yesUrl"].ToString();
                string noUrl = Request.Form["noUrl"].ToString();
                ResultData data = new ResultData();
                data.SetValue("title", "操作提示");
                data.SetValue("msg", msg);
                data.SetValue("yesUrl", yesUrl);
                data.SetValue("noUrl", noUrl);
                TempData["CONFIRM_MSG"] = data.ToJson();

                result.Code = Convert.ToInt32(ResultCode.Code.成功);
                result.Msg = "";
            }
            catch (Exception ex)
            {
                result.Code = Convert.ToInt32(ResultCode.Code.失败);
                result.Msg = ex.Message;
            }

            return JsonHelper.jsonResult(result);
        }
    }
}