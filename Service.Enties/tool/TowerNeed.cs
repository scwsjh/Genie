namespace Service.Enties
{
    public class TowerNeed
    {
        private System.String _code;

        /// <summary>
        ///
        /// </summary>
        public System.String code
        { get { return this._code; } set { this._code = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.String _parameter;

        /// <summary>
        ///
        /// </summary>
        public System.String parameter
        { get { return this._parameter; } set { this._parameter = value; } }

        private System.Int32 _count;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 count
        { get { return this._count; } set { this._count = value; } }

        private System.Int32 _retCount;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 retCount
        { get { return this._retCount; } set { this._retCount = value; } }

        private System.Int32 _isOp;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 isOp
        { get { return this._isOp; } set { this._isOp = value; } }
    }
}