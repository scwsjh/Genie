using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_monster
    {
        /// <summary>
        ///
        /// </summary>
        public genie_monster()
        {
        }

        private System.String _id;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id
        { get { return this._id; } set { this._id = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.Int32? _lev;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? lev
        { get { return this._lev; } set { this._lev = value; } }

        private System.Int32? _start;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? start
        { get { return this._start; } set { this._start = value; } }

        private System.Int32? _charm;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? charm
        { get { return this._charm; } set { this._charm = value; } }

        private System.Int32? _blood;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? blood
        { get { return this._blood; } set { this._blood = value; } }

        private System.String _skill;

        /// <summary>
        ///
        /// </summary>
        public System.String skill
        { get { return this._skill; } set { this._skill = value; } }

        private System.String _remark;

        /// <summary>
        ///
        /// </summary>
        public System.String remark
        { get { return this._remark; } set { this._remark = value; } }
    }
}