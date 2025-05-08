using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public interface IGenieTeamService
    {
        Task<genie_team> GetUserTeam(int uid);

        Task<bool> UpdateTeam(genie_team team);
    }
}