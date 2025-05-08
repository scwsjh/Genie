using Furion.ClayObject;

namespace Service.Core
{
    public class RandomTool
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="count">随机抽取个数</param>
        public RandomTool(int count)
        {
            _Count = count;
        }

        private int _Count;

        /// <summary>
        /// 随机抽取个数
        /// </summary>
        public int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                _Count = value;
            }
        }

        public List<T> UnitRandomExtract<T>(Random rand, List<T> datas)
        {
            int nItemCount = datas.Count;
            List<T> result = new List<T>();
            if (rand != null)
            {
                //临时变量
                Dictionary<object, int> dict = new Dictionary<object, int>(nItemCount);

                //为每个项算一个随机数并乘以相应的权值
                for (int i = datas.Count - 1; i >= 0; i--)
                {
                    dict.Add(datas[i], rand.Next(100));
                }

                //排序
                List<KeyValuePair<dynamic, int>> listDict = SortByValue(dict);

                Count = Count > listDict.Count ? listDict.Count : Count;
                //拷贝抽取权值最大的前Count项
                foreach (KeyValuePair<dynamic, int> kvp in listDict.GetRange(0, Count))
                {
                    result.Add(kvp.Key);
                }
            }
            return result;
        }

        public List<T> UnitControllerRandomExtract<T>(Random rand, List<T> datas)
        {
            int nItemCount = datas.Count;
            List<T> result = new List<T>();
            if (rand != null)
            {
                //临时变量
                Dictionary<object, int> dict = new Dictionary<object, int>(nItemCount);

                //为每个项算一个随机数并乘以相应的权值
                for (int i = datas.Count - 1; i >= 0; i--)
                {
                    dynamic item = Clay.Object(datas[i]);
                    dict.Add(datas[i], rand.Next(100) * Convert.ToInt32(item.random));
                }

                //排序
                List<KeyValuePair<dynamic, int>> listDict = SortByValue(dict);

                Count = Count > listDict.Count ? listDict.Count : Count;
                //拷贝抽取权值最大的前Count项
                foreach (KeyValuePair<dynamic, int> kvp in listDict.GetRange(0, Count))
                {
                    result.Add(kvp.Key);
                }
            }
            return result;
        }

        public List<T> UnitControllerRandomExtractLimit<T>(Random rand, List<T> datas)
        {
            int nItemCount = datas.Count;
            List<T> result = new List<T>();
            if (rand != null)
            {
                //临时变量
                Dictionary<object, int> dict = new Dictionary<object, int>(nItemCount);

                int onCanGet = 0;
                //为每个项算一个随机数并乘以相应的权值
                for (int i = datas.Count - 1; i >= 0; i--)
                {
                    dynamic item = Clay.Object(datas[i]);
                    int onRadio = 0;
                    if (Convert.ToInt32(item.random) > 0)
                    {
                        onRadio = Convert.ToInt32(item.random);
                        onCanGet++;
                    }
                    else
                    {
                        onRadio = item.must;
                    }
                    dict.Add(datas[i], rand.Next(100) * onRadio);
                }

                //排序
                List<KeyValuePair<dynamic, int>> listDict = SortByValue(dict);

                Count = Count > onCanGet ? onCanGet : Count;
                Count = Count > listDict.Count ? listDict.Count : Count;
                //拷贝抽取权值最大的前Count项
                foreach (KeyValuePair<dynamic, int> kvp in listDict.GetRange(0, Count))
                {
                    result.Add(kvp.Key);
                }
            }
            return result;
        }

        /// <summary>
        /// 限量抽取
        /// </summary>
        /// <param name="code"></param>
        /// <param name="InKey"></param>
        /// <param name="rand"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public List<dynamic> ControllerRandomExtractLimit(string userId, string InKey, Random rand, List<dynamic> datas)
        {
            int nItemCount = datas.Count;
            List<object> result = new List<object>();
            if (rand != null)
            {
                //缓存记录服务
                var redis = App.GetService<IRedisCache>();
                //var goodsLimit = App.GetService<IGameToolService>();

                //临时变量
                Dictionary<object, int> dict = new Dictionary<object, int>(nItemCount);

                int onCanGet = 0;
                //为每个项算一个随机数并乘以相应的权值
                for (int i = datas.Count - 1; i >= 0; i--)
                {
                    int onRadio = 0;
                    if (Convert.ToInt32(datas[i].random) > 0)
                    {
                        onRadio = datas[i].random;
                        onCanGet++;
                    }
                    else
                    {
                        onRadio = datas[i].must;
                    }
                    //if (datas[i].type != "Default")//限量物品
                    //{
                    //    string _InKey = string.Format("{0}_{1}_{2}", InKey, datas[i].code, datas[i].parameter);
                    //    if (await goodsLimit.GetGameGoodsLimitCount(_InKey) < Convert.ToInt32(datas[i].onCount))
                    //    {
                    //        dict.Add(datas[i], rand.Next(100) * onRadio);
                    //    }
                    //}
                    //else
                    //{
                    //    dict.Add(datas[i], rand.Next(100) * onRadio);
                    //}
                    dict.Add(datas[i], rand.Next(100) * onRadio);
                }

                //排序
                List<KeyValuePair<dynamic, int>> listDict = SortByValue(dict);

                Count = Count > onCanGet ? onCanGet : Count;
                Count = Count > listDict.Count ? listDict.Count : Count;
                //拷贝抽取权值最大的前Count项
                foreach (KeyValuePair<dynamic, int> kvp in listDict.GetRange(0, Count))
                {
                    if (kvp.Key.type == "Default")
                    {
                        result.Add(kvp.Key);
                    }
                    else
                    {
                        //string _InKey = string.Format("{0}_{1}_{2}", InKey, kvp.Key.code, kvp.Key.parameter);
                        //if (await goodsLimit.AddGameGoodsLimit(_InKey, kvp.Key.type, kvp.Key.code, kvp.Key.parameter, 1, userId))
                        //{
                        //    result.Add(kvp.Key);
                        //}
                        result.Add(kvp.Key);
                    }
                }
            }
            return result;
        }

        #region Tools

        /// <summary>
        /// 排序集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        private List<KeyValuePair<object, int>> SortByValue(Dictionary<object, int> dict)
        {
            List<KeyValuePair<object, int>> list = new List<KeyValuePair<object, int>>();

            if (dict != null)
            {
                list.AddRange(dict);

                list.Sort(
                  delegate (KeyValuePair<object, int> kvp1, KeyValuePair<object, int> kvp2)
                  {
                      return kvp2.Value - kvp1.Value;
                  });
            }
            return list;
        }

        #endregion Tools

        #region 绝对随机控制

        public static async Task<bool> RandomCheak(string userId, decimal random)
        {
            bool isok = false;
            if (random > 0)
            {
                List<int> radmonList = await GetUserRandom(userId);
                var onIndex = RandomSugar.GetFormatedNumeric(0, radmonList.Count - 1);
                int result = radmonList[onIndex];
                if (result <= Convert.ToInt32(random * 100))
                {
                    isok = true;
                    await ClearRandom(userId);
                }
                else
                {
                    //清理随机
                    radmonList.RemoveAt(onIndex);
                    await UpdateRandom(userId, radmonList);
                }
            }
            return isok;
        }

        public static async Task<bool> RandomCheak(string r_key, int random)
        {
            bool isok = false;
            if (random > 0)
            {
                List<int> radmonList = await GetUserRandom(r_key);
                var onIndex = RandomSugar.GetFormatedNumeric(0, radmonList.Count - 1);
                int result = radmonList[onIndex];
                if (result <= random)
                {
                    isok = true;
                    await ClearRandom(r_key);
                }
                else
                {
                    //清理随机
                    radmonList.RemoveAt(onIndex);
                    await UpdateRandom(r_key, radmonList);
                }
            }
            return isok;
        }

        private static async Task<List<int>> GetUserRandom(string r_key)
        {
            var redis = App.GetService<IRedisCache>();
            string key = string.Format("RANDOM:{0}", r_key);
            List<int> data = await redis.GetAsync<List<int>>(key);
            if (data == null || data.Count == 0)
            {
                data = new List<int>();
                for (int i = 1; i <= 100; i++)
                {
                    data.Add(i);
                }
                await redis.SetAsync(key, data, 3600);
            }
            return data;
        }

        private static async Task UpdateRandom(string r_key, List<int> data)
        {
            var redis = App.GetService<IRedisCache>();
            string key = string.Format("RANDOM:{0}", r_key);
            await redis.SetAsync(key, data, 3600);
        }

        public static async Task ClearRandom(string r_key)
        {
            var redis = App.GetService<IRedisCache>();
            string key = string.Format("RANDOM:{0}", r_key);
            await redis.DelAsync(key);
        }

        #endregion 绝对随机控制

        #region 活动抽奖随机抽取

        public List<dynamic> GetDrawRandomResult(string userId, int id, Random rand, List<dynamic> datas, int onCount)
        {
            int nItemCount = datas.Count;
            List<object> result = new List<object>();
            if (rand != null)
            {
                //临时变量
                Dictionary<object, int> dict = new Dictionary<object, int>(nItemCount);

                //为每个项算一个随机数并乘以相应的权值
                for (int i = datas.Count - 1; i >= 0; i--)
                {
                    int onRadio = datas[i].random;
                    if (datas[i].oktime > 0)
                    {
                        if (onCount >= datas[i].oktime)
                        {
                            dict.Add(datas[i], rand.Next(1, 10) * 2000000);
                        }
                        else
                        {
                            dict.Add(datas[i], rand.Next(1000) * onRadio);
                        }
                    }
                    else
                    {
                        dict.Add(datas[i], rand.Next(1000) * onRadio);
                    }
                }

                //排序
                List<KeyValuePair<dynamic, int>> listDict = SortByValue(dict);
                //拷贝抽取权值最大的前Count项
                foreach (KeyValuePair<dynamic, int> kvp in listDict.GetRange(0, Count))
                {
                    result.Add(kvp.Key);
                }
            }
            return result;
        }

        #endregion 活动抽奖随机抽取
    }
}