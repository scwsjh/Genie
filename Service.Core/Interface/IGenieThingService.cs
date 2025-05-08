using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public interface IGenieThingService
    {
        Task<List<genie_thing>> GetGenieThing(int page, int pageSize, RefAsync<int> total, int uid = 0);

        Task<List<genie_thing>> GetGenieThing(string type, int task);

        Task AddGenieThing(int uid, string sign, string type = "Def");
    }
}