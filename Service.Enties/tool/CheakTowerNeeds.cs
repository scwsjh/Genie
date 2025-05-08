using System.Collections.Generic;

namespace Service.Enties
{
    public class CheakTowerNeeds
    {
        private bool _result;

        public bool result
        { get { return this._result; } set { this._result = value; } }

        private System.Int32 _isDeal;

        /// <summary>
        /// 0不可交易1可交易
        /// </summary>
        public System.Int32 isDeal
        { get { return this._isDeal; } set { this._isDeal = value; } }

        private System.Int32 _isGive;

        /// <summary>
        /// 0不可交易1可交易
        /// </summary>
        public System.Int32 isGive
        { get { return this._isGive; } set { this._isGive = value; } }

        private System.String _tips;

        /// <summary>
        /// 0不可交易1可交易
        /// </summary>
        public System.String tips
        { get { return this._tips; } set { this._tips = value; } }

        private List<TowerNeeds> _Needs;

        public List<TowerNeeds> Needs
        { get { return this._Needs; } set { this._Needs = value; } }
    }
}