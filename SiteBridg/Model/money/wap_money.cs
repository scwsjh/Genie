using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Base")]
    public class wap_money
    {
        /// <summary>
        ///
        /// </summary>
        public wap_money()
        {
        }

        private System.Byte _id;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Byte id
        { get { return this._id; } set { this._id = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.String _unit;

        /// <summary>
        ///
        /// </summary>
        public System.String unit
        { get { return this._unit; } set { this._unit = value; } }

        private System.Byte? _turn;

        /// <summary>
        ///
        /// </summary>
        public System.Byte? turn
        { get { return this._turn; } set { this._turn = value; } }
    }
}