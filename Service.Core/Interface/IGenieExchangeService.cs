using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
	public interface IGenieExchangeService
	{
		Task<List<genie_exchange>> GetExChangeList();

		Task<List<genie_exchange>> GetExChangeData();

		Task<bool> SaveExChange(genie_exchange data);

		Task<bool> DelExChange(int id);

		Task<genie_exchange> GetExChangeInfo(int exId);

		Task<int> GetUserExChangeCount(int userId, int exId);

		Task<bool> InsertUserExChangeLog(int userId, int count, genie_exchange data);
	}
}