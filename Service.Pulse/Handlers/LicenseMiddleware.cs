using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Pulse.Handlers
{
    public class LicenseMiddleware
    {
        private readonly RequestDelegate _next;

        public LicenseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var url = $"{context.Request.Scheme}://{context.Request.Host}";
            Console.WriteLine($"Request URL: {url}"); // 或者根据需要使用url做其他事情

            await _next(context); // 继续执行管道中的下一个中间件或处理程序。
        }
    }
}