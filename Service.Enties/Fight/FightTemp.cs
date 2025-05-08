using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Enties
{
    public class FightTemp
    {
        public int uid { get; set; }
        public string nick { get; set; }
        public string camp { get; set; }
        public List<GenieAttr> genie { get; set; }
    }
}