using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public interface IGenieCallService
    {
        Task<List<genie_call>> GetCallData();

        Task<genie_call> GetCallInfo(int id);

        Task<bool> SaveCall(genie_call data);

        Task<bool> DelCall(int id);

        Task<double> GetUserCallCoolTime(int uid, int call);

        Task<bool> UpdateUserCallTime(int uid, int call, int time);
    }
}