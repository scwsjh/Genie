using Furion;
using System.Web;

namespace Web
{
    public class PageHelper
    {
        public static string GetRequestUrl(HttpRequest source)
        {
            return $"{source.PathBase}{source.Path}{source.QueryString}";
        }

        public static string GetPagePathValue(Controller cl)
        {
            return cl.Request.Path.Value;
        }

        public static string PageConfirm(string tips, string name, string msg, string url, string sid, string noUrl = "")
        {
            string toUrl = string.Format("/Msg/Confirm?name={0}&msg={1}&url={2}&noUrl={3}&sid={4}", name, msg, HttpUtility.UrlEncode(url), noUrl, sid);
            return string.Format("<a href='{0}'>{1}</a>", toUrl, tips);
        }

        public static async Task<bool> CheakPageToken(Controller cl)
        {
            string sid = cl.Request.Query["sid"];
            string token = string.Empty;
            if (cl.Request.Method == "GET")
            {
                token = NullHelper.DealNullOrEmpty(cl.Request.Query["_r"]);
            }
            else
            {
                token = NullHelper.DealNullOrEmpty(cl.Request.Form["_r"]);
            }
            return await UnitTool.CheakPageToken(sid, token);
        }

        public static async Task<string> SetPageToken(Controller cl)
        {
            string sid = cl.Request.Query["sid"];
            return await UnitTool.SetPageToken(sid);
        }

        public static void SetPageStamp(Controller cl, out string stamp)
        {
            var redis = App.GetService<IRedisCache>();
            stamp = StringHelper.RandomString(22);
            redis.SetAsync(StateHelper.userId(cl), stamp, 60).GetAwaiter().GetResult();
        }

        public static bool CheakPageStamp(Controller cl)
        {
            var redis = App.GetService<IRedisCache>();
            string OnStamp = NullHelper.DealNullOrEmpty(cl.Request.Query["stamp"]);
            string stamp = redis.GetAsync<string>(StateHelper.userId(cl)).GetAwaiter().GetResult();

            return OnStamp.Equals(stamp);
        }

        public static async Task<bool> OperateLock(string id, string pref = "PK_KEY", int time = 1)
        {
            var redis = App.GetService<IRedisCache>();
            string key = string.Format("{0}_{1}", pref, id);
            if (await redis.ExistsAsync(key))
            {
                return false;
            }
            else
            {
                await redis.SetAsync(key, true, time);
                return true;
            }
        }

        public static async Task<bool> OperateCheakLock(string id, string pref = "PK_KEY")
        {
            var redis = App.GetService<IRedisCache>();
            string key = string.Format("{0}_{1}", pref, id);
            if (await redis.ExistsAsync(key))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task UnOperateLock(string id, string pref = "PK_KEY")
        {
            var redis = App.GetService<IRedisCache>();
            string key = string.Format("{0}_{1}", pref, id);
            await redis.DelAsync(key);
        }
    }

    public class PkLock
    {
        public int Count
        {
            get; set;
        }

        public DateTime AddTime
        {
            get; set;
        }
    }
}