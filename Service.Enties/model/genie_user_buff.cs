using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_user_buff
    {
        /// <summary>
        ///
        /// </summary>
        public genie_user_buff()
        {
        }

        private System.String _ubId;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String ubId
        { get { return this._ubId; } set { this._ubId = value; } }

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

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.String _code;

        /// <summary>
        ///
        /// </summary>
        public System.String code
        { get { return this._code; } set { this._code = value; } }

        private System.Decimal? _sign;

        /// <summary>
        ///
        /// </summary>
        public System.Decimal? sign
        { get { return this._sign; } set { this._sign = value; } }

        private System.String _attr;

        /// <summary>
        ///
        /// </summary>
        public System.String attr
        { get { return this._attr; } set { this._attr = value; } }

        private System.Int32? _count;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? count
        { get { return this._count; } set { this._count = value; } }

        private System.String _img;

        /// <summary>
        ///
        /// </summary>
        public System.String img
        { get { return this._img; } set { this._img = value; } }

        private System.String _tip;

        /// <summary>
        ///
        /// </summary>
        public System.String tip
        { get { return this._tip; } set { this._tip = value; } }
    }
}