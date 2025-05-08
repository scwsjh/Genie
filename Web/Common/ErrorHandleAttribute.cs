using Microsoft.AspNetCore.Mvc.Filters;

namespace Web
{
    public class ErrorHandleAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //获取异常信息，入库保存
            var exception = filterContext.Exception;
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            var sid = filterContext.HttpContext.Request.Query["sid"];
            var msg = $"出错位置:{controllerName}/{actionName}/sid:{sid}----出错时间:{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}";
            Log4netHelper.Error(msg, exception);//记录异常日志到本地磁盘
            filterContext.Result = new RedirectResult("/Error/Error");
            base.OnException(filterContext);
        }
    }
}