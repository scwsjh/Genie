using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
	public interface IGenieMallService
	{
		Task<bool> AddMall(genie_mall mall);

		Task<bool> UpdateMall(genie_mall mall);

		Task<bool> Delete(string id);

		Task<genie_mall> GetMallInfo(string id);

		Task<List<genie_mall>> GetGameMallList();

		Task<List<genie_mall>> GetGameMallData();
	}
}