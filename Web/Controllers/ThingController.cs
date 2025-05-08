using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ThingController : GameController
    {
        private readonly IGenieThingService thingService;

        public ThingController(IGenieThingService thingService)
        {
            this.thingService = thingService;
        }

        public async Task<IActionResult> Index(int u)
        {
            ViewBag.Title = "精灵动态";
            int PageIndex = StringHelper.ConvertPage(Request.Query["p"]);
            int PageSize = 10;
            RefAsync<int> Total = 0;
            var data = await thingService.GetGenieThing(PageIndex, PageSize, Total, u);
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.Total = Total;
            return View(data);
        }
    }
}