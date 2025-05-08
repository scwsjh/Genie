using Furion;
using Microsoft.AspNetCore.Mvc;
using SiteBridg.Service;

namespace Web.Controllers
{
    public class IndexController : GameController
    {
        private readonly IGenieUserService userService;
        private readonly IGenieCallService callService;
        private readonly IGenieService genieService;
        private readonly IGenieThingService thingService;
        private readonly IGenieTeamService teamService;
        private readonly IWapUserService wapUserService;
        private readonly IGenieChatService chatService;
        private readonly IGenieAccService accService;
        private readonly IAdminUserService adminUserService;
        private readonly IGenieDicService dicService;
        private readonly IGenieNoticeService noticeService;

        public IndexController(IGenieUserService userService, IGenieCallService callService, IGenieService genieService, IGenieThingService thingService, IGenieTeamService teamService, IWapUserService wapUserService, IGenieChatService chatService, IGenieAccService accService, IAdminUserService adminUserService, IGenieDicService dicService, IGenieNoticeService noticeService)
        {
            this.userService = userService;
            this.callService = callService;
            this.genieService = genieService;
            this.thingService = thingService;
            this.teamService = teamService;
            this.wapUserService = wapUserService;
            this.chatService = chatService;
            this.accService = accService;
            this.adminUserService = adminUserService;
            this.dicService = dicService;
            this.noticeService = noticeService;
        }

        public async Task<IActionResult> Index()
        {
            int uid = StateHelper.userNo(this);
            var genieUserInfo = await userService.GetGenieUserInfo(uid);
            var accInfo = await accService.GetAccInfo(uid);
            //元气
            var vigorInfo = await userService.GetUserVigor(uid);
            //处理秘境
            var callData = await callService.GetCallData();
            List<CallTemp> CallRes = new List<CallTemp>();
            foreach (var item in callData)
            {
                CallTemp temp = new CallTemp();
                temp.call = item;
                temp.time = await callService.GetUserCallCoolTime(uid, item.id);
                CallRes.Add(temp);
            }
            ViewBag.MyVigor = vigorInfo;
            ViewBag.CallRes = CallRes;
            ViewBag.MyGenieCount = await genieService.GetUserGenieCount(uid);
            ViewBag.UserInfo = genieUserInfo;

            RefAsync<int> total = 0;
            ViewBag.Thing = await thingService.GetGenieThing(1, 3, total);
            ViewBag.LevInfo = UnitTool.GetLevData((int)accInfo.exploit);
            ViewBag.MaxGenCount = GameTool.GetMaxGenieCount((int)genieUserInfo.lev);

            //聊天信息
            ViewBag.ChatList = await chatService.GetMainMsg(uid);

            //判断是否是后台管理员
            ViewBag.IsAdmin = await adminUserService.GetAdminUserInfoByNo(uid) == null ? false : true;

            //公告
            ViewBag.Notice = await noticeService.GetGenieNoticeOnData();

            return View();
        }

        public async Task<IActionResult> Wu()
        {
            int uid = StateHelper.userNo(this);
            var genieUser = await userService.GetGenieUserInfo(uid);
            ViewBag.MaxGenCount = GameTool.GetMaxGenieCount((int)genieUser.lev);
            ViewBag.MyGenieCount = await genieService.GetUserGenieCount(uid);
            var accInfo = await accService.GetAccInfo(uid);

            var levInfo = UnitTool.GetLevData((int)accInfo.exploit);
            ViewBag.IsGetAward = await userService.CheakUserHonnerIsGet(uid);
            ViewBag.LevInfo = levInfo;
            ViewBag.MaxExp = GameTool.GetUserUpExp((int)genieUser.lev);
            ViewBag.Buff = await userService.GetUserBuff(uid);
            ViewBag.AccInfo = accInfo;
            return View(genieUser);
        }

