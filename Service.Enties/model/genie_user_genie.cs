using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    /// 
    /// </summary>
    [Tenant("Business")]
    public class genie_user_genie
    {
        /// <summary>
        /// 
        /// </summary>
        public genie_user_genie()
        {
        }

        private System.String _id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id { get { return this._id; } set { this._id = value; } }

        private System.Int32? _uid;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? uid { get { return this._uid; } set { this._uid = value; } }

        private System.Int32? _genieId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? genieId { get { return this._genieId; } set { this._genieId = value; } }

        private System.String _name;
        /// <summary>
        /// 
        /// </summary>
        public System.String name { get { return this._name; } set { this._name = value; } }

        private System.Int32? _type;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? type { get { return this._type; } set { this._type = value; } }

        private System.Int32? _charm;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? charm { get { return this._charm; } set { this._charm = value; } }

        private System.Int32? _blood;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? blood { get { return this._blood; } set { this._blood = value; } }

        private System.String _skill;
        /// <summary>
        /// 
        /// </summary>
        public System.String skill { get { return this._skill; } set { this._skill = value; } }

        private System.DateTime? _addTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? addTime { get { return this._addTime; } set { this._addTime = value; } }

        private System.Int32? _lev;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? lev { get { return this._lev; } set { this._lev = value; } }

        private System.Int32? _start;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? start { get { return this._start; } set { this._start = value; } }
    }
}