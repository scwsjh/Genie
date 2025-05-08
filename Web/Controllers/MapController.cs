using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class MapController : GameController
    {
        public readonly IGenieService genieService;
        private readonly IGenieThingService thingService;

        public MapController(IGenieService genieService, IGenieThingService thingService)
        {
            this.genieService = genieService;
            this.thingService = thingService;
        }

        public async Task<IActionResult> Index(int t)
        {
            ViewBag.Title = "精灵图鉴";
            t = t == 0 ? 1 : t;
            int PageIndex = StringHelper.ConvertPage(Request.Query["p"]);
            int PageSize = 10;
            RefAsync<int> Total = 0;
            var data = await genieService.GetGenieData(t, PageIndex, PageSize, Total);
            int uid = StateHelper.userNo(this);
            //处理
            List<GenieMapTemp> result = new List<GenieMapTemp>();
            foreach (var item in data)
            {
                GenieMapTemp temp = new GenieMapTemp();
                temp.genie = item;
                temp.isHave = await genieService.GetGenieIsHave(uid, item.genieId);
                result.Add(temp);
            }

            ViewBag.Type = t;
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.Total = Total;
            return View(result);
        }

        public async Task<IActionResult> Info(int id)
        {
            var info = await genieService.GetGenieInfo(id);
            if (info == null)
            {
                return Redirect(UnitTool.UrlToSid("/Map/Index", ViewBag.MySid));
            }
            int uid = StateHelper.userNo(this);
            var onMap = await genieService.GetGenieMap(uid, id);
            if (onMap == null)
            {
                ViewBag.IsHave = false;
            }
            else
            {
                ViewBag.IsHave = true;
                ViewBag.isGet = onMap.isGet;
            }

            ViewBag.Award = JsonConvert.DeserializeObject<List<TowerGet>>(info.mapAward);
            return View(info);
        }

        public async Task<IActionResult> Skill(int gen)
        {
            var info = await genieService.GetGenieInfo(gen);
            if (info == null)
            {
                return Redirect(UnitTool.UrlToSid("/Map/Index", ViewBag.MySid));
            }
            SkillItem skill = JsonConvert.DeserializeObject<SkillItem>(info.skill);
            ViewBag.Skill = skill;
            return View(info);
        }

        public async Task<IActionResult> GetAward(int id)
        {
            string url = $"/Map/Info/{id}";
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            int uid = StateHelper.userNo(this);
            var data = await genieService.GetGenieMap(uid, id);
            if (data == null)
            {
                return MessageHelper.Msg(this, "领取奖励", "暂未点亮图鉴!", url);
            }
            if (data.isGet != 0)
            {
                return MessageHelper.Msg(this, "领取奖励", "奖励已领取过啦!", url);
            }
            var genInfo = await genieService.GetGenieInfo(id);
            var award = JsonConvert.DeserializeObject<List<TowerGet>>(genInfo.mapAward);
            if (award.Count == 0)
            {
                return MessageHelper.Msg(this, "领取奖励", "该图鉴暂无奖励!", url);
            }
            if (await genieService.UpdateMapAwardStatus(uid, id, 1))
            {
                if (await GameTool.PutPropBatch(uid, 1, award, "图鉴奖励"))
                {
                    string goodsStr = GameTool.GetPropTips(award, 0);
                    string thing = $"领取图鉴【{genInfo.name}】奖励,获得:{goodsStr}!";
                    await thingService.AddGenieThing(uid, thing);
                    return MessageHelper.Msg(this, "图鉴奖励", $"领取成功,获得:{goodsStr}", url);
                }
                else
                {
                    return MessageHelper.Msg(this, "领取奖励", "领取失败,请稍后尝试!", url);
                }
            }
            else
            {
                return MessageHelper.Msg(this, "领取奖励", "领取失败,请稍后尝试!", url);
            }
        }
    }
}