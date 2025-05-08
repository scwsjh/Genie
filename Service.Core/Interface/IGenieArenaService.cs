using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
	public interface IGenieArenaService
	{
		Task<List<genie_arena>> GetArenaData();

		Task<genie_arena> GetArenaDataOne();

		Task<genie_arena> GetAreaInfo(string id);

		Task<List<genie_arena_award>> GetAreaAwardData();

		Task<genie_arena_award> GetAreaAwardInfo(string id);

		Task<bool> SaveAreaAward(genie_arena_award data);

		Task<bool> DelAreaAward(string id);

		Task<bool> UpdateArenaUser(string id, int uid);

		Task<bool> UpdateArenaCount(string id, int count);

		Task<genie_arena_user> GetArenaUserInfo(int uid);

		Task SetArenaUserIsPk(int uid);

		Task SetArenaUserIsOn(int uid);
	}
}