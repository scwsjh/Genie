using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public interface IGenieAccService
    {
        Task<genie_acc> GetAccInfo(int userId);

        Task<bool> UpdateUserAcc(int userId, int op, string type, int count, string remark);

        Task<List<genie_acc>> GetAccRankByExploit(int page, int pageSize, RefAsync<int> total);

        Task<List<ConsumeTemp>> GetConsumeInfoBySite(int type, int time, int page, int pageSize, RefAsync<int> total);
    }
}