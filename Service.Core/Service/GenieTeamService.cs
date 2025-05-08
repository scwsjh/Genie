using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public class GenieTeamService : IGenieTeamService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public GenieTeamService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public async Task<genie_team> GetUserTeam(int uid)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_team>();
            var data = await db.Queryable<genie_team>().Where(it => it.uid == uid).SingleAsync();
            if (data == null)
            {
                data = new genie_team();
                data.uid = uid;
                data.team = "";
                await db.Insertable(data).ExecuteCommandAsync();
            }
            return data;
        }

        public async Task<bool> UpdateTeam(genie_team team)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_team>();
            return await db.Updateable(team).ExecuteCommandAsync() > 0;
        }
    }
}