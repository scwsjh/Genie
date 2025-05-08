using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public class GenieAccService : IGenieAccService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public GenieAccService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public async Task<genie_acc> GetAccInfo(int userId)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_acc>();
            return await db.Queryable<genie_acc>().Where(it => it.userId == userId).SingleAsync();
        }

        public async Task<bool> UpdateUserAcc(int userId, int op, string type, int count, string remark)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_acc>();
            int opCount = op == 0 ? (0 - count) : count;
            bool result = await db.Updateable<genie_acc>()
                 .SetColumnsIF(type == AccEnum.AccType.copper.ToString(), it => it.copper == it.copper + opCount)
                 .SetColumnsIF(type == AccEnum.AccType.exploit.ToString(), it => it.exploit == it.exploit + opCount)
                 .Where(it => it.userId == userId).ExecuteCommandAsync() > 0;
            if (result)
            { }
            return result;
        }

        public async Task<List<genie_acc>> GetAccRankByExploit(int page, int pageSize, RefAsync<int> total)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_acc>();
            return await db.Queryable<genie_acc>().Where(it => it.exploit > 0)
                .OrderByDescending(it => it.exploit)
                .ToPageListAsync(page, pageSize, total);
        }

        public async Task<List<ConsumeTemp>> GetConsumeInfoBySite(int type, int time, int page, int pageSize, RefAsync<int> total)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_consume>();
            DateTime now = TimeHelper.GetDateTimeYMD(0);
            DateTime end = TimeHelper.GetDateTimeYMD(1);
            return await db.Queryable<genie_consume>().GroupBy(it => new { it.userId })
                .Where(it => it.type == type)
                .WhereIF(time == 0, it => it.addTime > now && it.addTime < end)
                .Select<ConsumeTemp>(it => new ConsumeTemp
                {
                    userId = (int)it.userId,
                    amount = SqlFunc.AggregateSum((decimal)it.amount
)
                }).MergeTable().OrderByDescending(it => it.amount).ToPageListAsync(page, pageSize, total);
        }
    }
}