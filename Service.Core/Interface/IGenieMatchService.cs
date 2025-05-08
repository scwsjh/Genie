using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public interface IGenieMatchService
    {
        Task<genie_match> GetUserMatch(int uid);

        Task<genie_match> GetUserMatchByRank(int rank);

        Task<List<genie_match>> GetMatchUser(int rank);

        Task UpdateMatchRankUser(int id, int uid);

        Task<bool> CheakIsGetAward(int uid);

        Task<bool> UpdateUserAwardLog(int uid);
    }
}