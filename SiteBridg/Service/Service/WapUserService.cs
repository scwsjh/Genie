using Common;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Service.Enties;
using SiteBridg.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SiteBridg.Service
{
    public class WapUserService : IWapUserService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public WapUserService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public async Task<wap_user> GetUserInfo(int id)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_user>();
            return await db.Queryable<wap_user>().Where(it => it.id == id).SingleAsync();
        }

        public async Task<wap_user> GetUserInfoBySid(string sid)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_user>();
            return await db.Queryable<wap_user>().Where(it => it.sid == sid).SingleAsync();
        }

        public wap_user GetUserInfoBySidSync(string sid)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_user>();
            return db.Queryable<wap_user>().Where(it => it.sid == sid).Single();
        }

        #region 货币操作

        public async Task<List<wap_money>> GetMoneyData()
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_money>();
            return await db.Queryable<wap_money>().ToListAsync();
        }

        public async Task<wap_money> GetMoneyInfo(int id)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_money>();
            return await db.Queryable<wap_money>().Where(it => it.id == id).SingleAsync();
        }

        public async Task<wap_user_money> GetUserMoneyInfo(int uid)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_user_money>();
            return await db.Queryable<wap_user_money>().Where(it => it.uid == uid).SingleAsync();
        }

        public async Task<int> GetUserMoneyCount(int uid, int money)
        {
            int result = 0;
            var info = await GetUserMoneyInfo(uid);
            if (info != null)
            {
                switch (money)
                {
                    case 0:
                        result = (int)info.mon00;
                        break;

                    case 1:
                        result = (int)info.mon10;
                        break;

                    case 2:
                        result = (int)info.mon20;
                        break;
                }
            }
            return result;
        }

        public async Task<bool> UpdateUserMoney(int uid, int money, int op, int count, string name, string remark, string logs)
        {
            bool result = false;
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_user_money>();
            int onBalance = 0;
            var data = await db.Queryable<wap_user_money>().Where(it => it.uid == uid).SingleAsync();
            if (money == 0)
            {
                onBalance = (int)data.mon00;
                result = await db.Updateable<wap_user_money>()
                    .SetColumnsIF(op == 1, it => it.mon00 == it.mon00 + count)
                    .SetColumnsIF(op != 1, it => it.mon00 == it.mon00 - count)
                    .Where(it => it.uid == uid).ExecuteCommandAsync() > 0;
            }
            else if (money == 1)
            {
                onBalance = (int)data.mon10;
                result = await db.Updateable<wap_user_money>()
                    .SetColumnsIF(op == 1, it => it.mon10 == it.mon10 + count)
                    .SetColumnsIF(op != 1, it => it.mon10 == it.mon10 - count)
                    .Where(it => it.uid == uid).ExecuteCommandAsync() > 0;
            }
            else if (money == 2)
            {
                onBalance = (int)data.mon20;
                result = await db.Updateable<wap_user_money>()
                    .SetColumnsIF(op == 1, it => it.mon20 == it.mon20 + count)
                    .SetColumnsIF(op != 1, it => it.mon20 == it.mon20 - count)
                    .Where(it => it.uid == uid).ExecuteCommandAsync() > 0;
            }
            if (result)
            {
                int upCount = op == 1 ? count : 0 - count;
                //添加日志
                wap_user_money_log log = new wap_user_money_log();
                log.uid = uid;
                log.name = name;
                log.money = money;
                log.amount = upCount;
                log.balance = onBalance + upCount;
                log.remarks = remark;
                log.logpass = logs;
                log.addtime = DateTime.Now;
                await db.Insertable(log).ExecuteCommandAsync();

                //添加消费日志
                if (op != 1)
                {
                    await AddConsumeLog(uid, money, count, remark);
                }
            }
            return result;
        }

        public async Task<bool> AddConsumeLog(int uid, int type, decimal amount, string remark)
        {
            genie_consume log = new genie_consume();
            log.id = StringHelper.NewGuid;
            log.type = type;
            log.userId = uid;
            log.amount = amount;
            log.addTime = DateTime.Now;
            log.remark = remark;
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_consume>();
            return await db.Insertable(log).ExecuteCommandAsync() > 0;
        }

        #endregion 货币操作

        #region 论坛经验

        public async Task<bool> UpdateUserBbsExp(int uid, int op, int count)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_user>();
            return await db.Updateable<wap_user>()
                .SetColumnsIF(op == 1, it => it.point == it.point + count)
                .SetColumnsIF(op != 1, it => it.point == it.point + count)
                .Where(it => it.id == uid).ExecuteCommandAsync() > 0;
        }

        #endregion 论坛经验
    }
}