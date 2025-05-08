using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public interface IGenieGoodsService
    {
        Task<bool> DelGoods(int goodsId);

        Task<List<genie_goods>> GetGoodsList();

        Task<bool> AddGenieGoods(genie_goods goods);

        Task<bool> DeleteGoods(int id);

        Task<bool> UpdateGenieGoods(genie_goods goods);

        Task<genie_goods> GetGoodsInfo(int goodsId);

        Task<genie_user_goods> GetUserGoodsInfo(string ugId);

        Task<genie_user_goods> GetUserGoodsInfo(int uid, int goodsId);

        Task<int> GetUserGoodsCount(int uid, int goodsId);

        Task<List<genie_user_goods>> GetUserGoodsData(int uid, int page, int pageSize, RefAsync<int> total);

        Task<bool> UpdateUserGoods(int uid, int op, int goodsId, int count, string remark = "");
    }
}