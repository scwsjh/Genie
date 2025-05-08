namespace Service.Core
{
	public interface IAdminUserService
	{
		Task<bool> DelAdminUser(string adminId);

		Task<bool> AddAdminUser(admin_user user);

		Task<admin_user> GetAdminUserInfo(string id);

		Task<bool> UpdateAdminUser(admin_user user);

		Task<List<admin_user>> GetAdminUserList();

		Task<admin_user> GetAdminUserInfoByNo(int no, int status = 1);

		admin_user GetAdminUserInfoByNoAsyc(int no, int status = 1);
	}
}