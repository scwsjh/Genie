using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Base")]
    public class wap_ygw_seed
    {
        /// <summary>
        ///
        /// </summary>
        public wap_ygw_seed()
        {
        }

        private System.Int32 _ID;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 ID
        { get { return this._ID; } set { this._ID = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.Int32? _aging;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? aging
        { get { return this._aging; } set { this._aging = value; } }

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

        private System.String _image;

        /// <summary>
        ///
        /// </summary>
        public System.String image
        { get { return this._image; } set { this._image = value; } }

        private System.Byte? _status;

        /// <summary>
        ///
        /// </summary>
        public System.Byte? status
        { get { return this._status; } set { this._status = value; } }
    }
}