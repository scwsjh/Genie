using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Base")]
    public class wap_farm
    {
        /// <summary>
        ///
        /// </summary>
        public wap_farm()
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

        private System.Int32? _level;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? level
        { get { return this._level; } set { this._level = value; } }

        private System.Int32? _point;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? point
        { get { return this._point; } set { this._point = value; } }

        private System.Int32? _steal;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? steal
        { get { return this._steal; } set { this._steal = value; } }

        private System.Int32? _mucks;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? mucks
        { get { return this._mucks; } set { this._mucks = value; } }

        private System.Int32? _csteal;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? csteal
        { get { return this._csteal; } set { this._csteal = value; } }

        private System.DateTime? _addtime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? addtime
        { get { return this._addtime; } set { this._addtime = value; } }

        private System.DateTime? _newtime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? newtime
        { get { return this._newtime; } set { this._newtime = value; } }

        private System.DateTime? _endtime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? endtime
        { get { return this._endtime; } set { this._endtime = value; } }
    }
}