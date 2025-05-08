using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_arena_user
    {
        /// <summary>
        ///
        /// </summary>
        public genie_arena_user()
        {
        }

        private System.Int32 _uid;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 uid
        { get { return this._uid; } set { this._uid = value; } }

        private System.Int32? _isOn;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? isOn
        { get { return this._isOn; } set { this._isOn = value; } }

        private System.String _time;

        /// <summary>
        ///
        /// </summary>
        public System.String time
        { get { return this._time; } set { this._time = value; } }

        private System.Int32? _isPk;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? isPk
        { get { return this._isPk; } set { this._isPk = value; } }
    }
}