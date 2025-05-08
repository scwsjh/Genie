using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Pulse
{
    public class MyAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler
        DefaultHandler = new AuthorizationMiddlewareResultHandler();

        public async Task HandleAsync(
            RequestDelegate requestDelegate,
            HttpContext httpContext,
            AuthorizationPolicy authorizationPolicy,
            PolicyAuthorizationResult policyAuthorizationResult)
        {
            //if (Show404ForForbiddenResult(policyAuthorizationResult))
            //{
            //    // Return a 404 to make it appear as if the resource does not exist.
            //    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            //    return;
            //}
            var data = authorizationPolicy.AuthenticationSchemes;
            foreach (var item in data)
            {
                if (item.Equals("Login"))
                {
                    httpContext.Response.StatusCode = 40002;
                    return;
                }
            }

            // Fallback to the default implementation.
            await DefaultHandler.HandleAsync(requestDelegate, httpContext, authorizationPolicy,
                           policyAuthorizationResult);
        }

        private bool Show404ForForbiddenResult(PolicyAuthorizationResult policyAuthorizationResult)
        {
            return policyAuthorizationResult.Forbidden &&
                policyAuthorizationResult.AuthorizationFailure.FailedRequirements.OfType<
                                                               Show404Requirement>().Any();
        }
    }

    public class Show404Requirement : IAuthorizationRequirement
    { }
}