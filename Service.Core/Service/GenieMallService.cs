using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
	public class GenieMallService : IGenieMallService, ITransient
	{
		private readonly ISqlSugarClient DbClient;

		public GenieMallService(ISqlSugarClient _DbClient)
		{
			DbClient = _DbClient;
		}

		public async Task<bool> AddMall(genie_mall mall)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_mall>();
			return await db.Insertable(mall).ExecuteCommandAsync() > 0;
		}

		public async Task<bool> UpdateMall(genie_mall mall)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_mall>();
			return await db.Updateable(mall).ExecuteCommandAsync() > 0;
		}

		public async Task<bool> Delete(string id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_mall>();
			return await db.Deleteable<genie_mall>().Where(it => it.mallId == id).ExecuteCommandAsync() > 0;
		}

		public async Task<genie_mall> GetMallInfo(string id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_mall>();
			return await db.Queryable<genie_mall>().Where(it => it.mallId == id).SingleAsync();
		}

		public async Task<List<genie_mall>> GetGameMallList()
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_mall>();
			var list = await db.Queryable<genie_mall>()
				.Where(i => i.endTime > DateTime.Now && DateTime.Now > i.addTime)
				.OrderBy(i => i.sort, OrderByType.Asc).ToListAsync();
			return list;
		}

		public async Task<List<genie_mall>> GetGameMallData()
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_mall>();
			var list = await db.Queryable<genie_mall>()
				.OrderBy(i => i.sort, OrderByType.Asc).ToListAsync();
			return list;
		}
	}
}