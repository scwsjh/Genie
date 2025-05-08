/*****************************************************************************
    *  @COPYRIGHT NOTICE
    *  @Copyright (c) 2021, kexun
    *  @All rights reserved
    *  @file	 : LockFilter.cs
    *  @version  : ver 1.0
    *  @author   : hu
    *  @date     : 2021/6/11 9:01
    *  @brief    : 锁过滤器
*****************************************************************************/

using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Service.Pulse
{
    public class LockFilter : IActionFilter
    {
        private readonly IOperateLock operateLock;
        public string code { get; set; } //自主传参（如果需要）
        public int second { get; set; } //锁定时间,0的话不限制
        private readonly IRedisCache redis;

        public LockFilter(IOperateLock _operateLock, IRedisCache _redis, int _second = 60, string _code = "")
        {
            operateLock = _operateLock;
            code = _code;
            second = _second;
            redis = _redis;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            string sid = context.HttpContext.Request.Query["sid"];
            string lockCode = "";
            if (string.IsNullOrEmpty(code))
            {
                lockCode = string.Format("ActionFilter:User:{0}", sid);
            }
            else
            {
                lockCode = string.Format("ActionFilter:{1}:{0}", sid, code);
            }
            operateLock.UnLock(lockCode).GetAwaiter().GetResult();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string sid = context.HttpContext.Request.Query["sid"];
            string lockCode = "";
            if (string.IsNullOrEmpty(code))
            {
                lockCode = string.Format("ActionFilter:User:{0}", sid);
            }
            else
            {
                lockCode = string.Format("ActionFilter:{1}:{0}", sid, code);
            }
            if (!operateLock.CheakLock(lockCode, second).GetAwaiter().GetResult())
            {
                ResultData result = new ResultData();
                result.SetValue("title", "提示");
                result.SetValue("msg", "请勿重复操作!");
                result.SetValue("url", "/Index");
                result.SetValue("button", "返回主页");
                result.SetValue("isToUrl", true);

                string key = string.Format("GAME_MSG:UNIT:{0}", sid);
                redis.SetAsync(key, result.ToJson()).GetAwaiter().GetResult();
                string retSid = string.IsNullOrEmpty(sid) ? context.HttpContext.Request.Query["sid"] : sid;
                context.Result = new RedirectResult("/Msg/Index?sid=" + retSid);
                return;
            }
            return;
        }
    }
}