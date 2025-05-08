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
    public class WapRanchService : IWapRanchService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public WapRanchService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public async Task<bool> UpdateRanchExp(int uid, int op, int count)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_ygw>();
            return await db.Updateable<wap_ygw>()
                .SetColumnsIF(op == 1, it => it.point == it.point + count)
                .SetColumnsIF(op != 1, it => it.point == it.point - count)
                .Where(it => it.uid == uid).ExecuteCommandAsync() > 0;
        }

        private async Task<string> GetPropName(int dtype, int id)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_ygw>();
            string name = string.Empty;
            if (dtype == 1 || dtype == 11)
            {
                var data = await db.Queryable<wap_ygw_seed>().Where(it => it.ID == id).SingleAsync();
                if (data != null)
                {
                    name = data.name;
                }
            }
            else if (dtype == 2)
            {
                var data = await db.Queryable<wap_ygw_muck>().Where(it => it.id == id).SingleAsync();
                if (data != null)
                {
                    name = data.name;
                }
            }
            return name;
        }

        public async Task<bool> UpdateRanchBag(int uid, int dtype, int op, int par, int count)
        {
            bool result = false;
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_ygw_bag>();
            var data = await db.Queryable<wap_ygw_bag>().Where(it => it.uid == uid && it.dtype == dtype && it.oid == par).FirstAsync();
            if (op == 1)
            {
                if (data == null)
                {
                    data = new wap_ygw_bag();
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

        public async Task<int> GetUserRanchPropCount(int uid, int dtype, int par)
        {
            int result = 0;
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_ygw_bag>();
            var data = await db.Queryable<wap_ygw_bag>().Where(it => it.uid == uid && it.dtype == dtype && it.oid == par).SingleAsync();
            if (data != null)
            {
                result = (int)data.amount;
            }
            return result;
        }
    }
}