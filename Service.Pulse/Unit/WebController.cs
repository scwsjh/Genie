using Common;
using Furion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Service.Core;

namespace Service.Pulse
{
    public class WebController : Controller
    {
        /// <summary>
        /// 请求过滤处理
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var _WebConfigOptions = App.GetOptions<WebConfigOptions>();
            var _WebSysConfigOptions = App.GetOptions<SystemConfigOptions>();
            var _loginUser = App.GetService<ILoginUser>();
            int MessageCount = 0;
            if (_loginUser.IsLogin)
            {
                ViewBag.MySid = _loginUser.OnLineSid;
                ViewBag.MyUserId = _loginUser.userId;
                ViewBag.MyNo = _loginUser.userNo;
                ViewBag.MyNick = _loginUser.nick;
                ViewBag.IsOnLine = true;
                var wapMsgService = App.GetService<SiteBridg.Service.IWapMessageService>();
                MessageCount = wapMsgService.GetNoRedMsgCount(_loginUser.userNo);
            }
            else
            {
                ViewBag.MySid = "0";
                ViewBag.MyUserId = "-1";
                ViewBag.MyNo = 0;
                ViewBag.MyNick = "游客";
                ViewBag.IsOnLine = false;
            }
            ViewBag.MessageCount = MessageCount;
            ViewBag.WebName = _WebConfigOptions.WebName;
            ViewBag.Title = string.Format("{0}", _WebConfigOptions.WebTitle);
            ViewBag.keywords = _WebConfigOptions.KeyWord;
            ViewBag.TimeName = _WebConfigOptions.TimeName;
            //源站点信息
            ViewBag.LoginUrl = _WebSysConfigOptions.LoginUrl;
            ViewBag.RegUrl = _WebSysConfigOptions.RegUrl;
            ViewBag.SiteName = _WebSysConfigOptions.SiteName;
            ViewBag.SiteUrl = _WebSysConfigOptions.SiteUrl;
            ViewBag.HomeUrl = _WebSysConfigOptions.HomeUrl;
            ViewBag.MyHomeUrl = _WebSysConfigOptions.MyHomeUrl;
            ViewBag.BbsUrl = _WebSysConfigOptions.BbsUrl;
            ViewBag.PlateUrl = _WebSysConfigOptions.PlateUrl;
            ViewBag.OutUrl = _WebSysConfigOptions.OutUrl;
            base.OnActionExecuting(filterContext);
        }
    }
}