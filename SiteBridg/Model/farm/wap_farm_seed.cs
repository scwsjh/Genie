using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Base")]
    public class wap_farm_seed
    {
        /// <summary>
        ///
        /// </summary>
        public wap_farm_seed()
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

        private System.Int32? _cycle;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? cycle
        { get { return this._cycle; } set { this._cycle = value; } }

        private System.Int32? _aging;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? aging
        { get { return this._aging; } set { this._aging = value; } }

        private System.Int32? _again;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? again
        { get { return this._again; } set { this._again = value; } }

        private System.Int32? _yield;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? yield
        { get { return this._yield; } set { this._yield = value; } }

        private System.Int32? _price;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? price
        { get { return this._price; } set { this._price = value; } }

        private System.Int32? _point;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? point
        { get { return this._point; } set { this._point = value; } }

        private System.Int32? _level;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? level
        { get { return this._level; } set { this._level = value; } }
    }
}