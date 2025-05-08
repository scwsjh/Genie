using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Enties;
using SiteBridg.Model;

namespace SiteBridg.Service
{
    public interface IWapUserService
    {
        Task<wap_user> GetUserInfo(int id);

        Task<wap_user> GetUserInfoBySid(string sid);

        wap_user GetUserInfoBySidSync(string sid);

        #region 货币操作

        Task<List<wap_money>> GetMoneyData();

        Task<wap_money> GetMoneyInfo(int id);

        Task<wap_user_money> GetUserMoneyInfo(int uid);

        Task<int> GetUserMoneyCount(int uid, int money);

        Task<bool> UpdateUserMoney(int uid, int money, int op, int count, string name, string remark, string logs);

        #endregion 货币操作

        #region 论坛经验

        Task<bool> UpdateUserBbsExp(int uid, int op, int count);

        #endregion 论坛经验
    }
}