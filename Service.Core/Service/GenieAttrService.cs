using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TencentCloud.Cdn.V20180606.Models;

namespace Service.Core.Service
{
    public class GenieAttrService : IGenieAttrService, ITransient
    {
        private readonly IGenieTeamService teamService;
        public readonly IGenieService genieService;

        public GenieAttrService(IGenieTeamService teamService, IGenieService genieService)
        {
            this.teamService = teamService;
            this.genieService = genieService;
        }

        public async Task<List<GenieAttr>> GetTeamGenieAttr(int uid, string camp = "Atk", List<AttrItem> attrs = null)
        {
            List<GenieAttr> result = new List<GenieAttr>();
            var teamInfo = await teamService.GetUserTeam(uid);
            if (!string.IsNullOrEmpty(teamInfo.team))
            {
                var team = JsonConvert.DeserializeObject<List<TeamTemp>>(teamInfo.team);
                foreach (var item in team)
                {
                    GenieAttr temp = new GenieAttr();
                    temp.num = item.num;
                    temp.camp = camp;
                    temp.id = item.id;
                    temp.genId = item.genId;
                    temp.name = item.name;
                    var genInfo = await genieService.GetUserGenieInfoByAttr(item.id);
                    temp.lev = (int)genInfo.lev;
                    temp.start = (int)genInfo.start;
                    temp.charm = (int)genInfo.charm;
                    temp.blood = (int)genInfo.blood;
                    temp.status = 1;

                    if (attrs != null)
                    {
                        temp.charm += Convert.ToInt32(UnitTool.ConventAttrValue(GameEnum.AttrCode.Charm.ToString(), attrs, temp.charm));
                        temp.blood += Convert.ToInt32(UnitTool.ConventAttrValue(GameEnum.AttrCode.Blood.ToString(), attrs, temp.blood));
                    }

                    result.Add(temp);
                }
            }
            return result;
        }

        public async Task<FightTemp> GetUserFightTemp(int uid, string camp = "Atk", string nick = "")
        {
            FightTemp result = new FightTemp();
            result.uid = uid;
            result.nick = nick;
            result.camp = camp;

            //获取Buff
            List<AttrItem> attrs = new List<AttrItem>();
            var buffService = App.GetService<IGenieUserService>();
            var buffData = await buffService.GetUserBuff(uid);
            var OnBuff = buffData.FindAll(it => it.code == BuffEnum.BuffName.Fight.ToString());
            foreach (var item in OnBuff)
            {
                var temp = JsonConvert.DeserializeObject<List<AttrItem>>(item.attr);
                attrs.AddRange(temp);
            }

            result.genie = await GetTeamGenieAttr(uid, camp, attrs);

            return result;
        }
    }
}