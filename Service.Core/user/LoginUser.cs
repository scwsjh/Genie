using Microsoft.AspNetCore.Http;
using SiteBridg.Model;
using SiteBridg.Service;

namespace Service.Core
{
    public class LoginUser : ILoginUser, IScoped
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWapUserService wapUserService;
        private wap_user userInfo = null;

        public LoginUser(IHttpContextAccessor httpContextAccessor, IWapUserService wapUserService)
        {
            _httpContextAccessor = httpContextAccessor;
            this.wapUserService = wapUserService;

            userInfo = wapUserService.GetUserInfoBySidSync(OnLineSid);
        }

        public bool IsLogin
        {
            get
            {
                if (userInfo == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public string userId
        {
            get { return userInfo != null ? userInfo.id.ToString() : "0"; }
        }

        public int userNo
        {
            get { return userInfo != null ? userInfo.id : 0; }
        }

        public string nick
        {
            get { return userInfo != null ? userInfo.name : ""; }
        }

        public int status
        {
            get { return userInfo != null ? (int)userInfo.status : 1; }
        }

        public string OnLineSid
        {
            get => _httpContextAccessor.HttpContext.Request.Query["sid"].ToString() ?? "0";
        }
    }
}