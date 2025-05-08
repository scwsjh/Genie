using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_mall
    {
        /// <summary>
        ///
        /// </summary>
        public genie_mall()
        {
        }

        private System.String _mallId;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String mallId
        { get { return this._mallId; } set { this._mallId = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.String _type;

        /// <summary>
        ///
        /// </summary>
        public System.String type
        { get { return this._type; } set { this._type = value; } }

        private System.String _goodsId;

        /// <summary>
        ///
        /// </summary>
        public System.String goodsId
        { get { return this._goodsId; } set { this._goodsId = value; } }

        private System.Decimal? _price;

        /// <summary>
        ///
        /// </summary>
        public System.Decimal? price
        { get { return this._price; } set { this._price = value; } }

        private System.String _payType;

        /// <summary>
        ///
        /// </summary>
        public System.String payType
        { get { return this._payType; } set { this._payType = value; } }

        private System.String _remark;

        /// <summary>
        ///
        /// </summary>
        public System.String remark
        { get { return this._remark; } set { this._remark = value; } }

        private System.Int32? _sort;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? sort
        { get { return this._sort; } set { this._sort = value; } }

        private System.DateTime? _addTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? addTime
        { get { return this._addTime; } set { this._addTime = value; } }

        private System.DateTime? _endTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? endTime
        { get { return this._endTime; } set { this._endTime = value; } }
    }
}