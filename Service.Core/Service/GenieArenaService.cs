using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Service
{
	public class GenieArenaService : IGenieArenaService, ITransient
	{
		private readonly ISqlSugarClient DbClient;

		public GenieArenaService(ISqlSugarClient _DbClient)
		{
			DbClient = _DbClient;
		}

		public async Task<List<genie_arena>> GetArenaData()
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_arena>();
			return await db.Queryable<genie_arena>().ToListAsync();
		}

		public async Task<genie_arena> GetArenaDataOne()
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_arena>();
			return await db.Queryable<genie_arena>().FirstAsync();
		}

		public async Task<genie_arena> GetAreaInfo(string id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_arena>();
			return await db.Queryable<genie_arena>().Where(it => it.id == id).SingleAsync();
		}

		public async Task<List<genie_arena_award>> GetAreaAwardData()
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_arena_award>();
			return await db.Queryable<genie_arena_award>().ToListAsync();
		}

		public async Task<genie_arena_award> GetAreaAwardInfo(string id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_arena_award>();
			return await db.Queryable<genie_arena_award>().Where(it => it.id == id).SingleAsync();
		}

		public async Task<bool> SaveAreaAward(genie_arena_award data)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_arena_award>();
			return await db.Storageable<genie_arena_award>(data).ExecuteCommandAsync() > 0;
		}

		public async Task<bool> DelAreaAward(string id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_arena_award>();
			return await db.Deleteable<genie_arena_award>().Where(it => it.id == id).ExecuteCommandAsync() > 0;
		}

		public async Task<bool> UpdateArenaUser(string id, int uid)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_arena>();
			bool result = await db.Updateable<genie_arena>()
				.SetColumns(it => it.userId == uid)
				.SetColumns(it => it.count == 0).Where(it => it.id == id).ExecuteCommandAsync() > 0;
			if (result)
			{
				await SetArenaUserIsOn(uid);
			}
			return result;
		}

		public async Task<bool> UpdateArenaCount(string id, int count)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_arena>();
			return await db.Updateable<genie_arena>()
				.SetColumns(it => it.count == it.count + count).Where(it => it.id == id).ExecuteCommandAsync() > 0;
		}

		public async Task<genie_arena_user> GetArenaUserInfo(int uid)
		{
			string time = TimeHelper.getDateTimeNumYMD;
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_arena_user>();
			var data = await db.Queryable<genie_arena_user>().Where(it => it.uid == uid).SingleAsync();
			if (data == null)
			{
				data = new genie_arena_user();
				data.uid = uid;
				data.isOn = 0;
				data.time = time;
				data.isPk = 0;
				await db.Insertable(data).ExecuteCommandAsync();
			}
			else
			{
				if (data.time != time)
				{
					data.isOn = 0;
					data.isPk = 0;
					data.time = time;
					await db.Updateable(data).ExecuteCommandAsync();
				}
			}
			return data;
		}

		public async Task SetArenaUserIsPk(int uid)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_arena_user>();
			await db.Updateable<genie_arena_user>().SetColumns(it => it.isPk == 1).Where(it => it.uid == uid).ExecuteCommandAsync();
		}

		public async Task SetArenaUserIsOn(int uid)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_arena_user>();
			await db.Updateable<genie_arena_user>().SetColumns(it => it.isOn == 1).Where(it => it.uid == uid).ExecuteCommandAsync();
		}
	}
}