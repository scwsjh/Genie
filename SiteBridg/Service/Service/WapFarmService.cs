using Furion.DependencyInjection;
using Service.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteBridg.Service
{
    public class WapFarmService : IWapFarmService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public WapFarmService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public async Task<bool> UpdateFramExp(int uid, int op, int count)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_farm>();
            return await db.Updateable<wap_farm>()
                .SetColumnsIF(op == 1, it => it.point == it.point + count)
                .SetColumnsIF(op != 1, it => it.point == it.point - count)
                .Where(it => it.uid == uid).ExecuteCommandAsync() > 0;
        }

        private async Task<string> GetPropName(int dtype, int id)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_farm>();
            string name = string.Empty;
            if (dtype == 1 || dtype == 11)
            {
                var data = await db.Queryable<wap_farm_seed>().Where(it => it.id == id).SingleAsync();
                if (data != null)
                {
                    name = data.name;
                }
            }
            else if (dtype == 2)
            {
                var data = await db.Queryable<wap_farm_muck>().Where(it => it.id == id).SingleAsync();
                if (data != null)
                {
                    name = data.name;
                }
            }
            else if (dtype == 3)
            {
                var data = await db.Queryable<wap_farm_trap>().Where(it => it.id == id).SingleAsync();
                if (data != null)
                {
                    name = data.name;
                }
            }
            return name;
        }

        public async Task<bool> UpdateFarmBag(int uid, int dtype, int op, int par, int count)
        {
            bool result = false;
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_farm_bag>();
            var data = await db.Queryable<wap_farm_bag>().Where(it => it.uid == uid && it.dtype == dtype && it.oid == par).FirstAsync();
            if (op == 1)
            {
                if (data == null)
                {
                    data = new wap_farm_bag();
                    data.uid = uid;
                    data.oid = par;
                    data.name = await GetPropName(dtype, par);
                    data.dtype = dtype;
                    data.amount = count;
                    result = await db.Insertable(data).ExecuteCommandAsync() > 0;
                }
                else
                {
                    data.amount = data.amount + count;
                    result = await db.Updateable(data).ExecuteCommandAsync() > 0;
                }
            }
            else
            {
                if (data != null)
                {
                    data.amount = data.amount - count;
                    result = await db.Updateable(data).ExecuteCommandAsync() > 0;
                }
            }

            return result;
        }

        public async Task<int> GetUserFarmPropCount(int uid, int dtype, int par)
        {
            int result = 0;
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_farm_bag>();
            var data = await db.Queryable<wap_farm_bag>().Where(it => it.uid == uid && it.dtype == dtype && it.oid == par).SingleAsync();
            if (data != null)
            {
                result = (int)data.amount;
            }
            return result;
        }
    }
}