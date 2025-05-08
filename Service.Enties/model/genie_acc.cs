using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_acc
    {
        /// <summary>
        ///
        /// </summary>
        public genie_acc()
        {
        }

        private System.Int32 _userId;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 userId
        { get { return this._userId; } set { this._userId = value; } }

        private System.Decimal? _copper;

        /// <summary>
        ///
        /// </summary>
        public System.Decimal? copper
        { get { return this._copper; } set { this._copper = value; } }

        private System.Decimal? _exploit;

        /// <summary>
        ///
        /// </summary>
        public System.Decimal? exploit
        { get { return this._exploit; } set { this._exploit = value; } }
    }
}