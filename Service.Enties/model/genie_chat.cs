using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_chat
    {
        /// <summary>
        ///
        /// </summary>
        public genie_chat()
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

        private System.Int32? _toUser;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? toUser
        { get { return this._toUser; } set { this._toUser = value; } }

        private System.String _code;

        /// <summary>
        ///
        /// </summary>
        public System.String code
        { get { return this._code; } set { this._code = value; } }

        private System.String _sign;

        /// <summary>
        ///
        /// </summary>
        public System.String sign
        { get { return this._sign; } set { this._sign = value; } }

        private System.DateTime? _addTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? addTime
        { get { return this._addTime; } set { this._addTime = value; } }

        private System.String _nick;

        /// <summary>
        ///
        /// </summary>
        public System.String nick
        { get { return this._nick; } set { this._nick = value; } }
    }
}