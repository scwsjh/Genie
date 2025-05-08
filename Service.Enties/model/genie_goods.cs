using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_goods
    {
        /// <summary>
        ///
        /// </summary>
        public genie_goods()
        {
        }

        private System.Int32? _goodsId;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
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

        private System.String _img;

        /// <summary>
        ///
        /// </summary>
        public System.String img
        { get { return this._img; } set { this._img = value; } }

        private System.String _tips;

        /// <summary>
        ///
        /// </summary>
        public System.String tips
        { get { return this._tips; } set { this._tips = value; } }

        private System.String _local;

        /// <summary>
        ///
        /// </summary>
        public System.String local
        { get { return this._local; } set { this._local = value; } }

        private System.String _content;

        /// <summary>
        ///
        /// </summary>
        public System.String content
        { get { return this._content; } set { this._content = value; } }
    }
}