using System.Xml;

namespace Service.Core
{
    public class UnitTool
    {
        #region 基础辅助方法

        public static string GetConstellation(DateTime dt)
        {
            string result = string.Empty;
            if (dt.DayOfYear >= 20 && dt.DayOfYear <= 49)
            {
                result = "水瓶座";
            }
            else if (dt.DayOfYear >= 50 && dt.DayOfYear <= 80)
            {
                result = "双鱼座";
            }
            else if (dt.DayOfYear >= 81 && dt.DayOfYear <= 110)
            {
                result = "白羊座";
            }
            else if (dt.DayOfYear >= 111 && dt.DayOfYear <= 141)
            {
                result = "金牛座";
            }
            else if (dt.DayOfYear >= 142 && dt.DayOfYear <= 173)
            {
                result = "双子座";
            }
            else if (dt.DayOfYear >= 174 && dt.DayOfYear <= 204)
            {
                result = "巨蟹座";
            }
            else if (dt.DayOfYear >= 205 && dt.DayOfYear <= 235)
            {
                result = "狮子座";
            }
            else if (dt.DayOfYear >= 236 && dt.DayOfYear <= 266)
            {
                result = "处女座";
            }
            else if (dt.DayOfYear >= 267 && dt.DayOfYear <= 297)
            {
                result = "天秤座";
            }
            else if (dt.DayOfYear >= 298 && dt.DayOfYear <= 327)
            {
                result = "天蝎座";
            }
            else if (dt.DayOfYear >= 328 && dt.DayOfYear <= 356)
            {
                result = "射手座";
            }
            else
            {
                result = "摩羯座";
            }
            return result;
        }

        #endregion 基础辅助方法

        #region 锁

        public static async Task<bool> OperateCheakLock(string id, string pref = "PK_KEY")
        {
            var cache = App.GetService<IMemoryCache>();
            string key = string.Format("{0}_{1}", pref, id);
            if (await cache.ExistsAsync(key))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task UnOperateLock(string id, string pref = "PK_KEY")
        {
            var cache = App.GetService<IMemoryCache>();
            string key = string.Format("{0}_{1}", pref, id);
            await cache.DelAsync(key);
        }

        public static async Task<string> SetPageToken(string sid)
        {
            string key = string.Format("{0}:{1}", "PageToken", sid);
            string token = StringHelper.RandomString(12);
            var cache = App.GetService<IMemoryCache>();
            await cache.SetAsync(key, token, 60);
            return token;
        }

        public static async Task<bool> CheakPageToken(string sid, string token)
        {
            var cache = App.GetService<IMemoryCache>();
            string key = string.Format("{0}:{1}", "PageToken", sid);
            string _token = await cache.GetAsync<string>(key);
            await cache.DelAsync(key);
            return token == _token;
        }

        #endregion 锁

        public static string TimeCodeName(string timeCode)
        {
            string result = string.Empty;
            switch (timeCode)
            {
                case "Long":
                    result = "永久";
                    break;

                case "Month":
                    result = "每月";
                    break;

                case "Week":
                    result = "每周";
                    break;

                case "Day":
                    result = "每天";
                    break;
            }
            return result;
        }

        public static string UrlToSid(string url, string sid)
        {
            return url.Contains("?") ? string.Format("{0}&sid={1}", url, sid) : string.Format("{0}?sid={1}", url, sid);
        }

        /// <summary>
        /// 百分比文本转换
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="type">0普通 1百分比</param>
        /// <returns></returns>
        public static string ConvertPercentString(decimal parameter, int type)
        {
            if (type == 1)
            {
                return string.Format("+{0}%", Convert.ToInt32(parameter * 100));
            }
            else
            {
                return string.Format("+{0}", parameter);
            }
        }

        /// <summary>
        /// 操作运算
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="par"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static decimal ComputeResult(decimal unit, decimal par, string type)
        {
            decimal result = 0;
            switch (type)
            {
                case "Plus":
                    result = unit + par;
                    break;

                case "Ride":
                    result = unit * (1 + par);
                    break;

                case "Reduce":
                    result = unit - par;
                    break;

                case "Except":
                    result = unit / par;
                    break;
            }
            return result;
        }

        #region 排序字符串

        private List<object> m_values = new List<object>();

        public void SetValue(object value)
        {
            m_values.Add(value);
        }

        public string GetSortField
        {
            get
            {
                string result = string.Empty;
                m_values.Sort();
                foreach (var item in m_values)
                {
                    result += string.Format("{0}_", item.ToString());
                }
                return result.TrimEnd('_');
            }
        }

        public static string GetSortKey(string one, string two)
        {
            string result = string.Empty;
            UnitTool res = new UnitTool();
            res.SetValue(one);
            res.SetValue(two);
            result = res.GetSortField;
            return result;
        }

        /// <summary>
        /// 拆分组合字符串（2组合）
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="removeStr">排除字符</param>
        /// <param name="pef">分隔符</param>
        /// <returns></returns>
        public static string GetSortStrSplit(string str, string removeStr, char pef = '_')
        {
            string result = string.Empty;
            string[] list = str.Split(pef);
            if (list.Length == 2)
            {
                result = list[0].Equals(removeStr) ? list[1] : list[0];
            }
            return result;
        }

        #endregion 排序字符串

        #region xml操作

        /// <summary>
        /// 获取帮派升级经验
        /// </summary>
        /// <param name="lev"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<LevData> GetLevData()
        {
            var cacheService = App.GetService<IMemoryCache>();
            List<LevData> result = cacheService.Get<List<LevData>>("GenieLevData");
            if (result == null)
            {
                result = new List<LevData>();
                string path = ConfigHelper.BasicPath + "data/lev.xml";
                XmlDocument xml = new XmlDocument();
                xml.Load(path);
                XmlNodeList lv = xml.SelectNodes("/lvs/lv");
                foreach (XmlNode xs in lv)
                {
                    LevData temp = new LevData();
                    temp.lev = Convert.ToInt32(xs.Attributes["lev"].Value);
                    temp.name = xs.Attributes["name"].Value;
                    temp.role = xs.Attributes["role"].Value;
                    temp.tips = xs.Attributes["tips"].Value;
                    temp.value = Convert.ToInt32(xs.Attributes["value"].Value);
                    result.Add(temp);
                }
                cacheService.Set("GenieLevData", result);
            }
            return result;
        }

        public static LevData GetLevData(int exploit)
        {
            var data = GetLevData();

            var result = data.FindLast(it => exploit >= it.value);
            if (result == null)
            {
                result = new LevData();
            }
            return result;
        }

        #endregion xml操作

        #region 概率补充方法

        public static int RandomOkCount(int count, int chance)
        {
            int result = 0;
            for (int i = 0; i < count; i++)
            {
                if (RandomUtil.CheakRandom(chance))
                {
                    result++;
                }
            }
            return result;
        }

        #endregion 概率补充方法

        public static decimal ConventAttrValue(string attrCode, List<AttrItem> attrs, decimal unit)
        {
            decimal result = 0.00M;
            foreach (var item in attrs)
            {
                if (item.code.Contains(attrCode))
                {
                    if (item.compute.Equals(GameEnum.ComputeType.Ride.ToString()))
                    {
                        result += unit * Convert.ToDecimal(item.parameter);
                    }
                    else if (item.compute.Equals(GameEnum.ComputeType.Plus.ToString()))
                    {
                        result += Convert.ToDecimal(item.parameter);
                    }
                }
            }
            return result;
        }
    }
}