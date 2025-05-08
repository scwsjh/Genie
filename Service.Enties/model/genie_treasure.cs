using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_treasure
    {
        /// <summary>
        ///
        /// </summary>
        public genie_treasure()
        {
        }

        private System.Int32 _id;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 id
        { get { return this._id; } set { this._id = value; } }

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

        private System.String _needData;

        /// <summary>
        ///
        /// </summary>
        public System.String needData
        { get { return this._needData; } set { this._needData = value; } }

        private System.String _pack;

        /// <summary>
        ///
        /// </summary>
        public System.String pack
        { get { return this._pack; } set { this._pack = value; } }

        private System.DateTime? _addTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? addTime
        { get { return this._addTime; } set { this._addTime = value; } }

        private System.DateTime? _endTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? endTime
        { get { return this._endTime; } set { this._endTime = value; } }
    }
}