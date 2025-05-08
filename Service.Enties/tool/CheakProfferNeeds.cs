using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Enties
{
    public class CheakProfferNeeds
    {
        private bool _result;

        public bool result
        { get { return this._result; } set { this._result = value; } }

        private System.String _tips;

        /// <summary>
        /// 0不可交易1可交易
        /// </summary>
        public System.String tips
        { get { return this._tips; } set { this._tips = value; } }

        private List<TowerProffer> _Needs;

        public List<TowerProffer> Needs
        { get { return this._Needs; } set { this._Needs = value; } }
    }
}