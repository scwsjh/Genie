using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Enties
{
    public class SkillAttr
    {
        public string id { get; set; }
        public int skId { get; set; }
        public int exp { get; set; }
        public SkillItem attr { get; set; }
    }
}