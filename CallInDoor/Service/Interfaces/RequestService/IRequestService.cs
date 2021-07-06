using Domain.DTO.RequestService;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.RequestService
{
    public interface IRequestService
    {


        (bool succsseded, List<string> result) ValidateRequestToCall(BaseMyServiceTBL baseServiceFromDB, AppUser provider, AppUser currentUser, bool hasReserveRequest, CheckDiscountTBL discountFromDb);

        Task<(bool succsseded, List<string> result)> ValidateRequestToChatService(BaseMyServiceTBL baseServiceFromDB, bool hasReserveRequest);


        (bool succsseded, List<string> result) ValidateWallet(double? cientBalance, ServiceRequestTBL requestfromDB, bool isNativeCustomer);


        /// <summary>
        /// ولیدیت کردن ریکوست که به سرویس های سشن وپریوریک از نوع چت وویس بیابد
        /// </summary>
        //Task<(bool succsseded, List<string> result)> ValidateRequestToPeriodedOrSessionChatService(BaseMyServiceTBL baseServiceFromDB, bool hasReserveRequest);
        Task<(bool succsseded, List<string> result)> ValidateRequestToPeriodedOrSessionChatService(BaseMyServiceTBL baseServiceFromDB, AppUser provider, AppUser currentUser, bool hasReserveRequest);

        (bool succsseded, List<string> result) ValidateSendChatToLimitedChatService(ServiceRequestTBL requestfromDB);
        (bool succsseded, List<string> result) ValidateRedisSendChatVoiceDuration(RedisValueForDurationChatVoice chatVoiceValueFromRedis);

        (bool succsseded, List<string> result) ValidateSendChatToChatService(SendChatToChatServiceDTO model);


        public double? ClientShouldPay(BaseMyServiceTBL baseServiceFromDB, CheckDiscountTBL discountFromDb = null);
        public double? ClientShouldPay(double price, CheckDiscountTBL discountFromDb = null);
        //public Task<double?> GellBlockeeMoney();

        bool IsInValidFile(IFormFile file);

        bool HasFreeMessage(int? FreeUsageMessageCount, int? FreeMessageCount);
        //double ComputePriceWithSitePercent(double price, int sitePercent);

        double? ComputePriceWithSitePercent(double? price, int? sitePercent);


        ChatMessageType ReturnChatMessageType(SendChatToChatServiceDTO model);

        Task<string> SaveFileToHost(string path, string lastPath, IFormFile file, bool IsVoice = false);


    }
}
