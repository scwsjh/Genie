using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Base")]
    public class wap_ygw
    {
        /// <summary>
        ///
        /// </summary>
        public wap_ygw()
        {
        }

        private System.Int32 _uid;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 uid
        { get { return this._uid; } set { this._uid = value; } }

        private System.Int32 _point;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 point
        { get { return this._point; } set { this._point = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.String _going;

        /// <summary>
        ///
        /// </summary>
        public System.String going
        { get { return this._going; } set { this._going = value; } }

        private System.Byte? _status;

        /// <summary>
        ///
        /// </summary>
        public System.Byte? status
        { get { return this._status; } set { this._status = value; } }

        private System.Int32 _level;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 level
        { get { return this._level; } set { this._level = value; } }

        private System.Int32 _cx;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 cx
        { get { return this._cx; } set { this._cx = value; } }

        private System.Int32 _qh;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 qh
        { get { return this._qh; } set { this._qh = value; } }

        private System.Int32 _jsk;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 jsk
        { get { return this._jsk; } set { this._jsk = value; } }

        private System.Int32 _a1;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 a1
        { get { return this._a1; } set { this._a1 = value; } }

        private System.Int32 _a2;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 a2
        { get { return this._a2; } set { this._a2 = value; } }

        private System.Int32 _a3;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 a3
        { get { return this._a3; } set { this._a3 = value; } }

        private System.Int32 _a4;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 a4
        { get { return this._a4; } set { this._a4 = value; } }

        private System.Int32 _a5;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 a5
        { get { return this._a5; } set { this._a5 = value; } }

        private System.Int32 _a6;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 a6
        { get { return this._a6; } set { this._a6 = value; } }

        private System.Int32 _a7;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 a7
        { get { return this._a7; } set { this._a7 = value; } }

        private System.Int32? _csteal;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? csteal
        { get { return this._csteal; } set { this._csteal = value; } }

        private System.Int32 _mucks;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 mucks
        { get { return this._mucks; } set { this._mucks = value; } }

        private System.DateTime? _addtime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? addtime
        { get { return this._addtime; } set { this._addtime = value; } }

        private System.DateTime? _endtime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? endtime
        { get { return this._endtime; } set { this._endtime = value; } }

        private System.DateTime? _time;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? time
        { get { return this._time; } set { this._time = value; } }

        private System.Int32 _qha;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 qha
        { get { return this._qha; } set { this._qha = value; } }
    }
}