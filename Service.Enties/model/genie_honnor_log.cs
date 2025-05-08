using SqlSugar;

namespace Kx.Microservices.Domain.Entity
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_honnor_log
    {
        /// <summary>
        ///
        /// </summary>
        public genie_honnor_log()
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

        private System.DateTime? _addTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? addTime
        { get { return this._addTime; } set { this._addTime = value; } }
    }
}