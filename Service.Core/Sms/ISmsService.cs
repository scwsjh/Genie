namespace Service.Core
{
    public interface ISmsService
    {
        bool SendSms(string phone, string ip, string billCode, out string smsTips);

        bool CheakSmsCode(string phone, string billCode, string code);
    }
}