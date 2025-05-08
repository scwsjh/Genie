using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.Core
{
    public class ConvertContent
    {
        public static string ConvertHtml(string str, string sid)
        {
            string htmlStr = str;
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            htmlStr = htmlStr.Replace("[Br]", "<br/>");
            htmlStr = Regex.Replace(htmlStr, @"\[HREF=(?<no>.+?)](?<name>.+?)\[/HREF]", "<a href='${no}&sid=" + sid + "' >${name}</a>");

            return htmlStr;
        }
    }
}