using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
	public interface IGenieTreasureService
	{
		Task<List<genie_treasure>> GetTreasureDataAll();

		Task<List<genie_treasure>> GetTreasureData();

		Task<genie_treasure> GetTreasureInfo(int Id);

		Task<bool> SaveTreasure(genie_treasure data);

		Task<bool> DeleteTreasure(int id);
	}
}