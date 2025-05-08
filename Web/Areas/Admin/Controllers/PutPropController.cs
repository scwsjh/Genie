using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PutPropController : AdminController
    {
        private readonly IWapUserService userService;
        private readonly IGenieChatService chatService;
        private readonly IGenieGoodsService goodsService;

        public PutPropController(IWapUserService userService, IGenieChatService chatService, IGenieGoodsService goodsService)
        {
            this.userService = userService;
            this.chatService = chatService;
            this.goodsService = goodsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PutOk(string id, string name, string sign, string awData, int goods, int count)
        {
            //奖励处理
            List<TowerGet> award = new List<TowerGet>();
            if (string.IsNullOrEmpty(awData))
            {
                if (goods != 0)
                {
                    var goodsInfo = await goodsService.GetGoodsInfo(goods);
                    if (goodsInfo == null)
                    {
                        return MessageHelper.MsgPage(this, "物品不存在!", "/Admin/PutProp/Index");
                    }
                    TowerGet add = new TowerGet();
                    add.code = GameEnum.PropCode.Goods.ToString();
                    add.name = goodsInfo.goodsName;
                    add.parameter = goods.ToString();
                    add.count = count;
                    award.Add(add);
                }
            }
            else
            {
                award = JsonUtil.DeserializeJsonToList<TowerGet>(awData);
            }
            List<int> users = new List<int>();
            string[] ids = id.Split(',');
            foreach (string item in ids)
            {
                var userInfo = await userService.GetUserInfo(Convert.ToInt32(item.Trim()));
                if (userInfo == null)
                {
                    continue;
                }
                users.Add(userInfo.id);
            }
            //开始发放
            int okCount = 0;
            string awardTips = string.Empty;
            if (award.Count > 0)
            {
                awardTips = "获得：" + GameTool.GetPropTips(award);
            }
            string chatMsg = $"【{name}】{sign},{awardTips}";
            foreach (var item in users)
            {
                //发送消息
                if (award.Count > 0)
                {
                    await GameTool.PutPropBatch(item, 1, award, "后台发放");
                }

                await chatService.AddChat(0, "", item, ChatEnum.code.User.ToString(), chatMsg);
                okCount++;
            }

            return MessageHelper.MsgPage(this, "发放成功!", "/Admin/PutProp/Index");
        }
    }
}