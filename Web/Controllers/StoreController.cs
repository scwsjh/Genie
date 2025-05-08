using Microsoft.AspNetCore.Mvc;
using TencentCloud.Ame.V20190916.Models;

namespace Web.Controllers
{
    public class StoreController : GameController
    {
        private readonly IGenieMallService mallService;
        private readonly IWapUserService userService;

        public StoreController(IGenieMallService mallService, IWapUserService userService)
        {
            this.mallService = mallService;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await mallService.GetGameMallList();
            ViewBag.PageToken = await PageHelper.SetPageToken(this);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(string g, int count)
        {
            count = count < 1 ? 1 : count;
            var mallInfo = await mallService.GetMallInfo(g);
            if (mallInfo == null)
            {
                return MessageHelper.Msg(this, "商店", "物品不存在!", "/Store/Index");
            }
            if (mallInfo.addTime > DateTime.Now || mallInfo.endTime < DateTime.Now)
            {
                return MessageHelper.Msg(this, "商店", "物品不存在!", "/Store/Index");
            }
            int uid = StateHelper.userNo(this);
            var need = mallInfo.price * count;
            bool isOk = false;
            if (mallInfo.payType.Contains("Site"))
            {
                string[] pars = mallInfo.payType.Split('#');
                int myAcc = await userService.GetUserMoneyCount(uid, Convert.ToInt32(pars[1]));
                if (myAcc < need)
                {
                    return MessageHelper.Msg(this, "商店", "余额不足!", "/Store/Index");
                }
                isOk = await userService.UpdateUserMoney(uid, Convert.ToInt32(pars[1]), 0, Convert.ToInt32(need), "商城购买", "精灵商城购买", "");
            }

            if (isOk)
            {
                if (await GameTool.UpdateBag(uid, 1, mallInfo.type, mallInfo.goodsId, count, "商城购买"))
                {
                    return MessageHelper.Msg(this, "商店", $"成功购买[{mallInfo.name}]×{count}!", "/Store/Index");
                }
                else
                {
                    return MessageHelper.Msg(this, "商店", "购买失败,请稍后尝试!", "/Store/Index");
                }
            }
            else
            {
                return MessageHelper.Msg(this, "商店", "购买失败,请稍后尝试!", "/Store/Index");
            }
        }
    }
}