using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion;
using SiteBridg.Enum;
using SiteBridg.temp;
using TencentCloud.Ame.V20190916.Models;

namespace SiteBridg
{
    public class WapSiteTool
    {
        public static async Task<bool> OperateSiteProp(int uid, string parameter, int operate, decimal count, string remark)
        {
            bool isok = false;
            string[] pars = parameter.Split('#');
            string code = pars[0];
            if (code == GoodsEnum.PropCode.Flower.ToString())
            {
                string par = pars[1];
                var gardenService = App.GetService<SiteBridg.Service.IWapGardenService>();
                isok = await gardenService.UpdateUserFlower(uid, Convert.ToInt32(par), operate, (int)count);
            }
            else if (code == GoodsEnum.PropCode.GardenExp.ToString())
            {
                var gardenService = App.GetService<SiteBridg.Service.IWapGardenService>();
                isok = await gardenService.UpdateGardenExp(uid, operate, (int)count);
            }
            else if (code == GoodsEnum.PropCode.GardenSend.ToString())
            {
                int par = Convert.ToInt32(pars[1]);
                var gardenService = App.GetService<SiteBridg.Service.IWapGardenService>();
                isok = await gardenService.UpdateUserGardenSend(uid, operate, par, (int)count);
            }
            else if (code == GoodsEnum.PropCode.GardenProp.ToString())
            {
                int par = Convert.ToInt32(pars[1]);
                var gardenService = App.GetService<SiteBridg.Service.IWapGardenService>();
                isok = await gardenService.UpdateUserGardenProp(uid, operate, par, (int)count);
            }
            else if (code == GoodsEnum.PropCode.Currency.ToString())
            {
                int par = Convert.ToInt32(pars[1]);
                var userService = App.GetService<SiteBridg.Service.IWapUserService>();
                isok = await userService.UpdateUserMoney(uid, par, operate, (int)count, "精灵大乐斗", remark, "");
            }
            else if (code == GoodsEnum.PropCode.FarmExp.ToString())
            {
                var farmService = App.GetService<Service.IWapFarmService>();
                isok = await farmService.UpdateFramExp(uid, operate, (int)count);
            }
            else if (code == GoodsEnum.PropCode.FarmSeed.ToString())
            {
                int par = Convert.ToInt32(pars[1]);
                var farmService = App.GetService<Service.IWapFarmService>();
                isok = await farmService.UpdateFarmBag(uid, 1, operate, par, (int)count);
            }
            else if (code == GoodsEnum.PropCode.FarmMuck.ToString())
            {
                int par = Convert.ToInt32(pars[1]);
                var farmService = App.GetService<Service.IWapFarmService>();
                isok = await farmService.UpdateFarmBag(uid, 2, operate, par, (int)count);
            }
            else if (code == GoodsEnum.PropCode.FarmTrap.ToString())
            {
                int par = Convert.ToInt32(pars[1]);
                var farmService = App.GetService<Service.IWapFarmService>();
                isok = await farmService.UpdateFarmBag(uid, 3, operate, par, (int)count);
            }
            else if (code == GoodsEnum.PropCode.FarmGoods.ToString())
            {
                int par = Convert.ToInt32(pars[1]);
                var farmService = App.GetService<Service.IWapFarmService>();
                isok = await farmService.UpdateFarmBag(uid, 11, operate, par, (int)count);
            }
            else if (code == GoodsEnum.PropCode.RanchExp.ToString())
            {
                var ranchService = App.GetService<Service.IWapRanchService>();
                isok = await ranchService.UpdateRanchExp(uid, operate, (int)count);
            }
            else if (code == GoodsEnum.PropCode.RanchSeed.ToString())
            {
                int par = Convert.ToInt32(pars[1]);
                var ranchService = App.GetService<Service.IWapRanchService>();
                isok = await ranchService.UpdateRanchBag(uid, 1, operate, par, (int)count);
            }
            else if (code == GoodsEnum.PropCode.RanchMuck.ToString())
            {
                int par = Convert.ToInt32(pars[1]);
                var ranchService = App.GetService<Service.IWapRanchService>();
                isok = await ranchService.UpdateRanchBag(uid, 2, operate, par, (int)count);
            }
            else if (code == GoodsEnum.PropCode.RanchGoods.ToString())
            {
                int par = Convert.ToInt32(pars[1]);
                var ranchService = App.GetService<Service.IWapRanchService>();
                isok = await ranchService.UpdateRanchBag(uid, 11, operate, par, (int)count);
            }
            else if (code == GoodsEnum.PropCode.BbsExp.ToString())
            {
                var userService = App.GetService<Service.IWapUserService>();
                isok = await userService.UpdateUserBbsExp(uid, operate, (int)count);
            }
            return isok;
        }

        public static async Task<SiteCheakTemp> CheakNeed(int uid, string parameter, string name, int onCount, int count = 1)
        {
            SiteCheakTemp retData = new SiteCheakTemp();
            retData.isOk = true;
            retData.tips = string.Empty;
            string[] pars = parameter.Split('#');
            string code = pars[0];
            if (code == GoodsEnum.PropCode.Flower.ToString())
            {
                string par = pars[1];
                var gardenService = App.GetService<SiteBridg.Service.IWapGardenService>();
                long MyCount = await gardenService.GetUserFlowerCount(uid, Convert.ToInt32(par));

                int neecCount = onCount * count;
                if (MyCount < neecCount)
                {
                    retData.isOk = false;
                }
                retData.tips += string.Format("<div class='row'>▸{0}:{1}/{2}{3}", name, MyCount, neecCount, MyCount < neecCount ? "<span style='color:red;'>(不满足)</span>" : "");
                retData.tips += "</div>";
            }
            else if (code == GoodsEnum.PropCode.Currency.ToString())
            {
                string par = pars[1];
                var userService = App.GetService<SiteBridg.Service.IWapUserService>();
                int MyCount = await userService.GetUserMoneyCount(uid, Convert.ToInt32(par));

                int neecCount = onCount * count;
                if (MyCount < neecCount)
                {
                    retData.isOk = false;
                }
                retData.tips += string.Format("<div class='row'>▸{0}:{1}/{2}{3}", name, MyCount, neecCount, MyCount < neecCount ? "<span style='color:red;'>(不满足)</span>" : "");
                retData.tips += "</div>";
            }

            return retData;
        }
    }
}