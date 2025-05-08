using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TencentCloud.Tci.V20190318.Models;

namespace Service.Core
{
    public class FightCompute
    {
        public static async Task<FightResult> Compute(FightTemp atk, FightTemp def, string fightType = "Person", List<TowerGet> award = null)
        {
            FightResult result = new FightResult();
            bool fig = true;
            int start = 0;//0攻击方,1失败方
            result.figState = new List<FightState>();
            while (fig)
            {
                int atkIndex = atk.genie.FindIndex(it => it.status == 1);
                int defIndex = def.genie.FindIndex(it => it.status == 1);
                if (atkIndex == -1 || defIndex == -1)
                {
                    if (defIndex == -1)
                    {
                        result.winId = atk.uid;
                        result.lowId = def.uid;
                    }
                    else if (atkIndex == -1)
                    {
                        result.winId = def.uid;
                        result.lowId = atk.uid;
                    }
                    fig = false;
                }

                if (fig)
                {
                    bool next = true;
                    FightState fiTip = new FightState();
                    fiTip.data = new List<string>();
                    while (next)
                    {
                        if (start == 0)
                        {
                            next = UnitFight(ref atk, atkIndex, ref def, defIndex, ref fiTip);
                            if (next)
                            {
                                next = UnitFight(ref def, defIndex, ref atk, atkIndex, ref fiTip);
                            }
                        }
                        else
                        {
                            next = UnitFight(ref def, defIndex, ref atk, atkIndex, ref fiTip);
                            if (next)
                            {
                                next = UnitFight(ref atk, atkIndex, ref def, defIndex, ref fiTip);
                            }
                        }
                    }
                    //失败先手
                    start = atk.genie[atkIndex].blood == 0 ? 0 : 1;
                    result.figState.Add(fiTip);
                }
            }
            if (fig == false)
            {
                var fightService = App.GetService<IGenieFightService>();
                //添加记录
                result.fightId = StringHelper.NewGuid;
                string atkJson = JsonConvert.SerializeObject(atk);
                string defJson = JsonConvert.SerializeObject(def);
                string logJson = JsonConvert.SerializeObject(result.figState);
                string awardJson = string.Empty;
                if (award != null)
                {
                    awardJson = JsonConvert.SerializeObject(award);
                }
                fig = await fightService.AddFight(result.fightId, atk.uid, def.uid, result.winId, result.lowId, atkJson, defJson, logJson, fightType, awardJson);
            }
            result.isOk = fig;
            if (result.isOk)
            {
                //扣除双方Buff
                var genUserService = App.GetService<IGenieUserService>();
                await genUserService.UpdateUserBuffCount(atk.uid, BuffEnum.BuffName.Fight.ToString(), 1);
                if (fightType == "Person")
                {
                    await genUserService.UpdateUserBuffCount(def.uid, BuffEnum.BuffName.Fight.ToString(), 1);
                }
            }

            return result;
        }

        private static bool UnitFight(ref FightTemp atk, int atkIndex, ref FightTemp def, int defIndex, ref FightState figlog)
        {
            int hurt = def.genie[defIndex].blood > atk.genie[atkIndex].charm ? atk.genie[atkIndex].charm : def.genie[defIndex].blood;
            def.genie[defIndex].blood -= hurt;
            string stateTips = string.Format("{0},生命-{1};[当前生命:{2}].", FightTips(atk.genie[atkIndex], def.genie[defIndex]), hurt, def.genie[defIndex].blood);
            figlog.data.Add(stateTips);

            bool result = def.genie[defIndex].blood > 0;
            if (result == false)
            {
                def.genie[defIndex].status = 0;
                figlog.data.Add(string.Format("[{0}]退出战斗.", GetGeneralName(def.genie[defIndex])));
            }
            return result;
        }

        private static string FightTips(GenieAttr atk, GenieAttr def)
        {
            string atkUbb = GetGeneralName(atk);
            string defUbb = GetGeneralName(def);
            List<string> data = new List<string>() {
        "{0}对{1}发出闪电般的攻击",
        "{0}轻松绕到{1}的背后发起了致命偷袭",
        "{0}狠狠的对{1}踢了一脚",
        "{0}轻松一个大摆拳,{1}顿时头脑混乱",
        "{0}抛了个媚眼,{1}瞬间被迷倒"
        };

            string tip = data[RandomHelper.GetFormatedNumeric(0, data.Count)];
            return string.Format(tip, atkUbb, defUbb);
        }

        private static string GetGeneralName(GenieAttr data)
        {
            string result = string.Empty;
            if (data.camp == FightEnum.Camp.Atk.ToString())
            {
                result = string.Format("[ATK={0}]", data.name);
            }
            else
            {
                result = string.Format("[DEF={0}]", data.name);
            }
            return result;
        }

        public static string LogTo(string sign)
        {
            if (string.IsNullOrEmpty(sign))
            {
                return "";
            }
            sign = Regex.Replace(sign, @"\[ATK=(?<name>.+?)]", "&nbsp;<span style=\"color:red\">${name}</span>&nbsp;");
            sign = Regex.Replace(sign, @"\[DEF=(?<name>.+?)]", "&nbsp;<span style=\"color:blue\">${name}</span>&nbsp;");
            return sign;
        }
    }
}