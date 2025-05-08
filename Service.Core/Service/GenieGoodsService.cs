using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Service
{
    public class GenieGoodsService : IGenieGoodsService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public GenieGoodsService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public async Task<bool> DelGoods(int goodsId)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_goods>();
            return await db.Deleteable<genie_goods>().Where(i => i.goodsId == goodsId).ExecuteCommandAsync() > 0;
        }

        public async Task<List<genie_goods>> GetGoodsList()
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_goods>();
            return await db.Queryable<genie_goods>().ToListAsync();
        }

        public async Task<bool> AddGenieGoods(genie_goods goods)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_goods>();
            return await db.Insertable(goods).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> DeleteGoods(int id)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_goods>();
            return await db.Deleteable<genie_goods>().Where(it => it.goodsId == id).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> UpdateGenieGoods(genie_goods goods)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_goods>();
            return await db.Updateable(goods).ExecuteCommandAsync() > 0;
        }

        public async Task<genie_goods> GetGoodsInfo(int goodsId)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_goods>();
            return await db.Queryable<genie_goods>().Where(it => it.goodsId == goodsId).SingleAsync();
        }

        public async Task<genie_user_goods> GetUserGoodsInfo(string ugId)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user_goods>();
            return await db.Queryable<genie_user_goods>().Where(it => it.ugId == ugId).SingleAsync();
        }

        public async Task<genie_user_goods> GetUserGoodsInfo(int uid, int goodsId)
        {
            string ugId = $"{uid}_{goodsId}";
            return await GetUserGoodsInfo(ugId);
        }

        public async Task<int> GetUserGoodsCount(int uid, int goodsId)
        {
            int result = 0;
            var data = await GetUserGoodsInfo(uid, goodsId);
            if (data != null)
            {
                result = (int)data.count;
            }
            return result;
        }

        public async Task<List<genie_user_goods>> GetUserGoodsData(int uid, int page, int pageSize, RefAsync<int> total)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user_goods>();
            var data = await db.Queryable<genie_user_goods>().Where(it => it.userId == uid && it.count > 0)
                     .OrderBy(it => it.ugId, OrderByType.Asc).ToPageListAsync(page, pageSize, total);
            return data;
        }

        public async Task<bool> UpdateUserGoods(int uid, int op, int goodsId, int count, string remark = "")
        {
            bool isOk = false;
            genie_user_goods UserGoods = new genie_user_goods();
            string ugId = $"{uid}_{goodsId}";
            try
            {
                var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user_goods>();
                UserGoods = await db.Queryable<genie_user_goods>().Where(it => it.ugId == ugId).SingleAsync();
                DbClient.AsTenant().BeginTran();
                if (op == 1)//增加
                {
                    if (UserGoods == null)
                    {
                        var goodsInfo = await GetGoodsInfo(goodsId);
                        UserGoods = new genie_user_goods();
                        UserGoods.ugId = ugId;
                        UserGoods.userId = uid;
                        UserGoods.goodsId = goodsId;
                        UserGoods.count = count;
                        UserGoods.lev = goodsInfo.lev;
                        UserGoods.code = goodsInfo.code;
                        UserGoods.goodsName = goodsInfo.goodsName;
                        isOk = await db.Insertable(UserGoods).ExecuteCommandAsync() > 0;
                    }
                    else
                    {
                        isOk = await db.Updateable<genie_user_goods>().SetColumns(it => it.count == it.count + count).Where(it => it.ugId == ugId).ExecuteCommandAsync() > 0;
                    }
                }
                else
                {
                    isOk = await db.Updateable<genie_user_goods>().SetColumns(it => it.count == it.count - count).Where(it => it.ugId == ugId).ExecuteCommandAsync() > 0;
                }
                DbClient.AsTenant().CommitTran();
            }
            catch
            {
                DbClient.AsTenant().RollbackTran();
                isOk = false;
            }
            if (isOk)
            {
            }
            return isOk;
        }
    }
}