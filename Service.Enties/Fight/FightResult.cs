using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Enties
{
    public class FightResult
    {
        public bool isOk { get; set; }
        public string fightId { get; set; }
        public int winId { get; set; }
        public int lowId { get; set; }
        public List<FightState> figState { get; set; }
    }

    public class FightState
    {
        public List<string> data { get; set; }
    }
}