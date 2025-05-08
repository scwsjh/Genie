using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using Furion.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using TencentCloud.Common;
using TencentCloud.Common.Profile;
using TencentCloud.Sms.V20210111;
using TencentCloud.Sms.V20210111.Models;

namespace Service.Core
{
    public class SmsService : ISmsService, ITransient
    {
        private readonly IRedisCache redis;
        private readonly IOptions<SmsOptions> config;

        public SmsService(IRedisCache _redis, IOptions<SmsOptions> _config)
        {
            redis = _redis;
            config = _config;
        }

        public bool SendSms(string phone, string ip, string billCode, out string smsTips)
        {
            smsTips = string.Empty;
            bool isok = false;
            string key = string.Format("SEND_SMS:{0}:{1}", billCode, phone);
            var data = redis.Get<string>(key);
            if (!string.IsNullOrEmpty(data))
            {
                smsTips = "验证码已经发送到您的手机!";
                return false;
            }
            string verifyCode = RandomHelper.GetFormatedNumeric(1000, 9999).ToString();
            redis.Set(key, verifyCode, config.Value.SmsOnTime);

            //if (SQLHelper.MongoDbSource.GetList<SmsLog>(it => it.phone == phone && it.endTime > DateTime.Now).Count > 0)
            //{
            //    smsTips = "验证码已经发送到您的手机!";
            //    return false;
            //}
            //SmsLog log = new SmsLog();
            //log.Id = StringHelper.NewGuid;
            //log.phone = phone;
            //log.ip = ip;
            //log.code = code;
            //SQLHelper.MongoDbSource.Insert(log);

            var sendResult = false;
            if (config.Value.SmsType == 0)
            {
                sendResult = SmsPostByAliyun(phone, verifyCode, ref smsTips);
            }
            else
            {
                sendResult = SmsPostByTencentyun(phone, verifyCode, ref smsTips);
            }
            if (sendResult)
            {
                isok = true;
            }
            else
            {
                redis.Del(key);
            }

            return isok;
        }

        public bool CheakSmsCode(string phone, string billCode, string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }
            string key = string.Format("SEND_SMS:{0}:{1}", billCode, phone);
            var data = redis.Get<string>(key);
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }
            bool isok = data.Equals(code);
            if (isok)
            {
                SettingSmsOut(phone, billCode);
            }
            return isok;
        }

        public void SettingSmsOut(string phone, string billCode)
        {
            string key = string.Format("SEND_SMS:{0}:{1}", billCode, phone);
            redis.Del(key);
        }

        private bool SmsPostByAliyun(string phone, string code, ref string Tips)
        {
            bool isok = false;
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", config.Value.AccessKey, config.Value.Secret);
            DefaultAcsClient client = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            request.Method = MethodType.POST;
            request.Domain = "dysmsapi.aliyuncs.com";
            request.Version = "2017-05-25";
            request.Action = "SendSms";
            // request.Protocol = ProtocolType.HTTP;
            request.AddQueryParameters("PhoneNumbers", phone);
            request.AddQueryParameters("SignName", config.Value.signName);
            request.AddQueryParameters("TemplateCode", config.Value.TemplateCode);
            ResultData codes = new ResultData();
            codes.SetValue("code", code);
            request.AddQueryParameters("TemplateParam", codes.ToJson());
            try
            {
                CommonResponse response = client.GetCommonResponse(request);
                ResultData result = new ResultData();
                result.FromJson(System.Text.Encoding.Default.GetString(response.HttpResponse.Content));
                if (result.GetValue("Code").ToString().Equals("OK"))
                {
                    isok = true;
                }
                else
                {
                    Tips = result.GetValue("Message").ToString();
                }
            }
            catch (ServerException e)
            {
                Tips = e.Message;
            }
            catch (ClientException e)
            {
                Tips = e.Message;
            }

            return isok;
        }

        private bool SmsPostByTencentyun(string phone, string code, ref string Tips)
        {
            bool isok = false;
            try
            {
                Credential cred = new Credential
                {
                    SecretId = config.Value.AccessKey,
                    SecretKey = config.Value.Secret
                };
                ClientProfile clientProfile = new ClientProfile();
                clientProfile.SignMethod = ClientProfile.SIGN_TC3SHA256;
                HttpProfile httpProfile = new HttpProfile();
                httpProfile.ReqMethod = "GET";
                httpProfile.Timeout = 10; // 请求连接超时时间，单位为秒(默认60秒)
                httpProfile.Endpoint = "sms.tencentcloudapi.com";
                httpProfile.WebProxy = Environment.GetEnvironmentVariable("HTTPS_PROXY");
                clientProfile.HttpProfile = httpProfile;
                SmsClient client = new SmsClient(cred, "ap-guangzhou", clientProfile);
                SendSmsRequest req = new SendSmsRequest();
                req.SmsSdkAppId = config.Value.SmsSdkAppId;
                req.SignName = config.Value.signName;
                req.ExtendCode = "";
                req.SenderId = "";
                req.SessionContext = "tanwan";
                req.PhoneNumberSet = new String[] { string.Format("+86{0}", phone) };
                req.TemplateId = config.Value.TemplateCode;
                req.TemplateParamSet = new String[] { code };
                SendSmsResponse resp = client.SendSmsSync(req);
                // 输出json格式的字符串回包
                // Console.WriteLine(AbstractModel.ToJsonString(resp));
                isok = true;
            }
            catch (Exception e)
            {
                Tips = e.Message;
            }

            return isok;
        }
    }
}