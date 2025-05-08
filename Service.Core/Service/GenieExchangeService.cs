using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
	public class GenieExchangeService : IGenieExchangeService, ITransient
	{
		private readonly ISqlSugarClient DbClient;

		public GenieExchangeService(ISqlSugarClient _DbClient)
		{
			DbClient = _DbClient;
		}

		public async Task<List<genie_exchange>> GetExChangeList()
		{
			DateTime onTime = DateTime.Now;
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_exchange>();
			var data = await db.Queryable<genie_exchange>().Where(it => it.startTime < onTime && it.endTime > onTime).ToListAsync();
			return data;
		}

		public async Task<List<genie_exchange>> GetExChangeData()
		{
			DateTime onTime = DateTime.Now;
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_exchange>();
			var data = await db.Queryable<genie_exchange>().ToListAsync();
			return data;
		}

		public async Task<genie_exchange> GetExChangeInfo(int exId)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_exchange>();
			return await db.Queryable<genie_exchange>().Where(it => it.exId == exId).SingleAsync();
		}

		public async Task<bool> SaveExChange(genie_exchange data)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_exchange>();
			return await db.Storageable(data).ExecuteCommandAsync() > 0;
		}

		public async Task<bool> DelExChange(int id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_exchange>();
			return await db.Deleteable<genie_exchange>().Where(it => it.exId == id).ExecuteCommandAsync() > 0;
		}

		public async Task<int> GetUserExChangeCount(int userId, int exId)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_exchange_log>();
			return await db.Queryable<genie_exchange_log>().Where(it => it.userId == userId && it.exId == exId).SumAsync(it => (int)it.count);
		}

		public async Task<bool> InsertUserExChangeLog(int userId, int count, genie_exchange data)
		{
			genie_exchange_log log = new genie_exchange_log();
			log.id = StringHelper.NewGuid;
			log.userId = userId;
			log.exId = data.exId;
			log.count = count;
			log.addTime = DateTime.Now;
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_exchange_log>();
			return await db.Insertable(log).ExecuteCommandAsync() > 0;
		}
	}
}