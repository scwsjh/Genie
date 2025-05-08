using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public class GenieChatService : IGenieChatService, ITransient
    {
        private readonly ISqlSugarClient DbClient;

        public GenieChatService(ISqlSugarClient _DbClient)
        {
            DbClient = _DbClient;
        }

        public async Task<List<genie_chat>> GetMainMsg(int uid)
        {
            List<genie_chat> result = new List<genie_chat>();
            //获取第一条私聊
            DateTime endTime = DateTime.Now.AddDays(-7);
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_chat>();
            var prvData = await db.Queryable<genie_chat>().Where(it => it.toUser == uid && it.addTime > endTime).OrderByDescending(it => it.addTime).Take(1).ToListAsync();
            //获取世界
            result.AddRange(prvData);
            string code = ChatEnum.code.Public.ToString();
            var pubData = await db.Queryable<genie_chat>().Where(it => it.code == code).OrderByDescending(it => it.addTime).Take(2).ToListAsync();
            result.AddRange(pubData);
            return result;
        }

        public async Task<List<genie_chat>> GetChatData(int uid, int type, int page, int pageSize, RefAsync<int> total)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_chat>();
            if (type == 1)
            {
                string code = ChatEnum.code.User.ToString();
                return await db.Queryable<genie_chat>().Where(it => (it.uid == uid || it.toUser == uid) && it.code == code).OrderByDescending(it => it.addTime).ToPageListAsync(page, pageSize, total);
            }
            else
            {
                string code = ChatEnum.code.Public.ToString();
                return await db.Queryable<genie_chat>().Where(it => it.uid == uid || it.toUser == uid || it.code == code).OrderByDescending(it => it.addTime).ToPageListAsync(page, pageSize, total);
            }
        }

        public async Task<bool> AddChat(int uid, string nick, int toId, string code, string sign)
        {
            var db = DbClient.AsTenant().GetConnectionWithAttr<genie_chat>();
            genie_chat chat = new genie_chat();
            chat.id = StringHelper.NewGuid;
            chat.uid = uid;
            chat.nick = nick;
            chat.toUser = toId;
            chat.code = code;
            chat.sign = sign;
            chat.addTime = DateTime.Now;
            return await db.Insertable(chat).ExecuteCommandAsync() > 0;
        }
    }
}