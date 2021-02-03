using Domain;
using Domain.DTO.RequestService;
using Domain.Entities;
using Domain.Enums;
using Domain.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.RequestService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RequestService : IRequestService
    {
        #region ctor
        private IStringLocalizer<ShareResource> _localizerShared;
        private readonly DataContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IAccountService _accountService;

        public RequestService(IStringLocalizer<ShareResource> localizerShared,
            DataContext context,
            IHostingEnvironment hostingEnvironment,
            IAccountService accountService
            )
        {
            _localizerShared = localizerShared;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _accountService = accountService;
        }


        #endregion

        /// <summary>
        /// ولیدیت کردن ریکوست که به سرویس های فیری از نوع چت وویس بیابد
        /// </summary>
        /// <param name="baseServiceFromDB"></param>
        /// <param name="hasReserveRequest"></param>
        /// <returns></returns>
        public async Task<(bool succsseded, List<string> result)> ValidateRequestToChatService(BaseMyServiceTBL baseServiceFromDB, bool hasReserveRequest)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();
            string curentUSerName = _accountService.GetCurrentUserName();

            if (baseServiceFromDB == null)
            {
                IsValid = false;
                Errors.Add(_localizerShared["NotFound"].Value.ToString());
                return (IsValid, Errors);
            }


            //****************************  PackageType  Validate  ***************************************//
            if (baseServiceFromDB.MyChatsService?.PackageType != PackageType.Free)
            {
                IsValid = false;
                Errors.Add(_localizerShared["InvalidPackageType"].Value.ToString());
                return (IsValid, Errors);
            }
            //****************************  End PackageType  Validate  ***************************************//



            if (baseServiceFromDB.UserName.ToLower() == curentUSerName)
            {
                IsValid = false;
                Errors.Add(_localizerShared["YouCantRequestToYourSelf"].Value.ToString());
                return (IsValid, Errors);

            }


            //سرویس باید از نوع چت باشد
            if (baseServiceFromDB.ServiceType != ServiceType.ChatVoice)
            {
                IsValid = false;
                Errors.Add(_localizerShared["InValidServiceType"].Value.ToString());
                return (IsValid, Errors);
            }



            if (baseServiceFromDB.ConfirmedServiceType != ConfirmedServiceType.Confirmed ||
                baseServiceFromDB.ProfileConfirmType != ProfileConfirmType.Confirmed)
            {
                IsValid = false;
                Errors.Add(_localizerShared["NotFound"].Value.ToString());
                return (IsValid, Errors);
            }

            if (hasReserveRequest)
            {
                IsValid = false;
                Errors.Add(_localizerShared["HasReserveRequest"].Value.ToString());
                return (IsValid, Errors);
            }


            //check user is active or not
            bool isonline = false;
            if (!baseServiceFromDB.MyChatsService.IsServiceReverse)
            {
                isonline = await _context.Users.Where(c => c.UserName == baseServiceFromDB.UserName)
                                                   .Select(c => c.IsOnline)
                                                   .FirstOrDefaultAsync();
                if (!isonline)
                {
                    IsValid = false;
                    Errors.Add(_localizerShared["ProviderIsUnAvailableMessage"].Value.ToString());
                    return (IsValid, Errors);
                }
            }


            return (IsValid, Errors);
        }




        /// <summary>
        /// ولیدیت کردن ریکوست که به سرویس های سشن وپریوریک از نوع چت وویس بیابد
        /// </summary>
        /// <param name="baseServiceFromDB"></param>
        /// <param name="hasReserveRequest"></param>
        /// <returns></returns>
        public async Task<(bool succsseded, List<string> result)> ValidateRequestToPeriodedOrSessionChatService(BaseMyServiceTBL baseServiceFromDB,
                                    bool hasReserveRequest)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();
            string curentUSerName = _accountService.GetCurrentUserName();

            if (baseServiceFromDB == null)
            {
                IsValid = false;
                Errors.Add(_localizerShared["NotFound"].Value.ToString());
                return (IsValid, Errors);
            }


            //****************************  PackageType  Validate  ***************************************//
            if (baseServiceFromDB.MyChatsService.PackageType != PackageType.limited)
            {
                IsValid = false;
                Errors.Add(_localizerShared["InvalidPackageType"].Value.ToString());
                return (IsValid, Errors);
            }
            //****************************  End PackageType  Validate  ***************************************//



            if (baseServiceFromDB.UserName.ToLower() == curentUSerName)
            {
                IsValid = false;
                Errors.Add(_localizerShared["YouCantRequestToYourSelf"].Value.ToString());
                return (IsValid, Errors);
            }


            //سرویس باید از نوع چت باشد
            if (baseServiceFromDB.ServiceType != ServiceType.ChatVoice)
            {
                IsValid = false;
                Errors.Add(_localizerShared["InValidServiceType"].Value.ToString());
                return (IsValid, Errors);
            }



            if (baseServiceFromDB.ConfirmedServiceType != ConfirmedServiceType.Confirmed ||
                baseServiceFromDB.ProfileConfirmType != ProfileConfirmType.Confirmed)
            {
                IsValid = false;
                Errors.Add(_localizerShared["NotFound"].Value.ToString());
                return (IsValid, Errors);
            }

            if (hasReserveRequest)
            {
                IsValid = false;
                Errors.Add(_localizerShared["HasReserveRequest"].Value.ToString());
                return (IsValid, Errors);
            }



            //check user is active or not
            bool isonline = false;
            if (!baseServiceFromDB.MyChatsService.IsServiceReverse)
            {
                isonline = await _context.Users.Where(c => c.UserName == baseServiceFromDB.UserName)
                                                   .Select(c => c.IsOnline)
                                                   .FirstOrDefaultAsync();
                if (!isonline)
                {
                    IsValid = false;
                    Errors.Add(_localizerShared["ProviderIsUnAvailableMessage"].Value.ToString());
                    return (IsValid, Errors);
                }
            }


            return (IsValid, Errors);
        }






        public (bool succsseded, List<string> result) ValidateSendChatToLimitedChatService(ServiceRequestTBL requestfromDB)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();
            string curentUSerName = _accountService.GetCurrentUserName();

            if (requestfromDB == null)
            {
                IsValid = false;
                Errors.Add(_localizerShared["NotFound"].Value.ToString());
                return (IsValid, Errors);
            }



            if (requestfromDB.ServiceRequestStatus != ServiceRequestStatus.Confirmed)
            {
                IsValid = false;
                Errors.Add(_localizerShared["RequestNotConfirmedMessgaes"].Value.ToString());
                return (IsValid, Errors);
            }

            if (requestfromDB.PackageType == PackageType.Free)
            {
                IsValid = false;
                Errors.Add(_localizerShared["InvalidPackageType"].Value.ToString());
                return (IsValid, Errors);
            }


            //پلن ندارد که
            if (!requestfromDB.HasPlan_LimitedChatVoice)
            {
                IsValid = false;
                Errors.Add(_localizerShared["YouDontHaveAnyPackage"].Value.ToString());
                return (IsValid, Errors);
            }

            if (requestfromDB.ExpireTime_LimitedChatVoice < DateTime.Now)
            {
                IsValid = false;
                Errors.Add(_localizerShared["YourPackageExpired"].Value.ToString());
                return (IsValid, Errors);
            }

            if (requestfromDB.UsedMessageCount_LimitedChat >= requestfromDB.AllMessageCount_LimitedChat)
            {
                IsValid = false;
                Errors.Add(_localizerShared["NumberOfFreeMessagesCompleted"].Value.ToString());
                return (IsValid, Errors);
            }


            return (IsValid, Errors);
        }




        public (bool succsseded, List<string> result) ValidateRedisSendChatVoiceDuration(RedisValueForDurationChatVoice chatVoiceValueFromRedis)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();
            if (chatVoiceValueFromRedis == null)
            {
                IsValid = false;
                Errors.Add(_localizerShared["NotFound"].Value.ToString());
                return (IsValid, Errors);
            }

            //zaman nadard
            if (chatVoiceValueFromRedis.EndTime == null && DateTime.Now <= chatVoiceValueFromRedis.EndTime)
            {
                IsValid = false;
                Errors.Add(_localizerShared["YourPackageExpired"].Value.ToString());
                return (IsValid, Errors);
            }

            if (chatVoiceValueFromRedis.RequestStatusForRedis == RequestStatusForRedis.BadPlan)
            {
                IsValid = false;
                Errors.Add(_localizerShared["YourPackageExpiredOrNoPlan"].Value.ToString());
                return (IsValid, Errors);
            }

            return (IsValid, Errors);
        }






        public (bool succsseded, List<string> result) ValidateSendChatToChatService(SendChatToChatServiceDTO model)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();

            #region file validation
            //file validation
            if (model.IsFile && model.File == null)
            {

                IsValid = false;

                Errors.Add(_localizerShared["TheFileIsRequired"].Value.ToString());

                //Errors.Add("File Is required");
                return (IsValid, Errors);

            }
            else if (model.IsFile && IsInValidFile(model.File))
            {
                IsValid = false;
                Errors.Add(_localizerShared["TheFileIsInValid"].Value.ToString());
                return (IsValid, Errors);
            }


            if (model.IsFile && model.File.Length > 500000000)
            {
                IsValid = false;
                Errors.Add(_localizerShared["TheFileIsTooLarge"].Value.ToString());
                return (IsValid, Errors);
            }



            #endregion

            #region  voice validatio
            //voice validation
            if (model.IsVoice && model.Voice == null)
            {
                IsValid = false;
                Errors.Add(_localizerShared["VoiceIsrequired"].Value.ToString());
                return (IsValid, Errors);
            }
            //else if (model.IsVoice && IsInValidFile(model.Voice))
            //{
            //    IsValid = false;
            //    Errors.Add("InvalidVoice");
            //    return (IsValid, Errors);
            //}


            if (model.IsVoice && model.Voice.Length > 500000000)
            {
                IsValid = false;
                Errors.Add(_localizerShared["VoiceIsTooLarge"].Value.ToString());
                return (IsValid, Errors);
            }

            #endregion


            return (IsValid, Errors);

        }





        /// <summary>
        /// محاسبه قیمت ودر صد کاهش
        /// </summary>
        /// <param name="price"></param>
        /// <param name="sitePercent"></param>
        /// <returns></returns>
        public double? ComputePriceWithSitePercent(double? price, int? sitePercent)
        {
            var priceWithSitePercent = (price * sitePercent) / 100;
            var computedPrice = price - priceWithSitePercent;
            return computedPrice;
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="FreeUsageMessageCount"></param>
        /// <param name="FreeMessageCount"></param>
        /// <returns></returns>
        public bool HasFreeMessage(int? FreeUsageMessageCount, int? FreeMessageCount)
        {
            if (FreeUsageMessageCount <= FreeMessageCount)
                return true;
            return false;
        }





        public bool IsInValidFile(IFormFile file)
        {
            var extention = Path.GetExtension(file.FileName).ToLower();
            return (extention == ".exe" ? true : false);
        }


        public ChatMessageType ReturnChatMessageType(SendChatToChatServiceDTO model)
        {
            if (model.IsFile)
                return ChatMessageType.File;
            else if (model.IsVoice)
                return ChatMessageType.Voice;
            return ChatMessageType.Text;
        }





        public async Task<string> SaveFileToHost(string path, string lastPath, IFormFile file, bool IsVoice = false)
        {

            string uniqueVideoFileName = null;
            if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
                _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, path);
            uniqueVideoFileName = (Guid.NewGuid().ToString().GetImgUrlFriendly() + "_" + file.FileName);

            if (IsVoice)
                uniqueVideoFileName = uniqueVideoFileName + ".wav";
            string filePath = Path.Combine(uploadsFolder, uniqueVideoFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //Delete LastImage Image
            if (!string.IsNullOrEmpty(lastPath))
            {
                //var LastVideoPath = lastPath?.Substring(1);
                var LastPath = Path.Combine(_hostingEnvironment.WebRootPath, lastPath);
                if (System.IO.File.Exists(LastPath))
                {
                    System.IO.File.Delete(LastPath);
                }
            }
            //update Newe video Address To database
            //user.VideoAddress = "/Upload/User/" + uniqueVideoFileName;

            return path + uniqueVideoFileName;

        }





        public (bool succsseded, List<string> result) ValidateWallet(double? cientBalance, ServiceRequestTBL requestfromDB, bool isNativeCustomer)
        {

            bool IsValid = true;
            List<string> Errors = new List<string>();



            //اگر موجودی 0 داشت
            if (cientBalance <= 0)
            {
                IsValid = false;
                Errors.Add(_localizerShared["NotEnoughtBalance"].Value.ToString());
                return (IsValid, Errors);
            }

            //اگر پیام رایگان ندارد   ********************************************************************************
            if (requestfromDB.FreeUsageMessageCount > requestfromDB.FreeMessageCount)
            {
                if (isNativeCustomer)
                {
                    if (cientBalance < requestfromDB.PriceForNativeCustomer)
                    {
                        IsValid = false;
                        Errors.Add(_localizerShared["NotEnoughtBalance"].Value.ToString());
                        return (IsValid, Errors);
                    }

                    //اگر قیمت کم شود پولش  منفی میشه
                    if (cientBalance - requestfromDB.PriceForNativeCustomer <= 0)
                    {
                        IsValid = false;
                        Errors.Add(_localizerShared["NotEnoughtBalance"].Value.ToString());
                        return (IsValid, Errors);
                    }
                }
                else
                {
                    if (cientBalance < requestfromDB.PriceForNonNativeCustomer)
                    {
                        IsValid = false;
                        Errors.Add(_localizerShared["NotEnoughtBalance"].Value.ToString());
                        return (IsValid, Errors);
                    }


                    //اگر قیمت کم شود پولش  منفی میشه
                    if (cientBalance - requestfromDB.PriceForNonNativeCustomer <= 0)
                    {
                        IsValid = false;
                        Errors.Add(_localizerShared["NotEnoughtBalance"].Value.ToString());
                        return (IsValid, Errors);
                    }

                }
                //if (isNativeCustomer && cientBalance < requestfromDB.PriceForNativeCustomer)
                //{
                //    List<string> erros = new List<string> { _localizerShared["NotEnoughtBalance"].Value.ToString() };
                //    return BadRequest(new ApiBadRequestResponse(erros));
                //}
                //else if (cientBalance < requestfromDB.PriceForNonNativeCustomer)
                //{
                //    List<string> erros = new List<string> { _localizerShared["NotEnoughtBalance"].Value.ToString() };
                //    return BadRequest(new ApiBadRequestResponse(erros));
                //}

            }
            return (IsValid, Errors);

        }


    }
}
