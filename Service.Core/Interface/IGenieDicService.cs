using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
	public interface IGenieDicService
	{
		Task<List<genie_dic>> GetDicData();

		Task<genie_dic> GetDicInfo(string code);

		Task<bool> UpdateDic(genie_dic data);
	}
}