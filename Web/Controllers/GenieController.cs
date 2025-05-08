using Microsoft.AspNetCore.Mvc;
using TencentCloud.Mgobe.V20201014.Models;

namespace Web.Controllers
{
    public class GenieController : GameController
    {
        private readonly IGenieService genieService;
        private readonly IGenieThingService thingService;
        private readonly IGenieTeamService teamService;

        public GenieController(IGenieService genieService, IGenieThingService thingService, IGenieTeamService teamService)
        {
            this.genieService = genieService;
            this.thingService = thingService;
            this.teamService = teamService;
        }

        public async Task<IActionResult> Index(int t)
        {
            ViewBag.Title = "我的精灵";
            int type = t == 0 ? 1 : t;
            int PageIndex = StringHelper.ConvertPage(Request.Query["p"]);
            int PageSize = 10;
            RefAsync<int> Total = 0;
            int uid = StateHelper.userNo(this);

            var data = await genieService.GetUserGenieData(uid, type, PageIndex, PageSize, Total);

            ViewBag.Type = type;
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.Total = Total;
            return View(data);
        }

        public async Task<IActionResult> Info(string gu, int t, string fgu)
        {
            ViewBag.Title = "精灵资料";
            int uid = StateHelper.userNo(this);
            var data = await genieService.GetUserGenieInfoByAttr(gu);
            if (data == null)
            {
                return Redirect(UnitTool.UrlToSid("/Genie/Index", ViewBag.MySid));
            }
            bool isMy = uid == data.uid;
            if (!string.IsNullOrEmpty(fgu))
            {
                int genType = (int)data.type - 1;
                var fusInfo = await genieService.GetUserGenieInfo(fgu);
                if (fusInfo != null)
                {
                    if (fusInfo.start >= data.start && fusInfo.type >= genType && fusInfo.id != data.id)
                    {
                        t = 1;
                        ViewBag.FusInfo = fusInfo;
                    }
                }
            }

            var genieInfo = await genieService.GetGenieInfo((int)data.genieId);

            //升级信息
            var NeedData = JsonConvert.DeserializeObject<List<TowerNeed>>(genieInfo.upNeed);
            int bs = Convert.ToInt32(Math.Pow(2, (int)data.lev - 1));
            var CheakData = await GameTool.CheakNeed(uid, NeedData, ViewBag.MySid, bs);

            ViewBag.CheakData = CheakData;
            ViewBag.IsMy = isMy;
            ViewBag.GenieInfo = genieInfo;
            ViewBag.Type = t == 0 ? 0 : 1;
            return View(data);
        }

        public async Task<IActionResult> Skill(string gu)
        {
            var data = await genieService.GetUserGenieInfo(gu);
            if (data == null)
            {
                return Redirect(UnitTool.UrlToSid("/Genie/Index", ViewBag.MySid));
            }
            SkillAttr skill = JsonConvert.DeserializeObject<SkillAttr>(data.skill);
            ViewBag.Skill = skill;
            return View(data);
        }

        public async Task<IActionResult> UpLevOk(string gu, int t)
        {
            string url = $"/Genie/Info?gu={gu}&t={t}";
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            int uid = StateHelper.userNo(this);
            var data = await genieService.GetUserGenieInfo(gu);
            if (data == null)
            {
                return Redirect(UnitTool.UrlToSid("/Genie/Index", ViewBag.MySid));
            }
            if (uid != data.uid)
            {
                return Redirect(UnitTool.UrlToSid("/Genie/Index", ViewBag.MySid));
            }
            if (data.lev > GameConfig.MaxGenieLev)
            {
                return MessageHelper.MsgPage(this, "已达到最高级啦!", url);
            }
            var genieInfo = await genieService.GetGenieInfo((int)data.genieId);
            //升级信息
            var NeedData = JsonConvert.DeserializeObject<List<TowerNeed>>(genieInfo.upNeed);
            if (NeedData.Count == 0)
            {
                return MessageHelper.MsgPage(this, "不满足升级条件!", url);
            }
            int bs = Convert.ToInt32(Math.Pow(2, (int)data.lev - 1));
            var CheakData = await GameTool.CheakNeed(uid, NeedData, ViewBag.MySid, bs);
            if (CheakData.result)
            {
                if (await GameTool.UpdateBagBatch(uid, 0, NeedData, "升级", false, bs))
                {
                    //更新等级
                    data.lev = data.lev + 1;
                    if (await genieService.UpdateUserGenie(data))
                    {
                        string thing = string.Format("成功晋升【精灵·{0}】的等级至{1}级!", data.name, data.lev);
                        await thingService.AddGenieThing(uid, thing);
                        return MessageHelper.MsgPage(this, "升级成功,等级+1!", url);
                    }
                    else
                    {
                        return MessageHelper.MsgPage(this, "升级失败,请稍后尝试!", url);
                    }
                }
                else
                {
                    return MessageHelper.MsgPage(this, "物品扣除失败,请稍后尝试!", url);
                }
            }
            else
            {
                return MessageHelper.MsgPage(this, "不满足升级要求!", url);
            }
        }

