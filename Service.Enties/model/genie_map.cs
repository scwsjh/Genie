using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_map
    {
        /// <summary>
        ///
        /// </summary>
        public genie_map()
        {
        }

        private System.String _mapId;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String mapId
        { get { return this._mapId; } set { this._mapId = value; } }

        private System.Int32? _userId;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? userId
        { get { return this._userId; } set { this._userId = value; } }

        private System.Int32? _genieId;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? genieId
        { get { return this._genieId; } set { this._genieId = value; } }

        private System.DateTime? _addTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? addTime
        { get { return this._addTime; } set { this._addTime = value; } }

        private System.Int32? _isGet;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? isGet
        { get { return this._isGet; } set { this._isGet = value; } }
    }
}