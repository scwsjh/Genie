using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public class GenieCallService : IGenieCallService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public GenieCallService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public async Task<List<genie_call>> GetCallData()
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_call>();
            return await db.Queryable<genie_call>().ToListAsync();
        }

        public async Task<genie_call> GetCallInfo(int id)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_call>();
            return await db.Queryable<genie_call>().Where(it => it.id == id).SingleAsync();
        }

        public async Task<bool> SaveCall(genie_call data)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_call>();
            return await db.Storageable(data).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> DelCall(int id)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_call>();
            return await db.Deleteable<genie_call>().Where(it => it.id == id).ExecuteCommandAsync() > 0;
        }

        public async Task<double> GetUserCallCoolTime(int uid, int call)
        {
            double result = 0;
            string id = string.Format("{0}_{1}", uid, call);
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_call_log>();
            var data = await db.Queryable<genie_call_log>().Where(it => it.id == id).SingleAsync();
            if (data != null)
            {
                if (data.eTime > DateTime.Now)
                {
                    result = TimeHelper.TimeDiffSeconds(DateTime.Now, (DateTime)data.eTime);
                }
            }
            return result;
        }

        public async Task<bool> UpdateUserCallTime(int uid, int call, int time)
        {
            bool result = false;
            string id = string.Format("{0}_{1}", uid, call);
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_call_log>();
            var data = await db.Queryable<genie_call_log>().Where(it => it.id == id).SingleAsync();
            if (data == null)
            {
                data = new genie_call_log();
                data.id = id;
                data.sTime = DateTime.Now;
                data.eTime = DateTime.Now.AddMinutes(time);
                result = await db.Insertable(data).ExecuteCommandAsync() > 0;
            }
            else
            {
                data.sTime = DateTime.Now;
                data.eTime = DateTime.Now.AddMinutes(time);
                result = await db.Updateable(data).ExecuteCommandAsync() > 0;
            }
            return result;
        }
    }
}