using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_exchange_log
    {
        /// <summary>
        ///
        /// </summary>
        public genie_exchange_log()
        {
        }

        private System.String _id;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id
        { get { return this._id; } set { this._id = value; } }

        private System.Int32? _userId;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? userId
        { get { return this._userId; } set { this._userId = value; } }

        private System.Int32? _exId;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? exId
        { get { return this._exId; } set { this._exId = value; } }

        private System.Int32? _count;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? count
        { get { return this._count; } set { this._count = value; } }

        private System.DateTime? _addTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? addTime
        { get { return this._addTime; } set { this._addTime = value; } }
    }
}