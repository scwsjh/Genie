using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Base")]
    public class wap_user_money_log
    {
        /// <summary>
        ///
        /// </summary>
        public wap_user_money_log()
        {
        }

        private System.Int32 _id;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsIdentity = true)]
        public System.Int32 id
        { get { return this._id; } set { this._id = value; } }

        private System.Int32? _uid;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? uid
        { get { return this._uid; } set { this._uid = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.Int32? _money;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? money
        { get { return this._money; } set { this._money = value; } }

        private System.Int32? _amount;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? amount
        { get { return this._amount; } set { this._amount = value; } }

        private System.Int32? _balance;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? balance
        { get { return this._balance; } set { this._balance = value; } }

        private System.String _remarks;

        /// <summary>
        ///
        /// </summary>
        public System.String remarks
        { get { return this._remarks; } set { this._remarks = value; } }

        private System.String _logpass;

        /// <summary>
        ///
        /// </summary>
        public System.String logpass
        { get { return this._logpass; } set { this._logpass = value; } }

        private System.DateTime? _addtime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? addtime
        { get { return this._addtime; } set { this._addtime = value; } }
    }
}