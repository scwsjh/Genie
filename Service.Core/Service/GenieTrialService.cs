using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TencentCloud.Ame.V20190916.Models;

namespace Service.Core
{
	public class GenieTrialService : IGenieTrialService, ITransient
	{
		private readonly ISqlSugarClient DbClient;

		public GenieTrialService(ISqlSugarClient _DbClient)
		{
			DbClient = _DbClient;
		}

		public async Task<List<genie_trial>> GetTrialData()
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_trial>();
			return await db.Queryable<genie_trial>().Where(it => it.status == 1).ToListAsync();
		}

		public async Task<List<genie_trial>> GetTrialDataAll()
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_trial>();
			return await db.Queryable<genie_trial>().ToListAsync();
		}

		public async Task<genie_trial> GetTrialInfo(int id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_trial>();
			return await db.Queryable<genie_trial>().Where(it => it.id == id).SingleAsync();
		}

		public async Task<bool> SaveTrial(genie_trial data)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_trial>();
			return await db.Storageable(data).ExecuteCommandAsync() > 0;
		}

		public async Task<bool> DeleteTrial(int id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_trial>();
			return await db.Deleteable<genie_trial>().Where(it => it.id == id).ExecuteCommandAsync() > 0;
		}

		public async Task<FightTemp> GetMonsterFightTemp(int id, string monsters, string nick = "")
		{
			FightTemp result = new FightTemp();
			result.uid = id;
			result.nick = nick;
			result.camp = FightEnum.Camp.Def.ToString();
			result.genie = await GetTeamGenieAttr(monsters);

			return result;
		}

		public async Task<List<GenieAttr>> GetTeamGenieAttr(string monsters)
		{
			var monsterService = App.GetService<IGenieService>();
			List<GenieAttr> result = new List<GenieAttr>();
			string[] mons = monsters.Split('#');
			int index = 1;
			foreach (var monster in mons)
			{
				var monsterInfo = await monsterService.GetMonsterInfo(monster);
				if (monsterInfo != null)
				{
					GenieAttr temp = new GenieAttr();
					temp.num = index;
					temp.camp = FightEnum.Camp.Def.ToString();
					temp.id = monster;
					temp.genId = 0;
					temp.name = monsterInfo.name;
					temp.lev = (int)monsterInfo.lev;
					temp.start = (int)monsterInfo.start;
					temp.charm = GenieTool.GetGenAttrCompute((int)monsterInfo.charm, (int)monsterInfo.lev, (int)monsterInfo.start);
					temp.blood = GenieTool.GetGenAttrCompute((int)monsterInfo.blood, (int)monsterInfo.lev, (int)monsterInfo.start);
					temp.status = 1;
					result.Add(temp);
					index++;
				}
			}
			return result;
		}
	}
}