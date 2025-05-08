using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Service.Core
{
	public class GenieTool
	{
		public static string GetGenieTypeTips(int type)
		{
			string result = "普通";
			switch (type)
			{
				case 1:
					result = "普通";
					break;

				case 2:
					result = "精英";
					break;

				case 3:
					result = "珍稀";
					break;

				case 4:
					result = "传说";
					break;
			}
			return result;
		}

		public static int GetGenAttrCompute(int unit, int lev, int start)
		{
			int result = unit * Convert.ToInt32(Math.Pow(2, (lev - 1)));
			if (start > 0)
			{
				int add = Convert.ToInt32(Math.Pow(2, start - 1));
				decimal bs = add * 0.1M;

				result += Convert.ToInt32(result * bs);
			}
			return result;
		}
	}
}