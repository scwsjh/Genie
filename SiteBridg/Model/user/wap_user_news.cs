using SqlSugar;

namespace SiteBridg.Model
{
    [Tenant("Base")]
    public class wap_user_news
    {
        /// <summary>
        ///
        /// </summary>
        public wap_user_news()
        {
        }

        private System.Int32 _uid;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 uid
        { get { return this._uid; } set { this._uid = value; } }

        private System.Int32? _home;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? home
        { get { return this._home; } set { this._home = value; } }

        private System.Byte? _qnum;

        /// <summary>
        ///
        /// </summary>
        public System.Byte? qnum
        { get { return this._qnum; } set { this._qnum = value; } }

        private System.Byte? _mail;

        /// <summary>
        ///
        /// </summary>
        public System.Byte? mail
        { get { return this._mail; } set { this._mail = value; } }

        private System.Byte? _phone;

        /// <summary>
        ///
        /// </summary>
        public System.Byte? phone
        { get { return this._phone; } set { this._phone = value; } }
    }
}