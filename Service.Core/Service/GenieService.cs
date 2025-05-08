using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
	public class GenieService : IGenieService, ITransient
	{
		private readonly ISqlSugarClient DbClient;

		public GenieService(ISqlSugarClient _DbClient)
		{
			DbClient = _DbClient;
		}

		public async Task<List<genie_genie>> GetGenieData(int type, int page, int pageSize, RefAsync<int> total)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_genie>();
			var data = await db.Queryable<genie_genie>()
				.WhereIF(type != 0, it => it.type == type).ToPageListAsync(page, pageSize, total);
			return data;
		}

		public async Task<List<genie_genie>> GetGenieData()
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_genie>();
			var data = await db.Queryable<genie_genie>().ToListAsync();
			return data;
		}

		public async Task<genie_genie> GetGenieInfo(int genieId)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_genie>();
			var data = await db.Queryable<genie_genie>().Where(it => it.genieId == genieId).SingleAsync();
			return data;
		}

		public async Task<bool> SaveGenie(genie_genie data)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_genie>();
			return await db.Storageable(data).ExecuteCommandAsync() > 0;
		}

		public async Task<bool> DeleteGenie(int genieId)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_genie>();
			return await db.Deleteable<genie_genie>().Where(it => it.genieId == genieId).ExecuteCommandAsync() > 0;
		}

		public async Task<List<genie_monster>> GetMonsterData()
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_monster>();
			return await db.Queryable<genie_monster>().ToListAsync();
		}

		public async Task<genie_monster> GetMonsterInfo(string id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_monster>();
			return await db.Queryable<genie_monster>().Where(it => it.id == id).SingleAsync();
		}

		public async Task<bool> SaveMonster(genie_monster data)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_monster>();
			return await db.Storageable(data).ExecuteCommandAsync() > 0;
		}

		public async Task<bool> DeleteMonster(string id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_monster>();
			return await db.Deleteable<genie_monster>().Where(it => it.id == id).ExecuteCommandAsync() > 0;
		}

		public async Task<List<genie_user_genie>> GetUserGenieData(int uid, int type, int page, int pageSize, RefAsync<int> total)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user_genie>();
			var data = await db.Queryable<genie_user_genie>().Where(it => it.uid == uid)
				.WhereIF(type != 0, it => it.type == type)
					 .OrderBy(it => it.start, OrderByType.Desc).OrderBy(it => it.lev, OrderByType.Desc).OrderBy(it => it.genieId, OrderByType.Asc).ToPageListAsync(page, pageSize, total);
			return data;
		}

		public async Task<List<genie_user_genie>> GetUserGenieDataBySerch(int uid, List<string> NoAt, int page, int pageSize, RefAsync<int> total, string serch = "")
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user_genie>();
			var data = await db.Queryable<genie_user_genie>().Where(it => it.uid == uid && !NoAt.Contains(it.id))
				.OrderBy(it => it.start, OrderByType.Desc).OrderBy(it => it.lev, OrderByType.Desc).OrderBy(it => it.type, OrderByType.Desc)
				.ToPageListAsync(page, pageSize, total);
			return data;
		}

		public async Task<List<genie_user_genie>> GetUserGenieDataByFusion(int uid, int type, int start, string NoOn, int page, int pageSize, RefAsync<int> total, string serch = "")
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user_genie>();
			var data = await db.Queryable<genie_user_genie>().Where(it => it.uid == uid && it.id != NoOn && it.type >= type && it.start >= start)
				.OrderBy(it => it.start, OrderByType.Desc).OrderBy(it => it.lev, OrderByType.Desc).OrderBy(it => it.type, OrderByType.Desc)
				.ToPageListAsync(page, pageSize, total);
			return data;
		}

		public async Task<int> GetUserGenieCount(int uid)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user_genie>();
			return await db.Queryable<genie_user_genie>().Where(it => it.uid == uid).CountAsync();
		}

		public async Task<bool> GetGenieIsHave(int uid, int genieId)
		{
			string key = string.Format("{0}_{1}", uid, genieId);
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_map>();
			return await db.Queryable<genie_map>().Where(it => it.mapId == key).AnyAsync();
		}

		public async Task<genie_map> GetGenieMap(int uid, int genieId)
		{
			string key = string.Format("{0}_{1}", uid, genieId);
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_map>();
			return await db.Queryable<genie_map>().Where(it => it.mapId == key).SingleAsync();
		}

		private async Task<bool> AddGenieMap(int uid, int genId)
		{
			genie_map map = new genie_map();
			map.mapId = string.Format("{0}_{1}", uid, genId);
			map.userId = uid;
			map.genieId = genId;
			map.isGet = 0;
			map.addTime = DateTime.Now;
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_map>();
			return await db.Insertable(map).ExecuteCommandAsync() > 0;
		}

		public async Task<bool> UpdateMapAwardStatus(int uid, int genId, int isGet)
		{
			string key = string.Format("{0}_{1}", uid, genId);
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_map>();
			return await db.Updateable<genie_map>().SetColumns(it => it.isGet == isGet).Where(it => it.mapId == key).ExecuteCommandAsync() > 0;
		}

		public async Task<genie_user_genie> GetUserGenieInfo(string gid)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user_genie>();
			return await db.Queryable<genie_user_genie>().Where(it => it.id == gid).SingleAsync();
		}

		public async Task<genie_user_genie> GetUserGenieInfoByAttr(string gid)
		{
			var data = await GetUserGenieInfo(gid);

			data.charm = GenieTool.GetGenAttrCompute((int)data.charm, (int)data.lev, (int)data.start);
			data.blood = GenieTool.GetGenAttrCompute((int)data.blood, (int)data.lev, (int)data.start);

			List<AttrItem> attrs = new List<AttrItem>();
			//处理天赋
			if (!string.IsNullOrEmpty(data.skill))
			{
				SkillAttr tel = JsonConvert.DeserializeObject<SkillAttr>(data.skill);
				if (tel.attr.skill.code == SkillEnum.SkillCode.unit.ToString())
				{
					foreach (var item in tel.attr.skill.skill)
					{
						foreach (var ar in item.effect)
						{
							AttrItem add = new AttrItem();
							add.code = ar.code;
							add.compute = ar.compute;
							add.parameter = ar.parameter;
							add.tip = ar.tip;
							attrs.Add(add);
						}
					}
				}
			}
			if (attrs.Count > 0)
			{
				data.charm += Convert.ToInt32(UnitTool.ConventAttrValue(GameEnum.AttrCode.Charm.ToString(), attrs, (int)data.charm));
				data.blood += Convert.ToInt32(UnitTool.ConventAttrValue(GameEnum.AttrCode.Blood.ToString(), attrs, (int)data.blood));
			}
			return data;
		}

		public async Task<bool> AddUserGenie(int uid, int genId, int count)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user_genie>();
			var genInfo = await GetGenieInfo(genId);
			string skill = string.Empty;
			if (!string.IsNullOrEmpty(genInfo.skill))
			{
				SkillAttr attr = new SkillAttr();
				attr.id = StringHelper.NewGuid;
				attr.skId = genInfo.genieId;
				attr.exp = 0;
				attr.attr = JsonConvert.DeserializeObject<SkillItem>(genInfo.skill);
				skill = JsonConvert.SerializeObject(attr);
			}

			bool result = false;
			List<genie_user_genie> adds = new List<genie_user_genie>();
			for (int i = 0; i < count; i++)
			{
				genie_user_genie add = new genie_user_genie();
				add.id = StringHelper.NewGuid;
				add.uid = uid;
				add.genieId = genId;
				add.name = genInfo.name;
				add.type = genInfo.type;
				add.charm = genInfo.charm;
				add.blood = genInfo.blood;
				add.skill = skill;
				add.lev = 1;
				add.start = 0;
				add.addTime = DateTime.Now;
				adds.Add(add);
			}
			if (adds.Count > 0)
			{
				if (!await GetGenieIsHave(uid, genId))
				{
					await AddGenieMap(uid, genId);
				}
				result = await db.Insertable(adds).ExecuteCommandAsync() > 0;
			}
			return result;
		}

		public async Task<bool> UpdateUserGenie(genie_user_genie info)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user_genie>();
			return await db.Updateable(info).ExecuteCommandAsync() > 0;
		}

		public async Task<bool> DelUserGenie(string gu)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_user_genie>();
			return await db.Deleteable<genie_user_genie>().Where(it => it.id == gu).ExecuteCommandAsync() > 0;
		}

		#region 技能

		public async Task<List<genie_skill>> GetGenieSkillData()
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_skill>();
			return await db.Queryable<genie_skill>().ToListAsync();
		}

		public async Task<genie_skill> GetGenieSkillInfo(int id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_skill>();
			return await db.Queryable<genie_skill>().Where(it => it.skId == id).SingleAsync();
		}

		public async Task<bool> SaveGenieSkill(genie_skill data)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_skill>();
			return await db.Storageable(data).ExecuteCommandAsync() > 0;
		}

		public async Task<bool> DeleteGenieSkill(int id)
		{
			var db = DbClient.AsTenant().GetConnectionWithAttr<genie_skill>();
			return await db.Deleteable<genie_skill>().Where(it => it.skId == id).ExecuteCommandAsync() > 0;
		}

		#endregion 技能
	}
}