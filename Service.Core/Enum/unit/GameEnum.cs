namespace Service.Core
{
    public class GameEnum
    {
        public enum Sex
        {
            男,
            女,
            未知
        }

        public enum TimeCode
        {
            Long,
            Month,
            Week,
            Day
        }

        public enum PropCode//游戏资源编码
        {
            Site,//站点
            Goods,//物品
            Genie,//精灵
            copper,//金币
            exploit,//功勋
            lev,//等级
            Exp,//经验
        }

        public enum LogCode
        {
            增加,
            减少
        }

        public enum ComputeType
        {
            Plus,//加法计算
            Ride,//乘法计算
            Reduce,//减法计算
            Except,//除法计算
        }

        public enum RandomBusOpName
        {
            pack,//礼包
        }

        public enum AttrCode
        {
            Charm,
            Blood
        }
    }
}