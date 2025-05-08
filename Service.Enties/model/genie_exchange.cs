using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_exchange
    {
        /// <summary>
        ///
        /// </summary>
        public genie_exchange()
        {
        }

        private System.Int32 _exId;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 exId
        { get { return this._exId; } set { this._exId = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.String _tips;

        /// <summary>
        ///
        /// </summary>
        public System.String tips
        { get { return this._tips; } set { this._tips = value; } }

        private System.Int32? _count;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? count
        { get { return this._count; } set { this._count = value; } }

        private System.String _needData;

        /// <summary>
        ///
        /// </summary>
        public System.String needData
        { get { return this._needData; } set { this._needData = value; } }

        private System.String _getData;

        /// <summary>
        ///
        /// </summary>
        public System.String getData
        { get { return this._getData; } set { this._getData = value; } }

        private System.DateTime? _startTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? startTime
        { get { return this._startTime; } set { this._startTime = value; } }

        private System.DateTime? _endTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? endTime
        { get { return this._endTime; } set { this._endTime = value; } }
    }
}