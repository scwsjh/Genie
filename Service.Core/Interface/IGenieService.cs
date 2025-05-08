using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
	public interface IGenieService
	{
		Task<List<genie_genie>> GetGenieData(int type, int page, int pageSize, RefAsync<int> total);

		Task<List<genie_genie>> GetGenieData();

		Task<genie_genie> GetGenieInfo(int genieId);

		Task<bool> SaveGenie(genie_genie data);

		Task<bool> DeleteGenie(int genieId);

		Task<List<genie_monster>> GetMonsterData();

		Task<genie_monster> GetMonsterInfo(string id);

		Task<bool> SaveMonster(genie_monster data);

		Task<bool> DeleteMonster(string id);

		Task<List<genie_user_genie>> GetUserGenieData(int uid, int type, int page, int pageSize, RefAsync<int> total);

		Task<List<genie_user_genie>> GetUserGenieDataBySerch(int uid, List<string> NoAt, int page, int pageSize, RefAsync<int> total, string serch = "");

		Task<List<genie_user_genie>> GetUserGenieDataByFusion(int uid, int type, int start, string NoOn, int page, int pageSize, RefAsync<int> total, string serch = "");

		Task<int> GetUserGenieCount(int uid);

		Task<bool> GetGenieIsHave(int uid, int genieId);

		Task<genie_map> GetGenieMap(int uid, int genieId);

		Task<bool> UpdateMapAwardStatus(int uid, int genId, int isGet);

		Task<genie_user_genie> GetUserGenieInfo(string gid);

		Task<genie_user_genie> GetUserGenieInfoByAttr(string gid);

		Task<bool> AddUserGenie(int uid, int genId, int count);

		Task<bool> UpdateUserGenie(genie_user_genie info);

		Task<bool> DelUserGenie(string gu);

		#region 技能

		Task<List<genie_skill>> GetGenieSkillData();

		Task<genie_skill> GetGenieSkillInfo(int id);

		Task<bool> SaveGenieSkill(genie_skill data);

		Task<bool> DeleteGenieSkill(int id);

		#endregion 技能
	}
}