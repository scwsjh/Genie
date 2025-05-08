using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
	public interface IGenieTrialService
	{
		Task<List<genie_trial>> GetTrialData();

		Task<List<genie_trial>> GetTrialDataAll();

		Task<genie_trial> GetTrialInfo(int id);

		Task<bool> SaveTrial(genie_trial data);

		Task<bool> DeleteTrial(int id);

		Task<FightTemp> GetMonsterFightTemp(int id, string monsters, string nick = "");

		Task<List<GenieAttr>> GetTeamGenieAttr(string monsters);
	}
}