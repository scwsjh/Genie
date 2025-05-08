using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_user_goods
    {
        /// <summary>
        ///
        /// </summary>
        public genie_user_goods()
        {
        }

        private System.String _ugId;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String ugId
        { get { return this._ugId; } set { this._ugId = value; } }

        private System.Int32? _userId;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? userId
        { get { return this._userId; } set { this._userId = value; } }

        private System.Int32? _goodsId;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? goodsId
        { get { return this._goodsId; } set { this._goodsId = value; } }

        private System.String _goodsName;

        /// <summary>
        ///
        /// </summary>
        public System.String goodsName
        { get { return this._goodsName; } set { this._goodsName = value; } }

        private System.Int32? _lev;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? lev
        { get { return this._lev; } set { this._lev = value; } }

        private System.String _code;

        /// <summary>
        ///
        /// </summary>
        public System.String code
        { get { return this._code; } set { this._code = value; } }

        private System.Int32? _count;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? count
        { get { return this._count; } set { this._count = value; } }
    }
}