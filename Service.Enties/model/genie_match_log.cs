using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_match_log
    {
        /// <summary>
        ///
        /// </summary>
        public genie_match_log()
        {
        }

        private System.Int32 _userId;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 userId
        { get { return this._userId; } set { this._userId = value; } }

        private System.String _time;

        /// <summary>
        ///
        /// </summary>
        public System.String time
        { get { return this._time; } set { this._time = value; } }
    }
}