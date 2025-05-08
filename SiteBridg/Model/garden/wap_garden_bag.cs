using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Base")]
    public class wap_garden_bag
    {
        /// <summary>
        ///
        /// </summary>
        public wap_garden_bag()
        {
        }

        private System.Int32 _id;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 id
        { get { return this._id; } set { this._id = value; } }

        private System.Int32? _uid;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? uid
        { get { return this._uid; } set { this._uid = value; } }

        private System.Int32? _did;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? did
        { get { return this._did; } set { this._did = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.Byte? _dtype;

        /// <summary>
        ///
        /// </summary>
        public System.Byte? dtype
        { get { return this._dtype; } set { this._dtype = value; } }

        private System.Int32? _amount;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? amount
        { get { return this._amount; } set { this._amount = value; } }

        private System.Int32? _point;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? point
        { get { return this._point; } set { this._point = value; } }
    }
}