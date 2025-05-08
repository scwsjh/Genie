using SqlSugar;

namespace Service.Enties
{
	/// <summary>
	///
	/// </summary>
	[Tenant("Business")]
	public class genie_arena_award
	{
		/// <summary>
		///
		/// </summary>
		public genie_arena_award()
		{
		}

		private System.String _id;

		/// <summary>
		///
		/// </summary>
		[SugarColumn(IsPrimaryKey = true)]
		public System.String id
		{ get { return this._id; } set { this._id = value; } }

		private System.Int32? _count;

		/// <summary>
		///
		/// </summary>
		public System.Int32? count
		{ get { return this._count; } set { this._count = value; } }

		private System.String _award;

		/// <summary>
		///
		/// </summary>
		public System.String award
		{ get { return this._award; } set { this._award = value; } }

		private System.Int32? _isPut;

		/// <summary>
		///
		/// </summary>
		public System.Int32? isPut
		{ get { return this._isPut; } set { this._isPut = value; } }
	}
}