        public async Task<IActionResult> SerchGenie(string gu)
        {
            int PageIndex = StringHelper.ConvertPage(Request.Query["p"]);
            int PageSize = 10;
            RefAsync<int> Total = 0;
            int uid = StateHelper.userNo(this);
            var data = await genieService.GetUserGenieInfo(gu);
            if (data == null)
            {
                return Redirect(UnitTool.UrlToSid("/Genie/Index", ViewBag.MySid));
            }
            string url = $"/Genie/Info?gu={gu}&t=1";

            if (data.uid != uid)
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            int type = (int)data.type - 1;
            var result = await genieService.GetUserGenieDataByFusion(uid, type, (int)data.start, gu, PageIndex, PageSize, Total);
            ViewBag.GuId = gu;
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.Total = Total;
            return View(result);
        }

        public async Task<IActionResult> GenieFusion(string gu, string fgu)
        {
            string url = $"/Genie/Info?gu={gu}&fgu={fgu}&t=1";
            string okUrl = $"/Genie/Info?gu={gu}&t=1";
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            if (gu == fgu)
            {
                return Redirect(UnitTool.UrlToSid(okUrl, ViewBag.MySid));
            }
            int uid = StateHelper.userNo(this);
            var data = await genieService.GetUserGenieInfo(gu);
            if (data == null)
            {
                return Redirect(UnitTool.UrlToSid("/Genie/Index", ViewBag.MySid));
            }
            if (data.uid != uid)
            {
                return MessageHelper.MsgPage(this, "精灵不存在!", okUrl);
            }
            if (data.start >= data.lev)
            {
                return MessageHelper.MsgPage(this, "已达到该精灵融合上限!", okUrl);
            }
            var fusGeine = await genieService.GetUserGenieInfo(fgu);
            if (fusGeine == null)
            {
                return MessageHelper.MsgPage(this, "精灵不存在!", okUrl);
            }
            if (fusGeine.uid != uid)
            {
                return MessageHelper.MsgPage(this, "精灵不存在!", okUrl);
            }
            if (fusGeine.type < (data.type - 1))
            {
                return MessageHelper.MsgPage(this, "被融合精灵品级过低,无法融合!", okUrl);
            }
            if (fusGeine.start < data.start)
            {
                return MessageHelper.MsgPage(this, "被融合精灵星级不能小于当前精灵星级!", okUrl);
            }
            //判断是否在编队
            var teamInfo = await teamService.GetUserTeam(uid);
            if (!string.IsNullOrEmpty(teamInfo.team))
            {
                var team = JsonConvert.DeserializeObject<List<TeamTemp>>(teamInfo.team);
                if (team.Any(it => it.id == fgu))
                {
                    return MessageHelper.MsgPage(this, "被融合的精灵已被编队,请先从编队中移除!", okUrl);
                }
            }

            //开始执行融合
            if (await genieService.DelUserGenie(fgu))
            {
                data.start = data.start + 1;
                if (await genieService.UpdateUserGenie(data))
                {
                    string thing = string.Format("成功融合【精灵·{0}】的星级至{1}星!", data.name, data.start);
                    await thingService.AddGenieThing(uid, thing);

                    return MessageHelper.MsgPage(this, "融合成功，星级+1!", okUrl);
                }
                else
                {
                    return MessageHelper.MsgPage(this, "升星失败，请稍后尝试!", okUrl);
                }
            }
            else
            {
                return MessageHelper.MsgPage(this, "升星失败，请稍后尝试!", url);
            }
        }
    }
}