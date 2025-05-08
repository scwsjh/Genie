using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using SiteBridg.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteBridg.Service
{
    public class WapMessageService : IWapMessageService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public WapMessageService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public int GetNoRedMsgCount(int id)
        {
            int result = 0;
            var db = DbClient.AsTenant().GetConnectionWithAttr<wap_user_news>();
            var data = db.Queryable<wap_user_news>().Where(it => it.uid == id).Single();
            if (data != null)
            {
                result = (int)data.home;
            }
            return result;
        }
    }
}