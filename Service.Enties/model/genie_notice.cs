using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_notice
    {
        /// <summary>
        ///
        /// </summary>
        public genie_notice()
        {
        }

        private System.String _noticeId;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String noticeId
        { get { return this._noticeId; } set { this._noticeId = value; } }

        private System.String _title;

        /// <summary>
        ///
        /// </summary>
        public System.String title
        { get { return this._title; } set { this._title = value; } }

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

        private System.DateTime? _endTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? endTime
        { get { return this._endTime; } set { this._endTime = value; } }
    }
}