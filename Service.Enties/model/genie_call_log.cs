using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    /// 
    /// </summary>
    [Tenant("Business")]
    public class genie_call_log
    {
        /// <summary>
        /// 
        /// </summary>
        public genie_call_log()
        {
        }

        private System.String _id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id { get { return this._id; } set { this._id = value; } }

        private System.DateTime? _sTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? sTime { get { return this._sTime; } set { this._sTime = value; } }

        private System.DateTime? _eTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? eTime { get { return this._eTime; } set { this._eTime = value; } }
    }
}