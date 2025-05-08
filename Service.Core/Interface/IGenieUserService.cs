using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public interface IGenieUserService
    {
        Task<genie_user> GetGenieUserInfo(int uid);

        Task<List<genie_user>> GetUserRank(int page, int pageSize, RefAsync<int> total);

        Task<bool> UpdateUserExp(int userId, int exp, int op = 1);

        #region 元气

        Task<genie_user_vigor> GetUserVigor(int uid);

        Task<bool> UpdateUserVigor(int uid, int op, int count);

        #endregion 元气

        #region 状态

        Task<bool> AddUserState(int uid, GoodsBuff buff, int count = 1);

        Task<List<genie_user_buff>> GetUserBuff(int uid);

        Task<decimal> GetUserBuff(int uid, string code);

        Task<bool> UpdateUserBuffCount(int uid, string code, int count = 1);

        #endregion 状态

        #region 更新温馨值

        Task<bool> UpdateUserPoin(int uid, int op, int count);

        #endregion 更新温馨值

        #region 称号奖励

        Task<bool> CheakUserHonnerIsGet(int userId);

        Task<bool> SetUserHonnerGet(int userId);

        #endregion 称号奖励
    }
}