using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Enties
{
    public class TowerProffer
    {
        private System.Int32 _Id;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 Id
        { get { return this._Id; } set { this._Id = value; } }

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

        private System.String _ask;

        /// <summary>
        ///
        /// </summary>
        public System.String ask
        { get { return this._ask; } set { this._ask = value; } }

        private System.Int32 _count;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 count
        { get { return this._count; } set { this._count = value; } }

        private List<TowerProfferTemp> _NeedItem;

        public List<TowerProfferTemp> NeedItem
        { get { return this._NeedItem; } set { this._NeedItem = value; } }
    }

    public class TowerProfferTemp
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

        private System.String _ask;

        /// <summary>
        ///
        /// </summary>
        public System.String ask
        { get { return this._ask; } set { this._ask = value; } }

        private System.Int32 _count;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 count
        { get { return this._count; } set { this._count = value; } }
    }
}