using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_dou
    {
        /// <summary>
        ///
        /// </summary>
        public genie_dou()
        {
        }

        private System.Int32 _userId;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 userId
        { get { return this._userId; } set { this._userId = value; } }

        private System.String _upTime;

        /// <summary>
        ///
        /// </summary>
        public System.String upTime
        { get { return this._upTime; } set { this._upTime = value; } }

        private System.String _users;

        /// <summary>
        ///
        /// </summary>
        public System.String users
        { get { return this._users; } set { this._users = value; } }
    }
}