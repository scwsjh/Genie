namespace Web
{
    public class WebTitleConfig
    {
        public static string GetTitle(string name, string title)
        {
            return string.Format("{0}-{1}", name, title);
        }
    }
}