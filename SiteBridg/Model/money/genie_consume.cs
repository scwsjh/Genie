using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_consume
    {
        /// <summary>
        ///
        /// </summary>
        public genie_consume()
        {
        }

        private System.String _id;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id
        { get { return this._id; } set { this._id = value; } }

        private System.Int32? _type;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? type
        { get { return this._type; } set { this._type = value; } }

        private System.Decimal? _amount;

        /// <summary>
        ///
        /// </summary>
        public System.Decimal? amount
        { get { return this._amount; } set { this._amount = value; } }

        private System.DateTime? _addTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? addTime
        { get { return this._addTime; } set { this._addTime = value; } }

        private System.Int32? _userId;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? userId
        { get { return this._userId; } set { this._userId = value; } }

        private System.String _remark;

        /// <summary>
        ///
        /// </summary>
        public System.String remark
        { get { return this._remark; } set { this._remark = value; } }
    }
}