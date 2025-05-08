using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Service.Core;
using System;
using System.Threading.Tasks;

namespace Service.Pulse
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string sid = context.Request.Query["sid"];
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var statusCode = context.Response.StatusCode;

                if (ex is ArgumentException)
                {
                    statusCode = 500;
                }
                //context.Response.Redirect(UnitTool.UrlToSid("/Error/Error?sid=" + sid + "&msg=" + ex.Message, sid));
                await HandleExceptionAsync(context, statusCode, ex.Message);
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                var url = "";
                if (statusCode == 401)
                {
                    url = UnitTool.UrlToSid("/Error/NoPower", sid);
                }
                else if (statusCode == 404)
                {
                    url = UnitTool.UrlToSid("/Error/NoPage", sid);
                }
                else if (statusCode == 439)
                {
                    url = UnitTool.UrlToSid("/Error/LimitRqu", sid);
                }
                else if (statusCode == 502)
                {
                    url = UnitTool.UrlToSid("/Error/Error", sid);
                }
                if (!string.IsNullOrWhiteSpace(url))
                {
                    await HandleExceptionAsync(context, statusCode, url);
                    //context.Response.Redirect(url);
                }
            }
        }

        //异常错误信息捕获，将错误信息用Json方式返回
        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(msg.ToLower());
        }
    }

    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}