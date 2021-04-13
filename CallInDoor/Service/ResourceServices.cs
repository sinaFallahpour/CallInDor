﻿using Domain;
using Domain.DTO.Resource;
using Domain.Enums;
using Domain.Utilities;
using ICSharpCode.Decompiler.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ResourceServices : IResourceServices
    {


        #region  ctor

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<ShareResource> _localizerShared;


        public ResourceServices(IHttpContextAccessor httpContextAccessor, IStringLocalizer<ShareResource> localizerShared)
        {
            _httpContextAccessor = httpContextAccessor;
            _localizerShared = localizerShared;
        }




        #endregion

        public override string SetKeyName(string keyName, object id)
        {
            var key = keyName + id;
            return key;
        }




        public override (bool succsseded, List<string> result) ValidateAcceptLanguageHeader()
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();

            var header = GetAcceptLanguageHeader();

            //english or farsi or arab
            if (header != "en-US" && header != "fa-IR" && header != "ar")
            {
                IsValid = false;
                Errors.Add("Invalid Header");
                return (IsValid, Errors);
            }
            return (IsValid, Errors);
        }




        /// <summary>
        /// گرفتن header acept language
        /// </summary>
        /// <returns></returns>
        public override string GetAcceptLanguageHeader()
        {
            var userLangs = _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();
            return userLangs;
        }






        /// <summary>
        /// get all Data Anotation  key value
        /// </summary>
        /// <returns></returns>
        public override List<KeyValueDTO> GetDataAnotationAndErrorMessages()
        {
            var currentAcceptLAnguageHeader = GetAcceptLanguageHeader();

            var data = new DataAnotationAndErrorMessageDTO(_localizerShared);
            var model = new List<KeyValueDTO>();
            foreach (var prop in data.GetType().GetProperties())
            {
                var obj = new KeyValueDTO()
                {
                    Name = prop.Name,
                    Value = prop.GetValue(data, null).ToString(),
                };
                model.Add(obj);
            }
            return model;

        }


        /// <summary>
        /// گرفتن ادرس فایل ریسورس فعلی
        /// </summary>
        /// <returns></returns>
        public override string GetCurrentResourceFileAddress()
        {
            var currentAcceptLAnguageHeader = GetAcceptLanguageHeader();
            var address = "";
            if (currentAcceptLAnguageHeader == "fa-IR")
                address = @"..\Domain\ShareResource.fa-IR.resx";
            else if (currentAcceptLAnguageHeader == "en-US")
                address = @"..\Domain\ShareResource.en-US.resx";
            else if (currentAcceptLAnguageHeader == "ar")
                address = @"..\Domain\ShareResource.ar.resx";
            return address;
        }



        public override (bool succsseded, List<string> result) AddToShareResource(EditDataAnotationAndErrorMessageDTO model)
        {

            bool IsValid = true;
            List<string> Errors = new List<string>();

            if (model == null)
            {
                IsValid = false;
                Errors.Add("Invalid data");
                return (IsValid, Errors);
            }

            var address = GetCurrentResourceFileAddress();

            try
            {


                //using (ResXResourceWriter resx = new ResXResourceWriter(@"..\Domain\ShareResource.en-ca.resx"))
                using (ResXResourceWriter resx = new ResXResourceWriter(address))
                {
                    var sasasa = nameof(model.BlockUserMessage);
                    resx.AddResource(sasasa, model.BlockUserMessage);
                    resx.AddResource(nameof(model.cardNameAlreadyExist), model.cardNameAlreadyExist);
                    resx.AddResource(nameof(model.ConfirmPhoneMessage), model.ConfirmPhoneMessage);
                    resx.AddResource(nameof(model.EditableProfileNotAllowed), model.EditableProfileNotAllowed);
                    resx.AddResource(nameof(model.ErrorMessage), model.ErrorMessage);
                    resx.AddResource(nameof(model.ForniddenMessage), model.ForniddenMessage);
                    resx.AddResource(nameof(model.HasReserveRequest), model.HasReserveRequest);
                    resx.AddResource(nameof(model.InCorrectPassword), model.InCorrectPassword);
                    resx.AddResource(nameof(model.InternalServerMessage), model.InternalServerMessage);
                    resx.AddResource(nameof(model.InvaliAmountForTransaction), model.InvaliAmountForTransaction);
                    resx.AddResource(nameof(model.InvalidAnswer), model.InvalidAnswer);



                    resx.AddResource(nameof(model.InvalidaServiceCategory), model.InvalidaServiceCategory);
                    resx.AddResource(nameof(model.InvalidAttamp), model.InvalidAttamp);
                    resx.AddResource(nameof(model.InvalidDiscointCode), model.InvalidDiscointCode);
                    resx.AddResource(nameof(model.InvalidInput), model.InvalidInput);
                    resx.AddResource(nameof(model.InvalidPackageType), model.InvalidPackageType);
                    resx.AddResource(nameof(model.InvalidPhoneNumber), model.InvalidPhoneNumber);
                    resx.AddResource(nameof(model.InvalidQuestion), model.InvalidQuestion);
                    resx.AddResource(nameof(model.InValidServiceType), model.InValidServiceType);
                    resx.AddResource(nameof(model.LockedOutMessage), model.LockedOutMessage);
                    resx.AddResource(nameof(model.Noquestoin), model.Noquestoin);
                    resx.AddResource(nameof(model.NotEnoughtBalance), model.NotEnoughtBalance);
                    resx.AddResource(nameof(model.NotFound), model.NotFound);

                    resx.AddResource(nameof(model.NumberOfFreeMessagesCompleted), model.NumberOfFreeMessagesCompleted);
                    resx.AddResource(nameof(model.PhoneNumberAlreadyExist), model.PhoneNumberAlreadyExist);
                    resx.AddResource(nameof(model.problemUploadingTheFileMessage), model.problemUploadingTheFileMessage);
                    resx.AddResource(nameof(model.ProfileRejectedMessage), model.ProfileRejectedMessage);
                    resx.AddResource(nameof(model.ProviderIsUnAvailableMessage), model.ProviderIsUnAvailableMessage);
                    resx.AddResource(nameof(model.RequestNotConfirmedMessgaes), model.RequestNotConfirmedMessgaes);
                    resx.AddResource(nameof(model.ServiceIsDisabled), model.ServiceIsDisabled);
                    resx.AddResource(nameof(model.ServiceNotFound), model.ServiceNotFound);
                    resx.AddResource(nameof(model.somethingWentWrongForChaing), model.somethingWentWrongForChaing);
                    resx.AddResource(nameof(model.SuccessMessage), model.SuccessMessage);
                    resx.AddResource(nameof(model.TextIsRequired), model.TextIsRequired);

                    resx.AddResource(nameof(model.TheFileIsInValid), model.TheFileIsInValid);
                    resx.AddResource(nameof(model.TheFileIsRequired), model.TheFileIsRequired);
                    resx.AddResource(nameof(model.TheFileIsTooLarge), model.TheFileIsTooLarge);
                    resx.AddResource(nameof(model.TicketIsClosedMessage), model.TicketIsClosedMessage);

                    resx.AddResource(nameof(model.UnauthorizedMessage), model.UnauthorizedMessage);
                    resx.AddResource(nameof(model.UnMathPhoneNumberPassword), model.UnMathPhoneNumberPassword);
                    resx.AddResource(nameof(model.VoiceIsrequired), model.VoiceIsrequired);
                    resx.AddResource(nameof(model.VoiceIsTooLarge), model.VoiceIsTooLarge);
                    resx.AddResource(nameof(model.YouAreProviderOfThisService), model.YouAreProviderOfThisService);
                    resx.AddResource(nameof(model.YouCantRequestToACompay), model.YouCantRequestToACompay);
                    resx.AddResource(nameof(model.YouCantRequestToYourSelf), model.YouCantRequestToYourSelf);


                    resx.AddResource(nameof(model.YouCurrentlyHaveAnActivePackage), model.YouCurrentlyHaveAnActivePackage);
                    resx.AddResource(nameof(model.YouDOntAnyWallet), model.YouDOntAnyWallet);
                    resx.AddResource(nameof(model.YouDontHaveAnyPackage), model.YouDontHaveAnyPackage);
                    resx.AddResource(nameof(model.YouHaveReservedService), model.YouHaveReservedService);
                    resx.AddResource(nameof(model.YourPackageExpired), model.YourPackageExpired);
                    resx.AddResource(nameof(model.YourPackageExpiredOrNoPlan), model.YourPackageExpiredOrNoPlan);





                    //resx.AddResource("HeaderString1", "Make222222");
                    //resx.AddResource("HeaderString2", "Model");
                    //resx.AddResource("HeaderString3", "Year");
                    //resx.AddResource("HeaderString4", "Doors");
                    //resx.AddResource("HeaderString5", "Cylinders");
                    ////resx.AddResource("Information", SystemIcons.Information);
                    //resx.AddResource("EarlyAuto1", "12");
                    //resx.AddResource("EarlyAuto2", "1");
                }

                return (IsValid, Errors);



            }
            catch
            {
                IsValid = false;
                Errors.Add(PubicMessages.UnAuthorizeMessage);
                return (IsValid, Errors);
            }

        }


    }
}
