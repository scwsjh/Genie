using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.TagHelpers
{
    [HtmlTargetElement("Abar")]
    public class Abar : TagHelper
    {
        public string href { get; set; }
        public string sid { get; set; }
        public string AClass { get; set; }
        public bool token { get; set; }
        public string tokenSign { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("class", AClass);
            if (!string.IsNullOrEmpty(NullHelper.DealNullOrEmpty(href)))
            {
                href = href.Contains("?") ? string.Format("{0}&sid={1}", href, sid) : string.Format("{0}?sid={1}", href, sid);
                if (token)
                {
                    href += string.Format("&_r={0}", await UnitTool.SetPageToken(sid));
                }
                else
                {
                    if (!string.IsNullOrEmpty(tokenSign))
                    {
                        href += string.Format("&_r={0}", tokenSign);
                    }
                }
                output.Attributes.SetAttribute("href", href);
            }
            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
    }

    [HtmlTargetElement("Acheak")]
    public class Acheak : TagHelper
    {
        public string name { get; set; }
        public string href { get; set; }
        public string OnCheak { get; set; }
        public string OnValue { get; set; }
        public string sid { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (OnCheak.Equals(OnValue))
            {
                //output.PreContent.SetHtmlContent("<span>");
                //output.PostContent.SetHtmlContent("</span>");
            }
            else
            {
                output.TagName = "a";
                href = href.Contains("?") ? string.Format("{0}&sid={1}", href, sid) : string.Format("{0}?sid={1}", href, sid);
                output.Attributes.SetAttribute("href", href);
            }

            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
    }

    [HtmlTargetElement("AStyleCheak")]
    public class AStyleCheak : TagHelper
    {
        public string name { get; set; }
        public string href { get; set; }
        public string OnCheak { get; set; }
        public string OnValue { get; set; }
        public string sid { get; set; }
        public string CheakClass { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            href = href.Contains("?") ? string.Format("{0}&sid={1}", href, sid) : string.Format("{0}?sid={1}", href, sid);
            output.Attributes.SetAttribute("href", href);

            if (OnCheak.Equals(OnValue))
            {
                output.Attributes.SetAttribute("class", CheakClass);
            }
            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
    }
}