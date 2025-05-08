using SqlSugar;

namespace Service.Enties
{
    /// <summary>
    ///
    /// </summary>
    [Tenant("Base")]
    public class wap_user_money
    {
        /// <summary>
        ///
        /// </summary>
        public wap_user_money()
        {
        }

        private System.Int32 _uid;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 uid
        { get { return this._uid; } set { this._uid = value; } }

        private System.String _pass;

        /// <summary>
        ///
        /// </summary>
        public System.String pass
        { get { return this._pass; } set { this._pass = value; } }

        private System.Int32? _mon00;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? mon00
        { get { return this._mon00; } set { this._mon00 = value; } }

        private System.Int32? _mon01;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? mon01
        { get { return this._mon01; } set { this._mon01 = value; } }

        private System.Int32? _mon10;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? mon10
        { get { return this._mon10; } set { this._mon10 = value; } }

        private System.Int32? _mon11;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? mon11
        { get { return this._mon11; } set { this._mon11 = value; } }

        private System.Int32? _mon20;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? mon20
        { get { return this._mon20; } set { this._mon20 = value; } }

        private System.Int32? _mon21;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? mon21
        { get { return this._mon21; } set { this._mon21 = value; } }
    }
}