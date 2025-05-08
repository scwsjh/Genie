using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteBridg.Service
{
    public interface IWapRanchService
    {
        Task<bool> UpdateRanchExp(int uid, int op, int count);

        Task<bool> UpdateRanchBag(int uid, int dtype, int op, int par, int count);

        Task<int> GetUserRanchPropCount(int uid, int dtype, int par);
    }
}