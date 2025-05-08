using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Service
{
	public class GenieDicService : IGenieDicService, ITransient
	{
		private readonly ISqlSugarClient DbClient;

		public GenieDicService(ISqlSugarClient _DbClient)
		{
			DbClient = _DbClient;
		}

		public async Task<List<genie_dic>> GetDicData()
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_dic>();
			return await db.Queryable<genie_dic>().ToListAsync();
		}

		public async Task<genie_dic> GetDicInfo(string code)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_dic>();
			return await db.Queryable<genie_dic>().Where(x => x.code == code).SingleAsync();
		}

		public async Task<bool> UpdateDic(genie_dic data)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_dic>();
			return await db.Updateable(data).ExecuteCommandAsync() > 0;
		}
	}
}