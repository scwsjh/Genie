using Microsoft.AspNetCore.Authorization;

namespace Service.Pulse
{
    public class PolicyOperate : IAuthorizationRequirement
    {
        private string _operateType;

        public PolicyOperate(string opType = "Login")
        {
            _operateType = opType;
        }

        public string Operate
        {
            get => _operateType;
        }
    }
}