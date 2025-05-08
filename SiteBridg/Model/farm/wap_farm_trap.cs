using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Base")]
    public class wap_farm_trap
    {
        /// <summary>
        ///
        /// </summary>
        public wap_farm_trap()
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

        private System.Int32? _rate;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? rate
        { get { return this._rate; } set { this._rate = value; } }

        private System.Int32? _price;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? price
        { get { return this._price; } set { this._price = value; } }

        private System.Int32? _level;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? level
        { get { return this._level; } set { this._level = value; } }
    }
}