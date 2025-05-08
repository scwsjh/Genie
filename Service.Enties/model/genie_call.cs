using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    /// 
    /// </summary>
    [Tenant("Business")]
    public class genie_call
    {
        /// <summary>
        /// 
        /// </summary>
        public genie_call()
        {
        }

        private System.Int32 _id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 id { get { return this._id; } set { this._id = value; } }

        private System.String _name;
        /// <summary>
        /// 
        /// </summary>
        public System.String name { get { return this._name; } set { this._name = value; } }

        private System.String _needData;
        /// <summary>
        /// 
        /// </summary>
        public System.String needData { get { return this._needData; } set { this._needData = value; } }

        private System.String _getData;
        /// <summary>
        /// 
        /// </summary>
        public System.String getData { get { return this._getData; } set { this._getData = value; } }

        private System.Int32? _cool;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? cool { get { return this._cool; } set { this._cool = value; } }

        private System.String _img;
        /// <summary>
        /// 
        /// </summary>
        public System.String img { get { return this._img; } set { this._img = value; } }

        private System.Int32? _lev;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? lev { get { return this._lev; } set { this._lev = value; } }
    }
}