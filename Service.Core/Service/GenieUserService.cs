using Furion.DatabaseAccessor;
using Kdbndp.KingbaseTypes;
using Kx.Microservices.Domain.Entity;
using Service.Enties;
using SiteBridg.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public class GenieUserService : IGenieUserService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public GenieUserService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public async Task<genie_user> GetGenieUserInfo(int uid)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user>();
            var data = await db.Queryable<genie_user>().Where(it => it.uid == uid).SingleAsync();
            if (data == null)
            {
                data = new genie_user();
                data.uid = uid;
                data.notice = "什么也没留下.";
                data.addTime = DateTime.Now;
                data.lev = 1;
                data.exp = 0;
                data.poin = GameConfig.MaxPoin;
                data.poinTime = DateTime.Now;
                bool isOk = await db.Insertable(data).ExecuteCommandAsync() > 0;
                if (isOk)
                {
                    //添加账户
                    genie_acc acc = new genie_acc();
                    acc.userId = uid;
                    acc.copper = 0;
                    acc.exploit = GameConfig.DefExploit;
                    await db.Insertable(acc).ExecuteCommandAsync();
                }
            }
            else
            {
                if (data.poin < GameConfig.MaxPoin)
                {
                    double diff = TimeHelper.TimeDiffMinutes((DateTime)data.poinTime, DateTime.Now);
                    int count = Convert.ToInt32(Math.Floor(diff / GameConfig.PoinTime));

                    data.poin += count;
                    data.poin = data.poin > GameConfig.MaxPoin ? GameConfig.MaxPoin : data.poin;
                    if (count > 0)
                    {
                        data.poinTime = Convert.ToDateTime(data.poinTime).AddMinutes(count * GameConfig.PoinTime);
                        await db.Updateable(data).ExecuteCommandAsync();
                    }
                }
            }
            return data;
        }

        public async Task<List<genie_user>> GetUserRank(int page, int pageSize, RefAsync<int> total)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user>();
            return await db.Queryable<genie_user>().OrderByDescending(it => it.lev).OrderByDescending(it => it.exp).ToPageListAsync(page, pageSize, total);
        }

        public async Task<bool> UpdateUserExp(int userId, int exp, int op = 1)
        {
            bool result = false;
            try
            {
                var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user>();
                DbClient.AsTenant().BeginTran();
                if (op == 0)//扣除经验
                {
                    result = await db.Updateable<genie_user>().SetColumns(it => it.exp == it.exp - exp).Where(it => it.uid == userId).ExecuteCommandAsync() > 0;
                }
                else//增加经验
                {
                    var MyExp = await db.Queryable<genie_user>().Where(it => it.uid == userId).SingleAsync();

                    decimal onExp = (decimal)MyExp.exp + exp;
                    decimal maxExp = GameTool.GetUserUpExp((int)MyExp.lev);
                    if (onExp < maxExp)//不足升级
                    {
                        result = await db.Updateable<genie_user>().SetColumns(it => it.exp == onExp).Where(it => it.uid == userId).ExecuteCommandAsync() > 0;
                    }
                    else//足够升级
                    {
                        MyExp.lev = MyExp.lev + 1;
                        MyExp.exp = onExp - maxExp;
                        result = await db.Updateable<genie_user>(MyExp).Where(i => i.uid == userId).ExecuteCommandAsync() > 0;
                    }
                }
                DbClient.AsTenant().CommitTran();
            }
            catch
            {
                result = false;
                DbClient.AsTenant().RollbackTran();
            }
            if (result)
            {
            }
            return result;
        }

        #region 元气

        public async Task<genie_user_vigor> GetUserVigor(int uid)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user_vigor>();
            var data = await db.Queryable<genie_user_vigor>().Where(it => it.uid == uid).SingleAsync();
            if (data == null)
            {
                data = new genie_user_vigor();
                data.uid = uid;
                data.vigor = GameConfig.MaxVigor;
                data.upTime = DateTime.Now;
                await db.Insertable(data).ExecuteCommandAsync();
            }
            else
            {
                if (data.vigor < GameConfig.MaxVigor)
                {
                    if (data.upDay != TimeHelper.getDateTimeNumYMD)
                    {
                        data.vigor = GameConfig.MaxVigor;
                        data.upTime = DateTime.Now;
                        data.upDay = TimeHelper.getDateTimeNumYMD;
                        await db.Updateable(data).ExecuteCommandAsync();
                    }
                    else
                    {
                        double diff = TimeHelper.TimeDiffMinutes((DateTime)data.upTime, DateTime.Now);
                        int count = Convert.ToInt32(Math.Floor(diff / GameConfig.VigorTime));

                        data.vigor += count;
                        data.vigor = data.vigor > GameConfig.MaxVigor ? GameConfig.MaxVigor : data.vigor;
                        if (count > 0)
                        {
                            data.upTime = Convert.ToDateTime(data.upTime).AddMinutes(count * GameConfig.VigorTime);
                            await db.Updateable(data).ExecuteCommandAsync();
                        }
                    }
                }
            }
            return data;
        }

        public async Task<bool> UpdateUserVigor(int uid, int op, int count)
        {
            bool result = false;
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user_vigor>();
            if (op == 1)
            {
                result = await db.Updateable<genie_user_vigor>().SetColumns(it => it.vigor == it.vigor + count).Where(it => it.uid == uid).ExecuteCommandAsync() > 0;
            }
            else
            {
                var data = await db.Queryable<genie_user_vigor>().Where(it => it.uid == uid).SingleAsync();
                bool upTime = data.vigor >= GameConfig.MaxVigor;
                data.vigor = data.vigor - count;
                if (upTime)
                {
                    data.upTime = DateTime.Now;
                }
                result = await db.Updateable(data).ExecuteCommandAsync() > 0;
            }
            return result;
        }

        #endregion 元气

        #region 状态

        public async Task<bool> AddUserState(int uid, GoodsBuff buff, int count = 1)
        {
            bool result = false;
            var db = DbClient.AsTenant().GetConnectionScopeWithAttr<genie_user_buff>();
            string key = $"{uid}_{buff.goodsId}";
            var data = await db.Queryable<genie_user_buff>().Where(it => it.ubId == key).SingleAsync();
            if (data == null)
            {
                data = new genie_user_buff();
                data.ubId = key;
                data.userId = uid;
                data.goodsId = buff.goodsId;
                data.name = buff.name;
                data.code = buff.code;
                data.img = buff.img;
                data.tip = buff.tip;
                data.sign = buff.sign;
                data.attr = JsonConvert.SerializeObject(buff.attr);
                data.count = Convert.ToInt32(buff.count) * count;
                result = await db.Insertable(data).ExecuteCommandAsync() > 0;
            }
            else
            {
                int addCount = Convert.ToInt32(buff.count) * count;
                result = await db.Updateable<genie_user_buff>().SetColumns(it => it.count == it.count + addCount).Where(it => it.ubId == key).ExecuteCommandAsync() > 0;
            }
            return result;
        }

        public async Task<List<genie_user_buff>> GetUserBuff(int uid)
        {
            var db = DbClient.AsTenant().GetConnectionScopeWithAttr<genie_user_buff>();
            return await db.Queryable<genie_user_buff>().Where(it => it.userId == uid && it.count > 0).ToListAsync();
        }

        public async Task<decimal> GetUserBuff(int uid, string code)
        {
            decimal sum = 0.00M;
            var data = await GetUserBuff(uid);
            data = data.FindAll(it => it.code == code).ToList();
            sum = data.Sum(it => (decimal)it.sign);
            return sum;
        }

        public async Task<bool> UpdateUserBuffCount(int uid, string code, int count = 1)
        {
            var db = DbClient.AsTenant().GetConnectionScopeWithAttr<genie_user_buff>();
            return await db.Updateable<genie_user_buff>()
                .SetColumns(it => it.count == it.count - count)
                .Where(it => it.userId == uid && it.code == code && it.count > 0).ExecuteCommandAsync() > 0;
        }

        #endregion 状态

        #region 更新温馨值

        public async Task<bool> UpdateUserPoin(int uid, int op, int count)
        {
            bool result = false;
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user>();
            if (op == 1)
            {
                result = await db.Updateable<genie_user>().SetColumns(it => it.poin == it.poin + count).Where(it => it.uid == uid).ExecuteCommandAsync() > 0;
            }
            else
            {
                var data = await db.Queryable<genie_user>().Where(it => it.uid == uid).SingleAsync();
                bool upTime = data.poin >= GameConfig.MaxPoin;
                data.poin = data.poin - count;
                if (upTime)
                {
                    data.poinTime = DateTime.Now;
                }
                result = await db.Updateable(data).ExecuteCommandAsync() > 0;
            }
            return result;
        }

        #endregion 更新温馨值

        #region 称号奖励

        public async Task<bool> CheakUserHonnerIsGet(int userId)
        {
            bool result = true;
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_honnor_log>();
            var data = await db.Queryable<genie_honnor_log>().Where(it => it.userId == userId).SingleAsync();
            if (data == null)
            {
                result = false;
                data = new genie_honnor_log();
                data.userId = userId;
                data.time = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                data.addTime = DateTime.Now;
                await db.Insertable(data).ExecuteCommandAsync();
            }
            else
            {
                result = TimeHelper.getDateTimeNumYMD == data.time;
            }
            return result;
        }

        public async Task<bool> SetUserHonnerGet(int userId)
        {
            genie_honnor_log log = new genie_honnor_log();
            log.userId = userId;
            log.time = TimeHelper.getDateTimeNumYMD;
            log.addTime = DateTime.Now;
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_honnor_log>();
            return await db.Updateable(log).ExecuteCommandAsync() > 0;
        }

        #endregion 称号奖励
    }
}