using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
	public class GenieNoticeService : IGenieNoticeService, ITransient
	{
		private readonly ISqlSugarClient DbClient;

		public GenieNoticeService(ISqlSugarClient _DbClient)
		{
			DbClient = _DbClient;
		}

		public async Task<List<genie_notice>> GetGenieNoticeData()
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_notice>();
			return await db.Queryable<genie_notice>().OrderByDescending(it => it.addTime).ToListAsync();
		}

		public async Task<List<genie_notice>> GetGenieNoticeOnData()
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_notice>();
			return await db.Queryable<genie_notice>().Where(it => it.endTime > DateTime.Now).ToListAsync();
		}

		public async Task<genie_notice> GetNoticeInfo(string id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_notice>();
			return await db.Queryable<genie_notice>().Where(it => it.noticeId == id).SingleAsync();
		}

		public async Task<bool> SaveNotice(genie_notice data)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_notice>();
			return await db.Storageable(data).ExecuteCommandAsync() > 0;
		}

		public async Task<bool> DelNotice(string id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_notice>();
			return await db.Deleteable<genie_notice>().Where(it => it.noticeId == id).ExecuteCommandAsync() > 0;
		}
	}
}