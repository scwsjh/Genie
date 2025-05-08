namespace Service.Core
{
    public interface ILoginUser
    {
        string OnLineSid { get; }
        bool IsLogin { get; }
        string userId { get; }
        int userNo { get; }
        string nick { get; }
        int status { get; }
    }
}