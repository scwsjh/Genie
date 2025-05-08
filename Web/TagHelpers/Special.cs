using Furion;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.TagHelpers
{
    [HtmlTargetElement("Nick")]
    public class Nick : TagHelper
    {
        private readonly IWapUserService userService;

        public Nick(IWapUserService _userService)
        {
            userService = _userService;
        }

        public int userNo { get; set; }
        public int areaId { get; set; }
        public string sid { get; set; }
        public string userId { get; set; }
        public int nickType { get; set; }
        public string style { get; set; }
        public string type { get; set; }
        public bool showNo { get; set; } = false;//显示no
        public bool showTitle { get; set; } = false;//显示官衔
        public bool showBadge { get; set; } = false;//显示徽章
        public bool showNation { get; set; } = false;//是否跳转
        public bool showHead { get; set; } = false;//是否跳转
        public bool showOnLine { get; set; } = false;//是否显示在线/离线

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            string html = string.Empty;
            if (userNo == 0 || userId == "0")
            {
                html += "系统";
            }
            else
            {
                wap_user userInfo = new wap_user();
                if (string.IsNullOrEmpty(userId))
                {
                    userInfo = await userService.GetUserInfo(userNo);
                }
                //else
                //{
                //    userInfo = await userService.GetUserInfoByUserId(userId);
                //}
                if (userInfo != null)
                {
                    html += string.Format("<a class='a-nodec' style='{3}' href='/Index/Home/{0}?sid={1}'>{2}</a>", userInfo.id, sid, userInfo.name, style);
                    //显示ID
                    if (showNo)
                    {
                        html += string.Format("(ID:{0})", userInfo.id);
                    }
                }
            }

            output.Content.AppendHtml(html);
            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
    }
}