using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public class GenieDouService : IGenieDouService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public GenieDouService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public async Task<genie_dou> GetDouInfo(int uid)
        {
            Console.WriteLine(uid);
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_dou>();
            var data = await db.Queryable<genie_dou>().Where(it => it.userId == uid).SingleAsync();
            string timeNo = TimeHelper.getDateTimeNumYMD;
            if (data == null)
            {
                data = new genie_dou();
                data.userId = uid;
                data.users = JsonConvert.SerializeObject(await GetRandomUser(uid));
                data.upTime = timeNo;
                await db.Insertable(data).ExecuteCommandAsync();
            }
            else if (data.upTime != timeNo)
            {
                data.users = JsonConvert.SerializeObject(await GetRandomUser(uid));
                data.upTime = timeNo;
                await db.Updateable(data).ExecuteCommandAsync();
            }
            return data;
        }

        private async Task<List<DouTemp>> GetRandomUser(int uid)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user>();
            var data = await db.Queryable<genie_user>().Where(it => it.uid != uid).Take(10).OrderBy(it => SqlFunc.GetRandom()).ToListAsync();
            List<DouTemp> result = new List<DouTemp>();
            foreach (var item in data)
            {
                DouTemp temp = new DouTemp();
                temp.userId = item.uid;
                temp.status = 0;
                result.Add(temp);
            }
            return result;
        }

        public async Task<bool> RestDouInfo(int uid)
        {
            var data = await GetDouInfo(uid);
            data.users = JsonConvert.SerializeObject(await GetRandomUser(uid));
            data.upTime = TimeHelper.getDateTimeNumYMD;
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_dou>();
            return await db.Updateable(data).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> UpdateDouInfo(genie_dou info)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_dou>();
            return await db.Updateable(info).ExecuteCommandAsync() > 0;
        }
    }
}