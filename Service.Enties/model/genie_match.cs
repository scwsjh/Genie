using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    /// 
    /// </summary>
    [Tenant("Business")]
    public class genie_match
    {
        /// <summary>
        /// 
        /// </summary>
        public genie_match()
        {
        }

        private System.Int32 _id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsIdentity = true)]
        public System.Int32 id { get { return this._id; } set { this._id = value; } }

        private System.Int32? _uid;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? uid { get { return this._uid; } set { this._uid = value; } }
    }
}