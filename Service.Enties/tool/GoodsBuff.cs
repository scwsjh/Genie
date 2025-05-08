using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Enties
{
    public class GoodsBuff
    {
        public int goodsId { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public decimal sign { get; set; }
        public List<AttrItem> attr { get; set; }
        public string img { get; set; }
        public int count { get; set; }
        public string tip { get; set; }
    }
}