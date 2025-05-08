using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_user
    {
        /// <summary>
        ///
        /// </summary>
        public genie_user()
        {
        }

        private System.Int32 _uid;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 uid
        { get { return this._uid; } set { this._uid = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.String _notice;

        /// <summary>
        ///
        /// </summary>
        public System.String notice
        { get { return this._notice; } set { this._notice = value; } }

        private System.DateTime? _addTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? addTime
        { get { return this._addTime; } set { this._addTime = value; } }

        private System.Int32? _lev;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? lev
        { get { return this._lev; } set { this._lev = value; } }

        private System.Decimal? _exp;

        /// <summary>
        ///
        /// </summary>
        public System.Decimal? exp
        { get { return this._exp; } set { this._exp = value; } }

        private System.Int32? _poin;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? poin
        { get { return this._poin; } set { this._poin = value; } }

        private System.DateTime? _poinTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? poinTime
        { get { return this._poinTime; } set { this._poinTime = value; } }
    }
}