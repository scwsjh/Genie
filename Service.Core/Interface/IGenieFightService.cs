using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public interface IGenieFightService
    {
        Task<bool> AddFight(string id, int uid, int otid, int winId, int lowId, string atk, string def, string log, string type, string award = "");

        Task<genie_fight> GetFightInfo(string id);
    }
}