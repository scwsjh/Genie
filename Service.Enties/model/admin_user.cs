using SqlSugar;

namespace Service.Enties
{
    [Tenant("Business")]
    public class admin_user
    {
        /// <summary>
        ///
        /// </summary>
        public admin_user()
        {
        }

        private System.String _adminId;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String adminId
        { get { return this._adminId; } set { this._adminId = value; } }

        private System.Int32? _adminNo;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? adminNo
        { get { return this._adminNo; } set { this._adminNo = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.String _pwd;

        /// <summary>
        ///
        /// </summary>
        public System.String pwd
        { get { return this._pwd; } set { this._pwd = value; } }

        private System.Int32? _status;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? status
        { get { return this._status; } set { this._status = value; } }

        private System.DateTime _addTime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime addTime
        { get { return this._addTime; } set { this._addTime = value; } }
    }
}