using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    /// 
    /// </summary>
    [Tenant("Business")]
    public class genie_dic
    {
        /// <summary>
        /// 
        /// </summary>
        public genie_dic()
        {
        }

        private System.String _code;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String code { get { return this._code; } set { this._code = value; } }

        private System.String _sign;
        /// <summary>
        /// 
        /// </summary>
        public System.String sign { get { return this._sign; } set { this._sign = value; } }

        private System.String _remark;
        /// <summary>
        /// 
        /// </summary>
        public System.String remark { get { return this._remark; } set { this._remark = value; } }
    }
}