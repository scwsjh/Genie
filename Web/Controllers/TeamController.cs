using Mapster.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class TeamController : GameController
    {
        private readonly IGenieTeamService teamService;
        private readonly IGenieService genieService;
        private readonly IGenieUserService userService;

        public TeamController(IGenieTeamService teamService, IGenieService genieService, IGenieUserService userService)
        {
            this.teamService = teamService;
            this.genieService = genieService;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "精灵编队";
            int uid = StateHelper.userNo(this);
            var userInfo = await userService.GetGenieUserInfo(uid);
            var data = await teamService.GetUserTeam(uid);
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

            ViewBag.GenieLev = userInfo.lev;
            ViewBag.PageToken = await PageHelper.SetPageToken(this);
            return View(result);
        }

        public async Task<IActionResult> Change(int num)
        {
            ViewBag.Title = "编队-选择精灵";
            int PageIndex = StringHelper.ConvertPage(Request.Query["p"]);
            int PageSize = 10;
            RefAsync<int> Total = 0;
            int uid = StateHelper.userNo(this);
            var userInfo = await userService.GetGenieUserInfo(uid);

            var MyTeam = await teamService.GetUserTeam(uid);
            List<string> noAt = new List<string>();
            if (!string.IsNullOrEmpty(MyTeam.team))
            {
                List<TeamTemp> team = JsonConvert.DeserializeObject<List<TeamTemp>>(MyTeam.team);
                foreach (var item in team)
                {
                    noAt.Add(item.id);
                }
            }
            var data = await genieService.GetUserGenieDataBySerch(uid, noAt, PageIndex, PageSize, Total);

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.Total = Total;
            ViewBag.Num = num;
            ViewBag.PageToken = await PageHelper.SetPageToken(this);
            return View(data);
        }

        public async Task<IActionResult> ChangeOk(int num, string guid)
        {
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid("/Team/Index", ViewBag.MySid));
            }
            num = num < 0 ? 1 : num;
            num = num > 5 ? 1 : num;
            int uid = StateHelper.userNo(this);
            var userInfo = await userService.GetGenieUserInfo(uid);

            int mustLev = (num - 1) * 5;
            if (userInfo.lev < mustLev)
            {
                return MessageHelper.MsgPage(this, "花园等级未达到要求!", "/Team/Index");
            }
            var MyTeam = await teamService.GetUserTeam(uid);
            List<TeamTemp> team = new List<TeamTemp>();
            if (!string.IsNullOrEmpty(MyTeam.team))
            {
                team = JsonConvert.DeserializeObject<List<TeamTemp>>(MyTeam.team);
                if (team.Any(it => it.id == guid))
                {
                    return MessageHelper.MsgPage(this, "该精灵已在编队!", "/Team/Index");
                }
            }
            var genInfo = await genieService.GetUserGenieInfo(guid);
            if (genInfo == null)
            {
                return MessageHelper.MsgPage(this, "精灵不存在!", "/Team/Index");
            }
            if (genInfo.uid != uid)
            {
                return MessageHelper.MsgPage(this, "精灵不存在!", "/Team/Index");
            }
            if (team.Any(it => it.genId == genInfo.genieId))
            {
                return MessageHelper.MsgPage(this, "同种精灵只能编队1个哦!", "/Team/Index");
            }

            //开始变更
            int atIndex = team.FindIndex(it => it.num == num);
            if (atIndex > -1)
            {
                team.RemoveAt(atIndex);
            }
            //添加新队列
            TeamTemp newTeam = new TeamTemp();
            newTeam.num = num;
            newTeam.id = guid;
            newTeam.genId = (int)genInfo.genieId;
            newTeam.name = genInfo.name;
            team.Add(newTeam);
            team = team.OrderBy(it => it.num).ToList();
            MyTeam.team = JsonConvert.SerializeObject(team);
            if (!await teamService.UpdateTeam(MyTeam))
            {
                return MessageHelper.MsgPage(this, "上场失败,请稍后尝试!", "/Team/Index");
            }
            return Redirect(UnitTool.UrlToSid("/Team/Index", ViewBag.MySid));
        }

        public async Task<IActionResult> Down(int num)
        {
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid("/Team/Index", ViewBag.MySid));
            }
            num = num < 0 ? 1 : num;
            num = num > 5 ? 1 : num;
            int uid = StateHelper.userNo(this);

            var MyTeam = await teamService.GetUserTeam(uid);
            List<TeamTemp> team = new List<TeamTemp>();
            if (!string.IsNullOrEmpty(MyTeam.team))
            {
                team = JsonConvert.DeserializeObject<List<TeamTemp>>(MyTeam.team);
            }
            int atIndex = team.FindIndex(it => it.num == num);
            if (atIndex > -1)
            {
                team.RemoveAt(atIndex);
            }
            MyTeam.team = JsonConvert.SerializeObject(team);
            if (!await teamService.UpdateTeam(MyTeam))
            {
                return MessageHelper.MsgPage(this, "下场失败,请稍后尝试!", "/Team/Index");
            }
            return Redirect(UnitTool.UrlToSid("/Team/Index", ViewBag.MySid));
        }
    }
}