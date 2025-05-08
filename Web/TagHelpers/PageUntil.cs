using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.TagHelpers
{
    [HtmlTargetElement("PageBar")]
    public class PageUntil : TagHelper
    {
        public int PageSize { get; set; }
        public int PageTotal { get; set; }
        public string PageUrl { get; set; }
        public string PageClass { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", PageClass);
            string Astr = "<a  href='{0}'>{1}</a>";
            string html = string.Empty;
            int pageCount = 1;
            if (PageTotal % PageSize == 0)
            {
                pageCount = (PageTotal / PageSize) > 1 ? (PageTotal / PageSize) : 1;
            }
            else
            {
                pageCount = (PageTotal / PageSize) > 0 ? (PageTotal / PageSize) + 1 : 1;
            }

            int onPage = RegExpHelper.IsNum(RegExpHelper.GetUrlParamenter(PageUrl, "p")) ? Convert.ToInt32(RegExpHelper.GetUrlParamenter(PageUrl, "p")) : 1;
            onPage = onPage < 1 ? 1 : onPage;
            onPage = onPage > pageCount ? pageCount : onPage;
            if (PageTotal > PageSize)
            {
                if (onPage > 1)
                {
                    html += string.Format(Astr, RegExpHelper.DealQueryString(PageUrl, "p", "1"), "首页");
                    html += " . ";
                    html += string.Format(Astr, RegExpHelper.DealQueryString(PageUrl, "p", (onPage - 1).ToString()), "上页");
                }
                if (onPage > 1 && onPage < pageCount)
                {
                    html += " . ";
                }
                if (onPage < pageCount)
                {
                    html += string.Format(Astr, RegExpHelper.DealQueryString(PageUrl, "p", (onPage + 1).ToString()), "下页");
                    html += " . ";
                    html += string.Format(Astr, RegExpHelper.DealQueryString(PageUrl, "p", pageCount.ToString()), "尾页");
                }
                string gurl = RegExpHelper.DealQueryString(PageUrl, "p", "1").Replace("?p=1", "");
                gurl = gurl.Replace("&p=1", "");
                html += string.Format("<br /><form  action='{0}' method='get'>", RegExpHelper.DelQueryString(gurl));
                html += string.Format("第{0}/{1}页&nbsp;", onPage, pageCount);
                var par = RegExpHelper.GetUrlParamenter(gurl);
                if (!string.IsNullOrEmpty(par))
                {
                    string[] pars = par.Split("&");
                    foreach (var item in pars)
                    {
                        string[] t = item.Split("=");
                        html += string.Format("<input name='{0}' type='hidden' value='{1}' />", t[0], System.Web.HttpUtility.UrlDecode(t[1]));
                    }
                }
                html += string.Format("<input name='p'  type='number' value='{0}' style='width: 50px'>&nbsp;", onPage);
                html += "<input class='ipt-btn-gray-m'  type='submit' value='跳转' />";

                html += "</form>";
            }

            output.Content.AppendHtml(html);
            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
    }

    [HtmlTargetElement("PageNum")]
    public class PageNum : TagHelper
    {
        public int PageSize { get; set; }
        public int PageTotal { get; set; }
        public string PageUrl { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "pagenum");
            string Astr = "<a  href='{0}' class='n_page' >{1}</a>";
            int pageCount = 1;
            if (PageTotal % PageSize == 0)
            {
                pageCount = (PageTotal / PageSize) > 1 ? (PageTotal / PageSize) : 1;
            }
            else
            {
                pageCount = (PageTotal / PageSize) > 0 ? (PageTotal / PageSize) + 1 : 1;
            }

            int onPage = RegExpHelper.IsNum(RegExpHelper.GetUrlParamenter(PageUrl, "p")) ? Convert.ToInt32(RegExpHelper.GetUrlParamenter(PageUrl, "p")) : 1;
            onPage = onPage < 1 ? 1 : onPage;
            onPage = onPage > pageCount ? pageCount : onPage;

            string html = "<div class='pagenum_left' >";

            if (onPage > 1)
            {
                html += string.Format(Astr, RegExpHelper.DealQueryString(PageUrl, "p", (onPage - 1).ToString()), "上一页");
            }
            html += string.Format("<span><i>{0}</i>/{1}</span>", onPage, pageCount);
            if (onPage < pageCount)
            {
                html += string.Format(Astr, RegExpHelper.DealQueryString(PageUrl, "p", (onPage + 1).ToString()), "下一页");
            }
            html += " </div>";
            html += " <div class='pagenum_right'>";
            string gurl = RegExpHelper.DealQueryString(PageUrl, "p", "1").Replace("?p=1", "");
            gurl = gurl.Replace("&p=1", "");
            html += string.Format("<form  action='{0}' method='get'>", RegExpHelper.DelQueryString(gurl));
            var par = RegExpHelper.GetUrlParamenter(gurl);
            if (!string.IsNullOrEmpty(par))
            {
                string[] pars = par.Split("&");
                foreach (var item in pars)
                {
                    string[] t = item.Split("=");
                    html += string.Format("<input name='{0}' type='hidden' value='{1}' />", t[0], t[1]);
                }
            }
            html += "<input  type='submit' value='GO' class='but' emptyok='true' />";
            html += "<input name='p'  type='text' value='' />";

            html += "</form></div>";
            output.Content.AppendHtml(html);

            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
    }

    [HtmlTargetElement("PageMsg")]
    public class PageMsg : TagHelper
    {
        public string msg { get; set; }
        public string sid { get; set; }
        private readonly IMemoryCache cache;

        public PageMsg(IMemoryCache _cache)
        {
            cache = _cache;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "page-msg");
            string key = string.Format("GAME_MSG:PAGE:{0}", sid);
            string html = string.Empty;
            if (await cache.ExistsAsync(key))
            {
                html = await cache.GetAsync(key);
                await cache.DelAsync(key);
            }
            output.Content.AppendHtml(html);
            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
    }
}