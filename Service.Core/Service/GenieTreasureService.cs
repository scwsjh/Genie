using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
	public class GenieTreasureService : IGenieTreasureService, ITransient
	{
		private readonly ISqlSugarClient DbClient;

		public GenieTreasureService(ISqlSugarClient _DbClient)
		{
			DbClient = _DbClient;
		}

		public async Task<List<genie_treasure>> GetTreasureDataAll()
		{
			DateTime onTime = DateTime.Now;
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_treasure>();
			var data = await db.Queryable<genie_treasure>().ToListAsync();
			return data;
		}

		public async Task<List<genie_treasure>> GetTreasureData()
		{
			DateTime onTime = DateTime.Now;
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_treasure>();
			var data = await db.Queryable<genie_treasure>().Where(it => it.addTime < onTime && it.endTime > onTime).ToListAsync();
			return data;
		}

		public async Task<genie_treasure> GetTreasureInfo(int Id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_treasure>();
			return await db.Queryable<genie_treasure>().Where(it => it.id == Id).SingleAsync();
		}

		public async Task<bool> SaveTreasure(genie_treasure data)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_treasure>();
			return await db.Storageable(data).ExecuteCommandAsync() > 0;
		}

		public async Task<bool> DeleteTreasure(int id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_treasure>();
			return await db.Deleteable<genie_treasure>().Where(it => it.id == id).ExecuteCommandAsync() > 0;
		}
	}
}