        public async Task<IActionResult> Home(int id)
        {
            var userInfo = await userService.GetGenieUserInfo(id);
            var data = await teamService.GetUserTeam(id);
            List<TeamTemp> team = new List<TeamTemp>();
            List<TempResult> result = new List<TempResult>();

            if (!string.IsNullOrEmpty(data.team))
            {
                team = JsonConvert.DeserializeObject<List<TeamTemp>>(data.team);
                foreach (var item in team)
                {
                    TempResult temp = new TempResult();
                    temp.num = item.num;
                    temp.id = item.id;
                    temp.genId = item.genId;
                    temp.name = item.name;
                    var genInfo = await genieService.GetUserGenieInfo(item.id);
                    temp.lev = (int)genInfo.lev;
                    temp.start = (int)genInfo.start;
                    result.Add(temp);
                }
            }

            var domainUser = await wapUserService.GetUserInfo(id);
            if (domainUser == null)
            {
                return Redirect(UnitTool.UrlToSid("/Index/Index", ViewBag.MySid));
            }
            ViewBag.DomainUser = domainUser;
            ViewBag.GenieUser = userInfo;
            RefAsync<int> total = 0;
            ViewBag.Thing = await thingService.GetGenieThing(1, 3, total, domainUser.id);
            var accInfo = await accService.GetAccInfo(userInfo.uid);
            ViewBag.LevInfo = UnitTool.GetLevData((int)accInfo.exploit);
            ViewBag.MyGenieCount = await genieService.GetUserGenieCount(domainUser.id);
            ViewBag.Buff = await userService.GetUserBuff(domainUser.id);
            ViewBag.AccInfo = accInfo;
            return View(result);
        }

        public async Task<IActionResult> Rank(int t)
        {
            int PageIndex = StringHelper.ConvertPage(Request.Query["p"]);
            int PageSize = 10;
            RefAsync<int> Total = 0;
            if (t == 0)
            {
                var result = await userService.GetUserRank(PageIndex, PageSize, Total);
                ViewBag.RankTemp = result;
            }
            else if (t == 1)
            {
                var result = await accService.GetAccRankByExploit(PageIndex, PageSize, Total);
                ViewBag.RankTemp = result;
            }

            var moneyService = App.GetService<IWapUserService>();
            ViewBag.MoneyData = await moneyService.GetMoneyData();
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.Total = Total;
            ViewBag.Type = t;
            return View();
        }

        public async Task<IActionResult> Consume(int t, int c)
        {
            int PageIndex = StringHelper.ConvertPage(Request.Query["p"]);
            int PageSize = 10;
            RefAsync<int> Total = 0;
            var result = await accService.GetConsumeInfoBySite(t, c, PageIndex, PageSize, Total);

            var moneyService = App.GetService<IWapUserService>();
            ViewBag.MoneyData = await moneyService.GetMoneyData();

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.Total = Total;
            ViewBag.Type = t;
            ViewBag.TimeNo = c;
            return View(result);
        }

        public async Task<IActionResult> GetHonnerAward()
        {
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid("/Index/Wu", ViewBag.MySid));
            }

            var dicInfo = await dicService.GetDicInfo(DicEnum.Code.HonnerAward.ToString());
            if (dicInfo == null)
            {
                return MessageHelper.Msg(this, "领奖", "暂无可领取的奖励!", "/Index/Wu");
            }
            List<RankTemp> award = JsonUtil.DeserializeJsonToList<RankTemp>(dicInfo.sign);
            if (award.Count == 0)
            {
                return MessageHelper.Msg(this, "领奖", "暂无可领取的奖励!", "/Index/Wu");
            }
            int uid = StateHelper.userNo(this);
            if (await userService.CheakUserHonnerIsGet(uid))
            {
                return MessageHelper.Msg(this, "领奖", "奖励已经领取过啦!", "/Index/Wu");
            }
            var accInfo = await accService.GetAccInfo(uid);
            var levInfo = UnitTool.GetLevData((int)accInfo.exploit);

            RankTemp item = award.Find(it => it.min <= levInfo.lev && levInfo.lev <= it.max);
            if (item == null)
            {
                return MessageHelper.Msg(this, "领奖", "暂无奖励!", "/Index/Wu");
            }
            if (await userService.SetUserHonnerGet(uid))
            {
                if (await GameTool.PutPropBatch(uid, 1, item.award, "争霸赛"))
                {
                    string _retips = GameTool.GetPropTips(item.award);
                    string thing = $"领取【{levInfo.name}】称号奖励,获得:{_retips}";
                    await thingService.AddGenieThing(uid, thing);
                    return MessageHelper.Msg(this, "领奖", $"领取成功,获得:{_retips}", "/Index/Wu");
                }
                else
                {
                    return MessageHelper.Msg(this, "领奖", "领取失败,请联系管理员!", "/Index/Wu");
                }
            }
            else
            {
                return MessageHelper.Msg(this, "领奖", "领取失败,请稍后尝试!", "/Index/Wu");
            }
        }
    }
}