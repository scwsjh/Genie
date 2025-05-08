using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Enties
{
    public class game_pack
    {
        /// <summary>
        ///
        /// </summary>
        public game_pack()
        {
        }

        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.Int32 _chance;

        public System.Int32 chance
        { get { return this._chance; } set { this._chance = value; } }

        private System.Int32 _minCount;

        public System.Int32 minCount
        { get { return this._minCount; } set { this._minCount = value; } }

        private System.Int32 _maxCount;

        public System.Int32 maxCount
        { get { return this._maxCount; } set { this._maxCount = value; } }

        private List<game_pack_item> _items;

        public List<game_pack_item> items
        { get { return this._items; } set { this._items = value; } }
    }

    public class game_pack_item
    {
        private System.String _name;

        /// <summary>
        ///
        /// </summary>
        public System.String name
        { get { return this._name; } set { this._name = value; } }

        private System.String _type;

        /// <summary>
        ///
        /// </summary>
        public System.String type
        { get { return this._type; } set { this._type = value; } }

        private System.String _code;

        /// <summary>
        ///
        /// </summary>
        public System.String code
        { get { return this._code; } set { this._code = value; } }

        private System.String _parameter;

        /// <summary>
        ///
        /// </summary>
        public System.String parameter
        { get { return this._parameter; } set { this._parameter = value; } }

        private System.Int32 _random;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 random
        { get { return this._random; } set { this._random = value; } }

        private System.Int32 _minCount;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 minCount
        { get { return this._minCount; } set { this._minCount = value; } }

        private System.Int32 _maxCount;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 maxCount
        { get { return this._maxCount; } set { this._maxCount = value; } }

        private System.Int32 _onCount;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 onCount
        { get { return this._onCount; } set { this._onCount = value; } }

        private System.Int32 _must;

        /// <summary>
        ///
        /// </summary>
        public System.Int32 must
        { get { return this._must; } set { this._must = value; } }
    }
}