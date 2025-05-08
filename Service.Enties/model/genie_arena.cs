using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_arena
    {
        /// <summary>
        ///
        /// </summary>
        public genie_arena()
        {
        }

        private System.String _id;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id
        { get { return this._id; } set { this._id = value; } }

        private System.Int32? _userId;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? userId
        { get { return this._userId; } set { this._userId = value; } }

        private System.Int32? _count;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? count
        { get { return this._count; } set { this._count = value; } }
    }
}