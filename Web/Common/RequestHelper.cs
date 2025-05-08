namespace Web
{
    public class RequestHelper
    {
        public static string GetRequestUrl(HttpRequest source)
        {
            return $"{source.PathBase}{source.Path}{source.QueryString}";
        }
    }
}