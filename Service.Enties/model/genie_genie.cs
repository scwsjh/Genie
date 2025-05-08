using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_genie
    {
        /// <summary>
        ///
        /// </summary>
        public genie_genie()
        {
        }

        private System.Int32 _genieId;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 genieId
        { get { return this._genieId; } set { this._genieId = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.Int32? _type;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? type
        { get { return this._type; } set { this._type = value; } }

        private System.String _img;

        /// <summary>
        ///
        /// </summary>
        public System.String img
        { get { return this._img; } set { this._img = value; } }

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

        private System.String _upNeed;

        /// <summary>
        ///
        /// </summary>
        public System.String upNeed
        { get { return this._upNeed; } set { this._upNeed = value; } }

        private System.String _mapAward;

        /// <summary>
        ///
        /// </summary>
        public System.String mapAward
        { get { return this._mapAward; } set { this._mapAward = value; } }
    }
}