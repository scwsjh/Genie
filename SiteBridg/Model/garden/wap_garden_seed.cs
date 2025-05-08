using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Base")]
    public class wap_garden_seed
    {
        /// <summary>
        ///
        /// </summary>
        public wap_garden_seed()
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

        private System.Int32? _seed;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? seed
        { get { return this._seed; } set { this._seed = value; } }

        private System.Int32? _ling;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? ling
        { get { return this._ling; } set { this._ling = value; } }

        private System.Int32? _buds;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? buds
        { get { return this._buds; } set { this._buds = value; } }

        private System.Byte? _less;

        /// <summary>
        ///
        /// </summary>
        public System.Byte? less
        { get { return this._less; } set { this._less = value; } }

        private System.Int32? _more;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? more
        { get { return this._more; } set { this._more = value; } }

        private System.Byte? _dtype;

        /// <summary>
        ///
        /// </summary>
        public System.Byte? dtype
        { get { return this._dtype; } set { this._dtype = value; } }

        private System.Byte? _level;

        /// <summary>
        ///
        /// </summary>
        public System.Byte? level
        { get { return this._level; } set { this._level = value; } }

        private System.Int32? _price;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? price
        { get { return this._price; } set { this._price = value; } }

        private System.String _remark;

        /// <summary>
        ///
        /// </summary>
        public System.String remark
        { get { return this._remark; } set { this._remark = value; } }

        private System.Int32? _ch;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? ch
        { get { return this._ch; } set { this._ch = value; } }
    }
}