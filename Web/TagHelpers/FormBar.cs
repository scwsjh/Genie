using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.TagHelpers
{
    [HtmlTargetElement("FormBar")]
    public class FormBar : TagHelper
    {
        public string action { get; set; }
        public string method { get; set; }
        public bool aspantiforgery { get; set; }
        public bool token { get; set; }
        public string tokenSign { get; set; }
        public string sid { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "form";
            //href = href.Contains("?") ? string.Format("{0}&sid={1}", href, StateHelper.GetId) : string.Format("{0}?sid={1}", href, StateHelper.GetId);
            //output.Attributes.SetAttribute("href", href);
            if (method.ToUpper().Equals("GET"))
            {
                output.Content.AppendHtml(string.Format("<input name='sid' type='hidden' value='{0}' />", sid));
            }
            else
            {
                action = action.Contains("?") ? string.Format("{0}&sid={1}", action, sid) : string.Format("{0}?sid={1}", action, sid);
            }
            if (token)
            {
                output.Content.AppendHtml(string.Format("<input name='_r' type='hidden' value='{0}' />", await UnitTool.SetPageToken(sid)));
            }
            else
            {
                if (!string.IsNullOrEmpty(tokenSign))
                {
                    output.Content.AppendHtml(string.Format("<input name='_r' type='hidden' value='{0}' />", tokenSign));
                }
            }

            output.Attributes.SetAttribute("action", action);
            output.Attributes.SetAttribute("method", method);
            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
    }
}