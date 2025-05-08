using Furion;

namespace Web
{
    public class MessageHelper
    {
        public static IActionResult Msg(Controller cl, string title, string msg, string url, bool isToUrl = false, string buttonText = "返回", string sid = "")
        {
            var cache = App.GetService<IMemoryCache>();
            ResultData result = new ResultData();
            result.SetValue("title", title);
            result.SetValue("msg", msg);
            result.SetValue("url", url);
            result.SetValue("button", buttonText);
            result.SetValue("isToUrl", isToUrl);

            string retSid = string.IsNullOrEmpty(sid) ? cl.Request.Query["sid"] : sid;
            string key = string.Format("GAME_MSG:UNIT:{0}", retSid);
            cache.Set(key, result.ToJson(), 600);

            return cl.Redirect("/Msg/Index?sid=" + retSid);
        }

        public static IActionResult MsgPage(Controller cl, string msg, string url, string sid = "")
        {
            string retSid = string.IsNullOrEmpty(sid) ? cl.Request.Query["sid"] : sid;
            SetPageMsg(cl, msg, retSid);
            return cl.Redirect(UnitTool.UrlToSid(url, retSid));
        }

        private static void SetPageMsg(Controller cl, string msg, string sid)
        {
            var cache = App.GetService<IMemoryCache>();
            string key = string.Format("GAME_MSG:PAGE:{0}", sid);
            if (!string.IsNullOrEmpty(msg))
            {
                cache.Set(key, string.Format("*{0}", msg), 60);
            }
        }
    }
}