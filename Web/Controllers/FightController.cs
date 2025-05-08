using Microsoft.AspNetCore.Mvc;
using Service.Core;
using System.Text.RegularExpressions;

namespace Web.Controllers
{
    public class FightController : GameController
    {
        private readonly IGenieFightService fightService;
        private readonly IGenieUserService userService;
        private readonly IGenieAttrService attrService;
        private readonly IGenieThingService thingService;
        private readonly IGenieChatService chatService;
        private readonly IWapUserService wapUserService;
        private readonly IGenieAccService accService;

        public FightController(IGenieFightService fightService, IGenieUserService userService, IGenieAttrService attrService, IGenieThingService thingService, IGenieChatService chatService, IWapUserService wapUserService, IGenieAccService accService)
        {
            this.fightService = fightService;
            this.userService = userService;
            this.attrService = attrService;
            this.thingService = thingService;
            this.chatService = chatService;
            this.wapUserService = wapUserService;
            this.accService = accService;
        }

        [HttpGet]
        public async Task<IActionResult> FightUser(int id)
        {
            string url = $"/Index/Home/{id}";
            if (!await PageHelper.CheakPageToken(this))
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            int uid = StateHelper.userNo(this);
            if (uid == id)
            {
                return MessageHelper.Msg(this, "提示", "对自己就下手轻一点!", url);
            }
            var otUser = await userService.GetGenieUserInfo(id);
            if (otUser == null)
            {
                return Redirect(UnitTool.UrlToSid(url, ViewBag.MySid));
            }
            if (otUser.poin < 10)
            {
                return MessageHelper.Msg(this, "提示", "Ta的精灵坞已破败,系统保护中!", url);
            }

            var genieUser = await userService.GetGenieUserInfo(uid);
            if (genieUser.poin < GameConfig.MaxPoin)
            {
                return MessageHelper.Msg(this, "提示", "精灵坞正在恢复中,无法挑战!", url);
            }
            if (otUser.lev < (genieUser.lev - 5))
            {
                return MessageHelper.Msg(this, "提示", "Ta还太弱小,换个人挑战吧!", url);
            }
            //判断元气
            var vigorInfo = await userService.GetUserVigor(uid);
            if (vigorInfo.vigor < 1)
            {
                return MessageHelper.Msg(this, "提示", "元气不足,恢复一下吧!", url);
            }
            FightTemp atk = await attrService.GetUserFightTemp(uid);
            if (atk.genie.Count == 0)
            {
                return MessageHelper.Msg(this, "提示", "先进行编队,再来挑战吧!", "/Team/Index");
            }
            if (await userService.UpdateUserVigor(uid, 0, 1))
            {
                FightTemp def = await attrService.GetUserFightTemp((int)otUser.uid, FightEnum.Camp.Def.ToString());
                var fight = await FightCompute.Compute(atk, def);
                if (fight.isOk)
                {
                    var atkUser = await wapUserService.GetUserInfo(uid);
                    var defUser = await wapUserService.GetUserInfo(otUser.uid);
                    if (fight.winId == uid)
                    {
                        //更新对方繁荣度
                        await userService.UpdateUserPoin(otUser.uid, 0, GameConfig.PoinByPk);

                        string chatTips = $"精灵坞温馨值-{GameConfig.PoinByPk},";

                        var accInfo = await accService.GetAccInfo(otUser.uid);
                        if (accInfo.exploit > 0)
                        {
                            if (await accService.UpdateUserAcc(otUser.uid, 0, AccEnum.AccType.exploit.ToString(), 1, "攻击"))
                            {
                                await accService.UpdateUserAcc(uid, 1, AccEnum.AccType.exploit.ToString(), 1, "攻击");
                                chatTips += "功勋值-1";
                            }
                        }
                        chatTips = chatTips.TrimEnd(',');
                        //事件
                        string things = string.Format("对[{0}]发起了攻击并取得最终胜利！{1}[{2}]", GameTool.HomeUbb(otUser.uid, defUser.name), chatTips, GameTool.FightUbb(fight.fightId));
                        await thingService.AddGenieThing(uid, things);
                        //生成通知事件
                        string chatMsg = string.Format("{0}对你发起了攻击,并成功击败了你;{1}[{2}]", GameTool.HomeUbb(genieUser.uid, atkUser.name), chatTips, GameTool.FightUbb(fight.fightId));
                        await chatService.AddChat(0, "", otUser.uid, ChatEnum.code.User.ToString(), chatMsg);
                    }
                    else
                    {
                        string things = string.Format("对[{0}]发起了攻击,被对方击败,再接再厉哦！[{1}]", GameTool.HomeUbb(otUser.uid, defUser.name), GameTool.FightUbb(fight.fightId));
                        await thingService.AddGenieThing(uid, things);

                        //生成通知事件
                        string chatMsg = string.Format("恭喜,成功抵御了{0}的攻击.[{1}]", GameTool.HomeUbb(genieUser.uid, atkUser.name), GameTool.FightUbb(fight.fightId));
                        await chatService.AddChat(0, "", otUser.uid, ChatEnum.code.User.ToString(), chatMsg);
                    }
                    return Redirect(UnitTool.UrlToSid(string.Format("/Fight/FightLog?log={0}", fight.fightId), ViewBag.MySid));
                }
                else
                {
                    return MessageHelper.Msg(this, "争霸赛", "挑战失败,请稍后尝试!", url);
                }
            }
            else
            {
                return MessageHelper.Msg(this, "争霸赛", "挑战失败,请稍后尝试!", url);
            }
        }

        public async Task<IActionResult> FightLog(string log, int t)
        {
            ViewBag.Title = "战况";
            var logInfo = await fightService.GetFightInfo(log);
            if (logInfo == null)
            {
                return Redirect(UnitTool.UrlToSid("/Index/Index", ViewBag.MySid));
            }
            ViewBag.Type = t;
            return View(logInfo);
        }
    }
}