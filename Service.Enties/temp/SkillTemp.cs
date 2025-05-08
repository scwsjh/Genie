using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Enties
{
    public class SkillTemp
    {
        public string name { get; set; }
        public string code { get; set; }
        public string tip { get; set; }
        public List<SkillTempResult> skill { get; set; }
    }

    public class SkillTempResult
    {
        public List<SkillTempBefor> befor { get; set; }
        public List<SkillTempEffect> effect { get; set; }
    }

    public class SkillTempBefor
    {
        public string code { get; set; }
        public string type { get; set; }
        public string par { get; set; }
        public decimal sign { get; set; }
    }

    public class SkillTempEffect
    {
        public string code { get; set; }
        public string compute { get; set; }
        public System.Decimal parameter { get; set; }
        public System.String tip { get; set; }
    }
}