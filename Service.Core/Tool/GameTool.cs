using SiteBridg;
using SiteBridg.Service;
using SiteBridg.temp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public class GameTool
    {
        public static async Task<string> GetCurrencyName(string par)
        {
            string result = string.Empty;
            if (par.Contains("Site"))
            {
                string[] pars = par.Split('#');
                var wapUserService = App.GetService<IWapUserService>();
                var info = await wapUserService.GetMoneyInfo(Convert.ToInt32(pars[1]));
                if (info != null)
                {
                    result = info.name;
                }
            }
            return result;
        }

        public static int GetMaxGenieCount(int lev)
        {
            return GameConfig.GenieCount + lev;
        }

        public static decimal GetUserUpExp(int lev)
        {
            decimal result = 0M;
            if (lev < 200M)
            {
                result = 50M * ((lev * lev * lev) + (5 * lev)) - 80M;
            }
            else if (lev < 400)
            {
                result = 200M * ((lev * lev * lev) + (5 * lev)) - 80M;
            }
            else
            {
                result = 500M * ((lev * lev * lev) + (5 * lev)) - 80M;
            }
            return result;
        }

        #region 生成链接提示

        public static string GetGamePropHtml(string name, string code, string par, string sid, int type = 0)
        {
            string url = GetGamePropUrl(code, par, type);
            if (string.IsNullOrEmpty(url))
            {
                return name;
            }
            else
            {
                string link = url.Contains("?") ? "&" : "?";
                return string.Format("<a href='{0}{1}sid={2}'>{3}</a>", url, link, sid, name);
            }
        }

        public static string GetGamePropUrl(string code, string par, int type = 0)
        {
            string url = string.Empty;
            switch (code)
            {
                case "Genie":
                    url = type == 0 ? "/Map/Info/{0}" : "/Genie/Info?gu={0}";
                    break;

                case "Goods":
                    url = type == 0 ? "/Goods/Info/{0}" : "/Goods/Info/{0}";
                    break;
            }
            return string.Format(url, par);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Reward">内容</param>
        /// <param name="type">0无连接 1有链接</param>
        /// <param name="count">份额</param>
        /// <param name="sid">有链接时必传</param>
        /// <returns></returns>
        public static string GetPropTips(dynamic Reward, int type = 0, int count = 1, string sid = "")
        {
            string tips = string.Empty;
            foreach (var item in Reward)
            {
                decimal num = Convert.ToDecimal(item.count) * count;
                if (num <= 0)
                {
                    continue;
                }
                if (type == 0)
                {
                    tips += string.Format("{0}+{1},", item.name, num);
                }
                else
                {
                    tips += string.Format("<div class='n-item'>▸{0}+{1}</div>", GetGamePropHtml(item.name, item.code, item.parameter, sid), num);
                }
            }
            return tips.TrimEnd(',');
        }

        #endregion 生成链接提示

        #region 资源统一操作

        public static async Task<bool> UpdateBag(int uid, int operate, string goodsType, string parameter, decimal count, string remark)
        {
            bool isok = false;
            if (goodsType.Equals(GameEnum.PropCode.Site.ToString()))
            {
                isok = await WapSiteTool.OperateSiteProp(uid, parameter, operate, count, remark);
            }
            else if (goodsType.Equals(GameEnum.PropCode.Genie.ToString()))
            {
                var genieService = App.GetService<IGenieService>();
                if (operate == 1)
                {
                    isok = await genieService.AddUserGenie(uid, Convert.ToInt32(parameter), (int)count);
                }
            }
            else if (goodsType.Equals(GameEnum.PropCode.Goods.ToString()))
            {
                var goodsService = App.GetService<IGenieGoodsService>();
                isok = await goodsService.UpdateUserGoods(uid, operate, Convert.ToInt32(parameter), (int)count, remark);
            }
            else if (goodsType.Equals(GameEnum.PropCode.Exp.ToString()))
            {
                var userService = App.GetService<IGenieUserService>();
                isok = await userService.UpdateUserExp(uid, (int)count, operate);
            }
            else if (goodsType.Equals(GameEnum.PropCode.copper.ToString()))
            {
                var accService = App.GetService<IGenieAccService>();
                isok = await accService.UpdateUserAcc(uid, operate, AccEnum.AccType.copper.ToString(), (int)count, remark);
            }
            else if (goodsType.Equals(GameEnum.PropCode.exploit.ToString()))
            {
                var accService = App.GetService<IGenieAccService>();
                isok = await accService.UpdateUserAcc(uid, operate, AccEnum.AccType.exploit.ToString(), (int)count, remark);
            }
            return isok;
        }

        public static async Task<bool> UpdateBagBatch(int uid, int op, dynamic need, string remark = "", bool isRet = false, int count = 1)
        {
            try
            {
                foreach (var item in need)
                {
                    decimal num = isRet ? Convert.ToInt32(item.retCount) : Convert.ToInt32(item.count);
                    if (num <= 0)
                    {
                        continue;
                    }
                    if (item.isOp == 0 && op == 0)
                    {
                        continue;
                    }
                    num = num * count;
                    await UpdateBag(uid, op, item.code, item.parameter, num, remark);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> PutPropBatch(int uid, int op, dynamic Reward, string remark = "", int count = 1)
        {
            try
            {
                foreach (var item in Reward)
                {
                    decimal num = Convert.ToInt32(item.count) * count;
                    if (num <= 0)
                    {
                        continue;
                    }
                    await UpdateBag(uid, op, item.code, item.parameter, num, remark);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<CheakTowerNeed> CheakNeed(int uid, TowerNeed item, string sid, int count = 1)
        {
            CheakTowerNeed result = new CheakTowerNeed();
            bool isok = true;
            string tips = string.Empty;

            if (item.code == GameEnum.PropCode.Site.ToString())
            {
                var siteRes = await WapSiteTool.CheakNeed(uid, item.parameter, item.name, item.count, count);
                isok = siteRes.isOk;
                tips = siteRes.tips;
            }
            else if (item.code == GameEnum.PropCode.Goods.ToString())
            {
                int MyCount = 0;
                var goodsService = App.GetService<IGenieGoodsService>();
                var goodsInfo = await goodsService.GetUserGoodsInfo(uid, Convert.ToInt32(item.parameter));
                if (goodsInfo != null)
                {
                    MyCount = (int)goodsInfo.count;
                }
                int neecCount = item.count * count;
                if (MyCount < neecCount)
                {
                    isok = false;
                }
                tips += string.Format("<div class='n-item'>▸{0}:{1}/{2}{3}", GetGamePropHtml(item.name, item.code, item.parameter, sid), MyCount, neecCount, MyCount < neecCount ? "<span class='text-red'>(缺少)</span>" : "");
                tips += "</div>";
            }
            else if (item.code == GameEnum.PropCode.lev.ToString())
            {
                var userService = App.GetService<IGenieUserService>();
                var userInfo = await userService.GetGenieUserInfo(uid);
                if (userInfo.lev < item.count)
                {
                    isok = false;
                }
                tips += string.Format("<div class='n-item'>▸{0}:{1}/{2}{3}", GetGamePropHtml(item.name, item.code, item.parameter, sid), userInfo.lev, item.count, userInfo.lev < item.count ? "<span class='text-red'>(不满足)</span>" : "");
                tips += "</div>";
            }
            else if (item.code == GameEnum.PropCode.copper.ToString())
            {
                var accService = App.GetService<IGenieAccService>();
                var accInfo = await accService.GetAccInfo(uid);
                if (accInfo.copper < item.count)
                {
                    isok = false;
                }
                tips += string.Format("<div class='n-item'>▸{0}:{1}/{2}{3}", GetGamePropHtml(item.name, item.code, item.parameter, sid), accInfo.copper, item.count, accInfo.copper < item.count ? "<span class='text-red'>(不满足)</span>" : "");
                tips += "</div>";
            }
            else if (item.code == GameEnum.PropCode.exploit.ToString())
            {
                var accService = App.GetService<IGenieAccService>();
                var accInfo = await accService.GetAccInfo(uid);
                if (accInfo.exploit < item.count)
                {
                    isok = false;
                }
                tips += string.Format("<div class='n-item'>▸{0}:{1}/{2}{3}", GetGamePropHtml(item.name, item.code, item.parameter, sid), accInfo.exploit, item.count, accInfo.exploit < item.count ? "<span class='text-red'>(不满足)</span>" : "");
                tips += "</div>";
            }

            result.result = isok;
            result.tips = tips;
            return result;
        }

        public static async Task<CheakTowerNeed> CheakNeed(int uid, List<TowerNeed> needs, string sid, int count = 1)
        {
            CheakTowerNeed result = new CheakTowerNeed();
            bool isok = true;
            string tips = string.Empty;
            foreach (var item in needs)
            {
                var cheakResult = await CheakNeed(uid, item, sid, count);
                if (isok)
                {
                    isok = cheakResult.result;
                }
                tips += cheakResult.tips;
            }

            result.result = isok;
            result.tips = tips;
            result.Needs = needs;
            return result;
        }

        #endregion 资源统一操作

        #region 随机类操作

        public static List<TowerGet> CreatePropByPack(int uid, string opName, string opPar, game_pack packData, int count = 1, decimal luckAdd = 0)
        {
            luckAdd = luckAdd / 100;
            List<TowerGet> ResGet = new List<TowerGet>();
            for (int i = 0; i < count; i++)
            {
                if (!RandomUtil.CheakRandom(packData.chance))
                {
                    continue;
                }
                int onCount = RandomSugar.GetFormatedNumeric(Convert.ToInt32(packData.minCount), Convert.ToInt32(packData.maxCount) + 1);
                onCount = Convert.ToInt32(onCount * (1.0M + luckAdd));
                var resData = RandomLimitByPack(uid, opName, opPar, packData.items, onCount);
                foreach (var _Award in resData)
                {
                    bool isAdd = true;
                    foreach (var _main in ResGet)
                    {
                        if (_main.code == _Award.code && _main.parameter == _Award.parameter)
                        {
                            isAdd = false;
                            _main.count = _main.count + _Award.count;
                        }
                    }
                    if (isAdd)//添加
                    {
                        TowerGet add = new TowerGet();
                        add.code = _Award.code;
                        add.count = _Award.count;
                        add.name = _Award.name;
                        add.parameter = _Award.parameter;
                        ResGet.Add(add);
                    }
                }
            }
            return ResGet;
        }

        private static List<TowerGet> RandomLimitByPack(int uid, string opName, string opPar, List<game_pack_item> Reward, int count)
        {
            List<TowerGet> resultData = new List<TowerGet>();
            List<object> data = new List<object>();
            List<ushort> weights = new List<ushort>();
            List<dynamic> result = new List<dynamic>();
            count = count > Reward.Count ? Reward.Count : count;
            if (count > 0)
            {
                //随机数生成器
                Random rand = new Random();
                foreach (var item in Reward)
                {
                    data.Add(item);
                    weights.Add(ushort.Parse(item.random.ToString()));
                }
                RandomTool random = new RandomTool(count);
                result = random.UnitControllerRandomExtractLimit(rand, data);

                //获得物品
                foreach (var item in result)
                {
                    int getCount = RandomHelper.GetFormatedNumeric(Convert.ToInt32(item.minCount), Convert.ToInt32(item.maxCount) + 1);
                    TowerGet add = new TowerGet();
                    add.code = item.code;
                    add.name = item.name;
                    add.parameter = item.parameter;
                    add.count = getCount;
                    resultData.Add(add);
                }
            }
            return resultData;
        }

        #endregion 随机类操作

        #region 杂项辅助

        public static string FightUbb(string logId)
        {
            string url = $"[HREF=/Fight/FightLog?log={logId}]查看战况[/HREF]";
            return url;
        }

        public static string HomeUbb(int uid, string nick)
        {
            string url = $"[HREF=/Index/Home?id={uid}]{nick}[/HREF]";
            return url;
        }

        #endregion 杂项辅助
    }
}