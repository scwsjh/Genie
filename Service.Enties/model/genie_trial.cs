using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Business")]
    public class genie_trial
    {
        /// <summary>
        ///
        /// </summary>
        public genie_trial()
        {
        }

        private System.Int32 _id;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 id
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

        private System.String _team;

        /// <summary>
        ///
        /// </summary>
        public System.String team
        { get { return this._team; } set { this._team = value; } }

        private System.Int32? _exp;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? exp
        { get { return this._exp; } set { this._exp = value; } }

        private System.String _award;

        /// <summary>
        ///
        /// </summary>
        public System.String award
        { get { return this._award; } set { this._award = value; } }

        private System.Int32? _status;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? status
        { get { return this._status; } set { this._status = value; } }

        private System.Int32? _vigor;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? vigor
        { get { return this._vigor; } set { this._vigor = value; } }
    }
}