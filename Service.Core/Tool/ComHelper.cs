using Microsoft.AspNetCore.Http;

namespace Service.Core
{
    public class ComHelper
    {
        /// <summary>
        /// 获取客户Ip
        /// </summary>
        /// <param name = "context" ></ param >
        /// < returns ></ returns >
        public static string GetClientUserIp(HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            if (ip.Contains(","))
            {
                ip = ip.Split(',')[0];
            }
            //var ip = context.Request.Cookies["kxUserIp"];
            //if (string.IsNullOrEmpty(ip))
            //{
            //    ip = "0.0.0.0";
            //}

            return ip;
        }
    }
}