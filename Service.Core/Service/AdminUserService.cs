namespace Service.Core
{
	public class AdminUserService : IAdminUserService, ITransient
	{
		private readonly ISqlSugarClient DbClient;

		public AdminUserService(ISqlSugarClient _DbClient)
		{
			DbClient = _DbClient;
		}

		public async Task<bool> DelAdminUser(string adminId)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<admin_user>();
			return await db.Deleteable<admin_user>().Where(i => i.adminId == adminId).ExecuteCommandAsync() > 0;
		}

		public async Task<bool> AddAdminUser(admin_user user)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<admin_user>();
			return await db.Insertable(user).ExecuteCommandAsync() > 0;
		}

		public async Task<admin_user> GetAdminUserInfo(string id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<admin_user>();
			return await db.Queryable<admin_user>().Where(it => it.adminId == id).SingleAsync();
		}

		public async Task<bool> UpdateAdminUser(admin_user user)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<admin_user>();
			return await db.Updateable(user).ExecuteCommandAsync() > 0;
		}

		public async Task<List<admin_user>> GetAdminUserList()
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<admin_user>();
			return await db.Queryable<admin_user>().OrderBy(i => i.addTime, OrderByType.Desc).ToListAsync();
		}

		public async Task<admin_user> GetAdminUserInfoByNo(int no, int status = 1)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<admin_user>();
			return await db.Queryable<admin_user>().Where(i => i.adminNo == no).WhereIF(status == 1, i => i.status == 1).SingleAsync();
		}

		public admin_user GetAdminUserInfoByNoAsyc(int no, int status = 1)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<admin_user>();
			return db.Queryable<admin_user>().Where(i => i.adminNo == no).WhereIF(status == 1, i => i.status == 1).Single();
		}
	}
}