using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_skill
    {
        /// <summary>
        ///
        /// </summary>
        public genie_skill()
        {
        }

        private System.Int32 _skId;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 skId
        { get { return this._skId; } set { this._skId = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.String _attr;

        /// <summary>
        ///
        /// </summary>
        public System.String attr
        { get { return this._attr; } set { this._attr = value; } }

        private System.String _type;

        /// <summary>
        ///
        /// </summary>
        public System.String type
        { get { return this._type; } set { this._type = value; } }
    }
}