using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_thing
    {
        /// <summary>
        ///
        /// </summary>
        public genie_thing()
        {
        }

        private System.String _id;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id
        { get { return this._id; } set { this._id = value; } }

        private System.String _type;

        /// <summary>
        ///
        /// </summary>
        public System.String type
        { get { return this._type; } set { this._type = value; } }

        private System.Int32? _uid;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? uid
        { get { return this._uid; } set { this._uid = value; } }

        private System.String _sign;

        /// <summary>
        ///
        /// </summary>
        public System.String sign
        { get { return this._sign; } set { this._sign = value; } }

        private System.DateTime? _addTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? addTime
        { get { return this._addTime; } set { this._addTime = value; } }
    }
}