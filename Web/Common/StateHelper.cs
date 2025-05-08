namespace Web
{
    public class StateHelper
    {
        public static bool IsOnline(Controller page)
        {
            return page.ViewBag.IsOnLine == true;
        }

        public static string userId(Controller page)
        {
            return page.ViewBag.MyUserId ?? "0";
        }

        public static string Sid(Controller page)
        {
            return page.ViewBag.MySid ?? "";
        }

        public static string Nick(Controller page)
        {
            return page.ViewBag.MyNick ?? "";
        }

        public static int userNo(Controller page)
        {
            return page.ViewBag.MyNo;
        }
    }
}