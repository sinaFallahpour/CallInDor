using Domain;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Service.Interfaces.Resource;
using Service.Interfaces.SmsService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Service
{
    public class SmsService : ISmsService
    {

        private readonly IHttpClientFactory _ClientFactory;
        //private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IResourceServices _resourceServices;

        //private readonly IlogService _ilog;

        public SmsService(IHttpClientFactory httpClientFactory,
            IResourceServices resourceServices
            //IStringLocalizer<ShareResource> localizerShared
            /*, IlogService ilog*/)
        {
            _ClientFactory = httpClientFactory;
            //_localizerShared = localizerShared;
            _resourceServices = resourceServices;
            //_ilog = ilog;
        }
        public async Task<(bool isSuccess, string error)> SendMessage(string To, string data, string pattern)
        {
            if (string.IsNullOrWhiteSpace(To) || string.IsNullOrWhiteSpace(data) || string.IsNullOrWhiteSpace(pattern))
            {
                //return (false, "ورودی ها نادرست است");
                return (false, _resourceServices.GetErrorMessageByKey("InvalidInput"));
            }

            try
            {
                var number = To.Insert(0, "98").Remove(2, 1);
                string from = "+983000505";
                //string userName = "katino";
                //string pass = "1qaz2wsx!QAZ@WSX";

                string userName = "emb24";
                string pass = "1qaz2wsx!QAZ@WSX";

                string patternCode = pattern;
                string to = JsonConvert.SerializeObject(new string[] { number });
                string input_data = data;

                string url = $@"http://188.0.240.110/patterns/pattern?username={userName}&password={UrlEncoder.Default.Encode(pass)}&from={from}&to={to}&input_data={UrlEncoder.Default.Encode(input_data)}&pattern_code={patternCode}";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                var client = _ClientFactory.CreateClient();
                var response = await client.SendAsync(request);
                response.Dispose();

                return (true, _resourceServices.GetErrorMessageByKey("SuccessMessage"));
            }
            catch
            {
                return (false, _resourceServices.GetErrorMessageByKey("InternalServerMessage"));
            }
        }



        /// <summary>
        /// ارسال کد تایید بعد از ثبت نام در سایت
        /// </summary>
        /// <param name="code"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<(bool isSuccess, string error)> RegistrerCode(string code, string phoneNumber)
        {
            try
            {
                await SendMessage(phoneNumber, JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    ["code"] = code,
                }), "bxsok98udn");
                return (true, _resourceServices.GetErrorMessageByKey("SuccessMessage"));
            }
            catch
            {
                return (false, _resourceServices.GetErrorMessageByKey("InternalServerMessage"));
            }
        }




        /// <summary>
        /// فرستادن پسورد جدید برای کاربر
        /// بعد از forgetpassword 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<(bool isSuccess, string error)> RecoveryPassword(string code, string phoneNumber)
        {
            try
            {
                await SendMessage(phoneNumber, JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    ["password"] = code,
                }), "p9wywk8a9d");
                return (true, _resourceServices.GetErrorMessageByKey("SuccessMessage"));
            }
            catch
            {
                return (false, _resourceServices.GetErrorMessageByKey("InternalServerMessage"));
            }
        }




        /// <summary>
        ///     سرویس شما توسط ادمین تایید شد
        /// </summary>
        /// <param name="code"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<(bool isSuccess, string error)> ConfirmServiceByAdmin(string code, string phoneNumber)
        {
            try
            {
                await SendMessage(phoneNumber, JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    ["serviceName"] = code,
                }), "usdpft2ewz");
                return (true, _resourceServices.GetErrorMessageByKey("SuccessMessage"));
            }
            catch
            {
                return (false, _resourceServices.GetErrorMessageByKey("InternalServerMessage"));
            }
        }





        /// <summary>
        ///     سرویس شما توسط ادمین رد شد
        /// </summary>
        /// <param name="code"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<(bool isSuccess, string error)> RejectServiceByAdmin(string code, string phoneNumber)
        {
            try
            {
                await SendMessage(phoneNumber, JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    ["serviceName"] = code,
                }), "4yyxhe6s54");
                return (true, _resourceServices.GetErrorMessageByKey("SuccessMessage"));
            }
            catch
            {
                return (false, _resourceServices.GetErrorMessageByKey("InternalServerMessage"));
            }
        }





        /// <summary>
        /// ارسال پیغام درخواست شما توسط پروایدر تایید شد
        /// </summary>
        /// <param name="code"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<(bool isSuccess, string error)> ConfirmRequestByProvider(string code, string phoneNumber)
        {
            try
            {
                await SendMessage(phoneNumber, JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    ["serviceName"] = code,
                }), "zvm4zggqum");
                return (true, _resourceServices.GetErrorMessageByKey("SuccessMessage"));
            }
            catch
            {
                return (false, _resourceServices.GetErrorMessageByKey("InternalServerMessage"));
            }
        }






        /// <summary>
        /// ارسال پیغام درخواست شما توسط پروایدر رد شد
        /// </summary>
        /// <param name="code"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<(bool isSuccess, string error)> RejectRequestByProvider(string code, string phoneNumber)
        {
            try
            {
                await SendMessage(phoneNumber, JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    ["serviceName"] = code,
                }), "yf48bi3ckg");
                return (true, _resourceServices.GetErrorMessageByKey("SuccessMessage"));
            }
            catch
            {
                return (false, _resourceServices.GetErrorMessageByKey("InternalServerMessage"));
            }
        }




        /// <summary>
        /// ارسال پیغام درخواست یرداشت شما توسط ادمین تایید شد
        /// </summary>
        /// <param name="code"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<(bool isSuccess, string error)> AcceptWidthrawlRequestByAdmin(string code, string phoneNumber)
        {
            try
            {
                await SendMessage(phoneNumber, JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    ["text"] = code,
                }), "1p3xhnf8iw");
                return (true, _resourceServices.GetErrorMessageByKey("SuccessMessage"));
            }
            catch
            {
                return (false, _resourceServices.GetErrorMessageByKey("InternalServerMessage"));
            }
        }




        /// <summary>
        /// ارسال پیغام درخواست یرداشت شما توسط ادمین رد شد
        /// </summary>
        /// <param name="code"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<(bool isSuccess, string error)> RejectWidthrawlRequestByAdmin(string code, string phoneNumber)
        {
            try
            {
                await SendMessage(phoneNumber, JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    ["text"] = code,
                }), "1p3xhnf8iw");
                return (true, _resourceServices.GetErrorMessageByKey("SuccessMessage"));
            }
            catch
            {
                return (false, _resourceServices.GetErrorMessageByKey("InternalServerMessage"));
            }
        }





    }
}
