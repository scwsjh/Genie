using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
	public interface IGenieNoticeService
	{
		Task<List<genie_notice>> GetGenieNoticeData();

		Task<List<genie_notice>> GetGenieNoticeOnData();

		Task<genie_notice> GetNoticeInfo(string id);

		Task<bool> SaveNotice(genie_notice data);

		Task<bool> DelNotice(string id);
	}
}