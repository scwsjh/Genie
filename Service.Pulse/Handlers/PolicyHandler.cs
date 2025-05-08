using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Service.Pulse
{
    public class PolicyHandler : AuthorizationHandler<PolicyOperate>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyOperate requirement)
        {
            //if (requirement.Operate == PolicyEnum.PlolicyLogin)
            //{
            //    var _loginUser = App.GetService<ILoginUser>();
            //    if (!_loginUser.IsLogin)
            //    {
            //        return Task.CompletedTask;
            //    }
            //}

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}