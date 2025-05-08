using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    /// 
    /// </summary>
    [Tenant("Business")]
    public class genie_user_vigor
    {
        /// <summary>
        /// 
        /// </summary>
        public genie_user_vigor()
        {
        }

        private System.Int32 _uid;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 uid { get { return this._uid; } set { this._uid = value; } }

        private System.Int32? _vigor;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? vigor { get { return this._vigor; } set { this._vigor = value; } }

        private System.DateTime? _upTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? upTime { get { return this._upTime; } set { this._upTime = value; } }

        private System.String _upDay;
        /// <summary>
        /// 
        /// </summary>
        public System.String upDay { get { return this._upDay; } set { this._upDay = value; } }
    }
}