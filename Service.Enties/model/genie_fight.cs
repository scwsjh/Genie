using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_fight
    {
        /// <summary>
        ///
        /// </summary>
        public genie_fight()
        {
        }

        private System.String _id;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id
        { get { return this._id; } set { this._id = value; } }

        private System.Int32? _uid;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? uid
        { get { return this._uid; } set { this._uid = value; } }

        private System.Int32? _otId;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? otId
        { get { return this._otId; } set { this._otId = value; } }

        private System.Int32? _winId;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? winId
        { get { return this._winId; } set { this._winId = value; } }

        private System.Int32? _lowId;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? lowId
        { get { return this._lowId; } set { this._lowId = value; } }

        private System.String _atk;

        /// <summary>
        ///
        /// </summary>
        public System.String atk
        { get { return this._atk; } set { this._atk = value; } }

        private System.String _def;

        /// <summary>
        ///
        /// </summary>
        public System.String def
        { get { return this._def; } set { this._def = value; } }

        private System.String _log;

        /// <summary>
        ///
        /// </summary>
        public System.String log
        { get { return this._log; } set { this._log = value; } }

        private System.String _award;

        /// <summary>
        ///
        /// </summary>
        public System.String award
        { get { return this._award; } set { this._award = value; } }

        private System.String _type;

        /// <summary>
        ///
        /// </summary>
        public System.String type
        { get { return this._type; } set { this._type = value; } }

        private System.DateTime? _addTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? addTime
        { get { return this._addTime; } set { this._addTime = value; } }
    }
}