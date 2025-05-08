using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Base")]
    public class wap_garden_dz
    {
        /// <summary>
        ///
        /// </summary>
        public wap_garden_dz()
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

        private System.Int32? _hb;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? hb
        { get { return this._hb; } set { this._hb = value; } }

        private System.Int32? _money;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? money
        { get { return this._money; } set { this._money = value; } }

        private System.Int32? _point;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? point
        { get { return this._point; } set { this._point = value; } }
    }
}