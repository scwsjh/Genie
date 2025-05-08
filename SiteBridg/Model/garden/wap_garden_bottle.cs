using SqlSugar;

namespace SiteBridg.Model
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Base")]
    public class wap_garden_bottle
    {
        /// <summary>
        ///
        /// </summary>
        public wap_garden_bottle()
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

        private System.Int32? _mapid;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? mapid
        { get { return this._mapid; } set { this._mapid = value; } }

        private System.Int32? _amount;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? amount
        { get { return this._amount; } set { this._amount = value; } }
    }
}