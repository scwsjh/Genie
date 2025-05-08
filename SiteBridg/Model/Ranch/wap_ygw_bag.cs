using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Base")]
    public class wap_ygw_bag
    {
        /// <summary>
        ///
        /// </summary>
        public wap_ygw_bag()
        {
        }

        private System.Int32 _ID;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 ID
        { get { return this._ID; } set { this._ID = value; } }

        private System.Int32? _uid;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? uid
        { get { return this._uid; } set { this._uid = value; } }

        private System.Int32? _oid;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? oid
        { get { return this._oid; } set { this._oid = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.Int32? _dtype;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? dtype
        { get { return this._dtype; } set { this._dtype = value; } }

        private System.Int32? _amount;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? amount
        { get { return this._amount; } set { this._amount = value; } }
    }
}