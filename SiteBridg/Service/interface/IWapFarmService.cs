using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteBridg.Service
{
    public interface IWapFarmService
    {
        Task<bool> UpdateFramExp(int uid, int op, int count);

        Task<bool> UpdateFarmBag(int uid, int dtype, int op, int par, int count);

        Task<int> GetUserFarmPropCount(int uid, int dtype, int par);
    }
}