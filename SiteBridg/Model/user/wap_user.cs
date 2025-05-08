using SqlSugar;

namespace SiteBridg.Model
{
    [Tenant("Base")]
    public class wap_user
    {
        /// <summary>
        ///
        /// </summary>
        public wap_user()
        {
        }

        private int _id;

        /// <summary>
        ///
        /// </summary>
        public int id
        { get { return _id; } set { _id = value; } }

        private string _sid;

        /// <summary>
        ///
        /// </summary>
        public string sid
        { get { return _sid; } set { _sid = value; } }

        private string _name;

        /// <summary>
        ///
        /// </summary>
        public string name
        { get { return _name; } set { _name = value; } }

        private string _face;

        /// <summary>
        ///
        /// </summary>
        public string face
        { get { return _face; } set { _face = value; } }

        private string _pass;

        /// <summary>
        ///
        /// </summary>
        public string pass
        { get { return _pass; } set { _pass = value; } }

        private DateTime? _stime;

        /// <summary>
        ///
        /// </summary>
        public DateTime? stime
        { get { return _stime; } set { _stime = value; } }

        private byte? _gender;

        /// <summary>
        ///
        /// </summary>
        public byte? gender
        { get { return _gender; } set { _gender = value; } }

        private byte? _age;

        /// <summary>
        ///
        /// </summary>
        public byte? age
        { get { return _age; } set { _age = value; } }

        private byte? _birth;

        /// <summary>
        ///
        /// </summary>
        public byte? birth
        { get { return _birth; } set { _birth = value; } }

        private DateTime? _solar;

        /// <summary>
        ///
        /// </summary>
        public DateTime? solar
        { get { return _solar; } set { _solar = value; } }

        private DateTime? _lunar;

        /// <summary>
        ///
        /// </summary>
        public DateTime? lunar
        { get { return _lunar; } set { _lunar = value; } }

        private string _zodiac;

        /// <summary>
        ///
        /// </summary>
        public string zodiac
        { get { return _zodiac; } set { _zodiac = value; } }

        private string _star;

        /// <summary>
        ///
        /// </summary>
        public string star
        { get { return _star; } set { _star = value; } }

        private string _blood;

        /// <summary>
        ///
        /// </summary>
        public string blood
        { get { return _blood; } set { _blood = value; } }

        private string _sign;

        /// <summary>
        ///
        /// </summary>
        public string sign
        { get { return _sign; } set { _sign = value; } }

        private int? _point;

        /// <summary>
        ///
        /// </summary>
        public int? point
        { get { return _point; } set { _point = value; } }

        private byte? _status;

        /// <summary>
        ///
        /// </summary>
        public byte? status
        { get { return _status; } set { _status = value; } }
    }
}