using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteBridg.Model
{
    [Tenant("Base")]
    public class wap_garden
    {
        /// <summary>
        ///
        /// </summary>
        public wap_garden()
        {
        }

        private System.Int32 _uid;

        /// <summary>
        ///
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 uid
        { get { return this._uid; } set { this._uid = value; } }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.Byte? _level;

        /// <summary>
        ///
        /// </summary>
        public System.Byte? level
        { get { return this._level; } set { this._level = value; } }

        private System.Int32? _point;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? point
        { get { return this._point; } set { this._point = value; } }

        private System.Byte? _lands;

        /// <summary>
        ///
        /// </summary>
        public System.Byte? lands
        { get { return this._lands; } set { this._lands = value; } }

        private System.String _notice;

        /// <summary>
        ///
        /// </summary>
        public System.String notice
        { get { return this._notice; } set { this._notice = value; } }

        private System.Int32? _common;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? common
        { get { return this._common; } set { this._common = value; } }

        private System.Int32? _festival;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? festival
        { get { return this._festival; } set { this._festival = value; } }

        private System.Int32? _scarce;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? scarce
        { get { return this._scarce; } set { this._scarce = value; } }

        private System.Int32? _basket;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? basket
        { get { return this._basket; } set { this._basket = value; } }

        private System.Int32? _bottle;

        /// <summary>
        ///
        /// </summary>
        public System.Int32? bottle
        { get { return this._bottle; } set { this._bottle = value; } }

        private System.Byte? _config;

        /// <summary>
        ///
        /// </summary>
        public System.Byte? config
        { get { return this._config; } set { this._config = value; } }

        private System.DateTime? _addtime;

        /// <summary>
        ///
        /// </summary>
        public System.DateTime? addtime
        { get { return this._addtime; } set { this._addtime = value; } }
    }
}