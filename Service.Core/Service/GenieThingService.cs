using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public class GenieThingService : IGenieThingService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public GenieThingService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public async Task<List<genie_thing>> GetGenieThing(int page, int pageSize, RefAsync<int> total, int uid = 0)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_thing>();
            var data = await db.Queryable<genie_thing>()
                .WhereIF(uid != 0, it => it.uid == uid)
                .OrderBy(it => it.addTime, OrderByType.Desc)
                .ToPageListAsync(page, pageSize, total);
            return data;
        }

        public async Task<List<genie_thing>> GetGenieThing(string type, int task)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_thing>();
            var data = await db.Queryable<genie_thing>()
         .Where(it => it.type == type)
                .OrderBy(it => it.addTime, OrderByType.Desc).Take(task).ToListAsync();
            return data;
        }

        public async Task AddGenieThing(int uid, string sign, string type = "Def")
        {
            genie_thing thing = new genie_thing();
            thing.id = StringHelper.NewGuid;
            thing.uid = uid;
            thing.sign = sign;
            thing.addTime = DateTime.Now;
            thing.type = type;
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_thing>();
            bool isok = await db.Insertable(thing).ExecuteCommandAsync() > 0;
            if (isok)
            {
            }
        }
    }
}