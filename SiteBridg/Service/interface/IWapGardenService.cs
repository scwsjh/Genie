using Service.Enties;
using SiteBridg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteBridg.Service
{
    public interface IWapGardenService
    {
        Task<wap_garden> GetGardenInfo(int uid);

        Task<bool> UpdateGardenExp(int uid, int op, int count);

        #region 花朵操作

        Task<long> GetUserFlowerCount(int uid, int mapId);

        Task<bool> UpdateUserFlower(int uid, int mapId, int op, int count);

        #endregion 花朵操作

        #region 花园种子

        Task<wap_garden_seed> GetGardenSendInfo(int id);

        Task<bool> UpdateUserGardenSend(int uid, int op, int goodsId, int count);

        #endregion 花园种子

        #region 花园道具

        Task<wap_garden_dz> GetGardenPropInfo(int id);

        Task<bool> UpdateUserGardenProp(int uid, int op, int goodsId, int count);

        #endregion 花园道具
    }
}