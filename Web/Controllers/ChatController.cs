using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ChatController : GameController
    {
        private readonly IGenieChatService chatService;
        private readonly IWapUserService userService;

        public ChatController(IGenieChatService chatService, IWapUserService userService)
        {
            this.chatService = chatService;
            this.userService = userService;
        }

        public async Task<IActionResult> Index(int t, int u)
        {
            t = t == 0 ? 0 : 1;
            int PageIndex = StringHelper.ConvertPage(Request.Query["p"]);
            int PageSize = 10;
            RefAsync<int> Total = 0;
            int uid = StateHelper.userNo(this);
            List<genie_chat> result = new List<genie_chat>();
            result = await chatService.GetChatData(uid, t, PageIndex, PageSize, Total);

            if (u != 0)
            {
                var toUser = await userService.GetUserInfo(u);
                if (toUser != null)
                {
                    ViewBag.ToUserStr = $"回复:<strong>{toUser.name}</strong>&nbsp;&nbsp;&nbsp;&nbsp;<span onclick='del()' style='color:#1e5494'>[删除]</span>";
                }
                else
                {
                    ViewBag.ToUserStr = "";
                    u = 0;
                }
            }

            ViewBag.Type = t;
            ViewBag.ToUid = u;
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.Total = Total;
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Send(int uid, string sign, int t)
        {
            string url = $"/Chat/Index?t={t}&u={uid}";
            if (string.IsNullOrEmpty(sign))
            {
                return MessageHelper.MsgPage(this, "发言内容不能为空!", url);
            }
            int MyId = StateHelper.userNo(this);
            string nick = StateHelper.Nick(this);
            sign = StringHelper.NoHTML(sign);
            sign = StringHelper.Substr(sign, 80);
            string code = ChatEnum.code.Public.ToString();
            if (uid != 0)
            {
                var toInfo = await userService.GetUserInfo(uid);
                if (toInfo == null)
                {
                    return MessageHelper.MsgPage(this, "用户不存在!", url);
                }
                code = ChatEnum.code.User.ToString();
            }
            if (await chatService.AddChat(MyId, nick, uid, code, sign))
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            else
            {
                return MessageHelper.MsgPage(this, "发言失败,请稍后尝试!", url);
            }
        }
    }
}