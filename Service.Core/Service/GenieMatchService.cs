using Furion.DatabaseAccessor;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public class GenieMatchService : IGenieMatchService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public GenieMatchService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public async Task<genie_match> GetUserMatch(int uid)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_match>();
            var data = await db.Queryable<genie_match>().Where(it => it.uid == uid).SingleAsync();
            if (data == null)
            {
                data = new genie_match();
                data.uid = uid;
                data = await db.Insertable(data).ExecuteReturnEntityAsync();
            }
            return data;
        }

        public async Task<genie_match> GetUserMatchByRank(int rank)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_match>();
            return await db.Queryable<genie_match>().Where(it => it.id == rank).SingleAsync();
        }

        public async Task<List<genie_match>> GetMatchUser(int rank)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_match>();
            return await db.Queryable<genie_match>().Where(it => it.id <= rank).OrderBy(it => it.id, OrderByType.Desc).Take(10).ToListAsync();
        }

        public async Task UpdateMatchRankUser(int id, int uid)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_match>();
            await db.Updateable<genie_match>().SetColumns(it => it.uid == uid).Where(it => it.id == id).ExecuteCommandAsync();
        }

        public async Task<bool> CheakIsGetAward(int uid)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_match_log>();
            var data = await db.Queryable<genie_match_log>().Where(it => it.userId == uid).SingleAsync();
            if (data == null)
            {
                return false;
            }
            else
            {
                string time = TimeHelper.getDateTimeNumYMD;
                return data.time == time;
            }
        }

        public async Task<bool> UpdateUserAwardLog(int uid)
        {
            bool result = false;
            string time = TimeHelper.getDateTimeNumYMD;
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_match_log>();
            var data = await db.Queryable<genie_match_log>().Where(it => it.userId == uid).SingleAsync();
            if (data == null)
            {
                data = new genie_match_log();
                data.userId = uid;
                data.time = time;
                result = await db.Insertable(data).ExecuteCommandAsync() > 0;
            }
            else
            {
                data.time = time;
                result = await db.Updateable(data).ExecuteCommandAsync() > 0;
            }
            return result;
        }
    }
}