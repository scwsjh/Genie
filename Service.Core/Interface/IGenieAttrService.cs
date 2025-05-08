using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public interface IGenieAttrService
    {
        Task<List<GenieAttr>> GetTeamGenieAttr(int uid, string camp = "Atk", List<AttrItem> attrs = null);

        Task<FightTemp> GetUserFightTemp(int uid, string camp = "Atk", string nick = "");
    }
}