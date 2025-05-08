using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    /// 
    /// </summary>
    [Tenant("Business")]
    public class genie_team
    {
        /// <summary>
        /// 
        /// </summary>
        public genie_team()
        {
        }

        private System.Int32 _uid;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 uid { get { return this._uid; } set { this._uid = value; } }

        private System.String _team;
        /// <summary>
        /// 
        /// </summary>
        public System.String team { get { return this._team; } set { this._team = value; } }
    }
}