using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public interface IGenieChatService
    {
        Task<List<genie_chat>> GetMainMsg(int uid);

        Task<List<genie_chat>> GetChatData(int uid, int type, int page, int pageSize, RefAsync<int> total);

        Task<bool> AddChat(int uid, string nick, int toId, string code, string sign);
    }
}