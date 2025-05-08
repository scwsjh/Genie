using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Base")]
    public class wap_ygw_muck
    {
        /// <summary>
        ///
        /// </summary>
        public wap_ygw_muck()
        {
        }

        private System.Int32 _id;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 id
        { get { return this._id; } set { this._id = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.Int32? _price;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? price
        { get { return this._price; } set { this._price = value; } }

        private System.Int32? _speed;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? speed
        { get { return this._speed; } set { this._speed = value; } }

        private System.Int32? _level;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? level
        { get { return this._level; } set { this._level = value; } }

        private System.Int32? _money;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? money
        { get { return this._money; } set { this._money = value; } }
    }
}