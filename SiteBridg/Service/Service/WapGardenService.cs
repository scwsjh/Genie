using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteBridg.Model;
using Service.Enties;

namespace SiteBridg.Service
{
    public class WapGardenService : IWapGardenService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public WapGardenService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public async Task<wap_garden> GetGardenInfo(int uid)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_garden>();
            var data = await db.Queryable<wap_garden>().Where(it => it.uid == uid).SingleAsync();
            return data;
        }

        public async Task<bool> UpdateGardenExp(int uid, int op, int count)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_garden>();
            bool result = await db.Updateable<wap_garden>().SetColumnsIF(op == 1, it => it.point == it.point + count)
                .SetColumnsIF(op != 1, it => it.point == it.point - count).Where(it => it.uid == uid).ExecuteCommandAsync() > 0;
            return result;
        }

        #region 花朵操作

        public async Task<long> GetUserFlowerCount(int uid, int mapId)
        {
            int result = 0;
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_garden_basket>();
            var basket = await db.Queryable<wap_garden_basket>().Where(it => it.uid == uid && it.mapid == mapId).FirstAsync();
            if (basket != null)
            {
                result += (int)basket.amount;
            }
            var bottle = await db.Queryable<wap_garden_bottle>().Where(it => it.uid == uid && it.mapid == mapId).FirstAsync();
            if (bottle != null)
            {
                result += (int)bottle.amount;
            }

            return result;
        }

        public async Task<bool> UpdateUserFlower(int uid, int mapId, int op, int count)
        {
            bool result = false;
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_garden_basket>();
            if (op == 1)
            {
                var basketInfo = await db.Queryable<wap_garden_basket>().Where(it => it.uid == uid && it.mapid == mapId).FirstAsync();
                if (basketInfo == null)
                {
                    wap_garden_basket add = new wap_garden_basket();
                    add.uid = uid;
                    add.mapid = mapId;
                    add.amount = count;
                    result = await db.Insertable(add).ExecuteCommandAsync() > 0;
                }
                else
                {
                    basketInfo.amount += count;
                    result = await db.Updateable(basketInfo).ExecuteCommandAsync() > 0;
                }
            }
            else
            {
                var bottleInfo = await db.Queryable<wap_garden_bottle>().Where(it => it.uid == uid && it.mapid == mapId).FirstAsync();
                if (bottleInfo != null)
                {
                    int opCount = bottleInfo.amount >= count ? count : (int)bottleInfo.amount;
                    if (opCount > 0)
                    {
                        result = await db.Updateable<wap_garden_bottle>().SetColumns(it => it.amount == it.amount - opCount).Where(it => it.id == bottleInfo.id).ExecuteCommandAsync() > 0;
                        if (result)
                        {
                            count = count - opCount;
                        }
                    }
                }

                if (count > 0)
                {
                    var basketInfo = await db.Queryable<wap_garden_basket>().Where(it => it.uid == uid && it.mapid == mapId).FirstAsync();
                    if (basketInfo != null)
                    {
                        int opCount = basketInfo.amount >= count ? count : (int)basketInfo.amount;
                        result = await db.Updateable<wap_garden_basket>().SetColumns(it => it.amount == it.amount - opCount).Where(it => it.id == basketInfo.id).ExecuteCommandAsync() > 0;
                    }
                }
            }
            return result;
        }

        #endregion 花朵操作

        #region 花园种子

        public async Task<wap_garden_seed> GetGardenSendInfo(int id)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_garden_seed>();
            return await db.Queryable<wap_garden_seed>().Where(it => it.id == id).SingleAsync();
        }

        public async Task<bool> UpdateUserGardenSend(int uid, int op, int goodsId, int count)
        {
            bool result = false;
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_garden_bag>();
            var data = await db.Queryable<wap_garden_bag>().Where(it => it.uid == uid && it.did == goodsId && it.dtype == 0).FirstAsync();
            if (op == 1)
            {
                if (data == null)
                {
                    var sendInfo = await GetGardenSendInfo(goodsId);
                    if (sendInfo != null)
                    {
                        wap_garden_bag bag = new wap_garden_bag();
                        bag.uid = uid;
                        bag.did = goodsId;
                        bag.name = sendInfo.name;
                        bag.dtype = 0;
                        bag.amount = count;
                        bag.point = 0;
                        result = await db.Insertable(bag).ExecuteCommandAsync() > 0;
                    }
                }
                else
                {
                    result = await db.Updateable<wap_garden_bag>().SetColumns(it => it.amount == it.amount + count).Where(it => it.id == data.id).ExecuteCommandAsync() > 0;
                }
            }
            else
            {
                if (data != null)
                {
                    result = await db.Updateable<wap_garden_bag>().SetColumns(it => it.amount == it.amount - count).Where(it => it.id == data.id).ExecuteCommandAsync() > 0;
                }
            }
            return result;
        }

        #endregion 花园种子

        #region 花园道具

        public async Task<wap_garden_dz> GetGardenPropInfo(int id)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_garden_dz>();
            return await db.Queryable<wap_garden_dz>().Where(it => it.id == id).SingleAsync();
        }

        public async Task<bool> UpdateUserGardenProp(int uid, int op, int goodsId, int count)
        {
            bool result = false;
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_garden_bag>();
            var data = await db.Queryable<wap_garden_bag>().Where(it => it.uid == uid && it.did == goodsId && it.dtype == 1).SingleAsync();
            if (op == 1)
            {
                if (data == null)
                {
                    var propInfo = await GetGardenPropInfo(goodsId);
                    if (propInfo != null)
                    {
                        wap_garden_bag bag = new wap_garden_bag();
                        bag.uid = uid;
                        bag.did = goodsId;
                        bag.name = propInfo.name;
                        bag.dtype = 1;
                        bag.amount = count;
                        bag.point = propInfo.point;
                        result = await db.Insertable(bag).ExecuteCommandAsync() > 0;
                    }
                }
                else
                {
                    result = await db.Updateable<wap_garden_bag>().SetColumns(it => it.amount == it.amount + count).Where(it => it.id == data.id).ExecuteCommandAsync() > 0;
                }
            }
            else
            {
                if (data != null)
                {
                    result = await db.Updateable<wap_garden_bag>().SetColumns(it => it.amount == it.amount - count).Where(it => it.id == data.id).ExecuteCommandAsync() > 0;
                }
            }
            return result;
        }

        #endregion 花园道具
    }
}