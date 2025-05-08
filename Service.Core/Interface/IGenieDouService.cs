using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public interface IGenieDouService
    {
        Task<genie_dou> GetDouInfo(int uid);

        Task<bool> RestDouInfo(int uid);

        Task<bool> UpdateDouInfo(genie_dou info);
    }
}