using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public class GenieFightService : IGenieFightService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public GenieFightService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public async Task<bool> AddFight(string id, int uid, int otid, int winId, int lowId, string atk, string def, string log, string type, string award)
        {
            genie_fight fight = new genie_fight();
            fight.id = id;
            fight.uid = uid;
            fight.otId = otid;
            fight.winId = winId;
            fight.lowId = lowId;
            fight.atk = atk;
            fight.def = def;
            fight.log = log;
            fight.award = award;
            fight.type = type;
            fight.addTime = DateTime.Now;
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_fight>();
            return await db.Insertable(fight).ExecuteCommandAsync() > 0;
        }

        public async Task<genie_fight> GetFightInfo(string id)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_fight>();
            return await db.Queryable<genie_fight>().Where(it => it.id == id).SingleAsync();
        }
    }
}