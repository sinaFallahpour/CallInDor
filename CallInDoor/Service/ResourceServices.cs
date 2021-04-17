using Domain;
using Domain.DTO.Resource;
using Domain.Enums;
using Domain.Utilities;
using ICSharpCode.Decompiler.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Service.Interfaces.Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ResourceServices : IResourceServices
    {


        #region  ctor

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<ShareResource> _localizerShared;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ResourceServices(IHttpContextAccessor httpContextAccessor, IStringLocalizer<ShareResource> localizerShared,
                                                    IHostingEnvironment hostingEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _localizerShared = localizerShared;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion



        /// <summary>
        /// گرفتن header acept language
        /// </summary>
        /// <returns></returns>
        public override string GetCurrentAcceptLanguageHeader()
        {
            var userLangs = _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();
            var firstLang = userLangs.Split(',').FirstOrDefault();
            var defaultLang = string.IsNullOrEmpty(firstLang) ? "en-US" : firstLang;
            return defaultLang;
        }



        /// <summary>
        /// get  object off json ErrorMessagesDitionary file from wwwroot
        /// </summary>
        /// <returns></returns>
        public override ErrorMessageDictionary GetErrorMessageObject()
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Resource/ErrorMessages.json");
            string jsonString = System.IO.File.ReadAllText(filePath);
            var errorMessageDictionary = JsonConvert.DeserializeObject<ErrorMessageDictionary>(jsonString);
            return errorMessageDictionary;
        }

        /// <summary>
        /// get  all erroe messages
        /// </summary>
        /// <param name="languageHeader"></param>
        /// <param name="errorMessageDictionary"></param>
        /// <returns></returns>
        public override List<KeyValueDTO> GetAllErrorMessage(LanguageHeader languageHeader)
        {
            ErrorMessageDictionary errorMessageDictionary = GetErrorMessageObject();
            var keyValueDTO = new List<KeyValueDTO>();
            if (languageHeader == LanguageHeader.Persian)
            {
                keyValueDTO = errorMessageDictionary.Dictionary.Select(c => new KeyValueDTO
                {
                    Name = c.Key,
                    Value = c.Languages.FirstOrDefault(c => c.Header == "fa-IR").Val
                }).ToList();
            }
            else if (languageHeader == LanguageHeader.English)
            {
                keyValueDTO = errorMessageDictionary.Dictionary.Select(c => new KeyValueDTO
                {
                    Name = c.Key,
                    Value = c.Languages.FirstOrDefault(c => c.Header == "en-US").Val
                }).ToList();
            }
            else if (languageHeader == LanguageHeader.Arab)
            {
                keyValueDTO = errorMessageDictionary.Dictionary.Select(c => new KeyValueDTO
                {
                    Name = c.Key,
                    Value = c.Languages.FirstOrDefault(c => c.Header == "ar").Val
                }).ToList();
            }

            return keyValueDTO;
        }


        /// <summary>
        /// Get value of a key by key name
        /// </summary>
        /// <param name="key"></param>
        /// <param name="languageHeader"></param>
        /// <returns></returns>
        public override string GetErrorMessageByKey(string key)
        {
            ErrorMessageDictionary errorMessageDictionary = GetErrorMessageObject();
            var header = GetCurrentAcceptLanguageHeader();

            string value = "";
            if (header == "fa-IR")
            {
                value = errorMessageDictionary.Dictionary.FirstOrDefault(c => c.Key == key)?
                           .Languages.FirstOrDefault(c => c.Header == "fa-IR").Val;
            }
            else if (header == "en-US")
            {
                value = errorMessageDictionary.Dictionary.FirstOrDefault(c => c.Key == key)?
                             .Languages.FirstOrDefault(c => c.Header == "en-US").Val;
            }
            else if (header == "ar")
            {
                value = errorMessageDictionary.Dictionary.FirstOrDefault(c => c.Key == key)?
                             .Languages.FirstOrDefault(c => c.Header == "ar").Val;
            }
            return value;
        }

        /// <summary>
        /// Edit value Of ErrorMessages
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessageDictionary"></param>
        /// <param name="headerName"></param>
        /// <returns></returns>
        public override (bool succsseded, List<string> result) EditJsonResource(EditErrorMessageDTO2 model, string headerName)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();


            ErrorMessageDictionary errorMessageDictionary = GetErrorMessageObject();

            try
            {
                foreach (var item in model.GetType().GetProperties())
                {
                    var errorMessage = errorMessageDictionary.Dictionary.FirstOrDefault(c => c.Key == item.Name);
                    if (errorMessage != null)
                    {
                        errorMessage.Languages.FirstOrDefault(c => c.Header == headerName).Val = item.GetValue(model).ToString();
                    }
                }

                return (IsValid, Errors);
            }
            catch (Exception ex)
            {
                IsValid = false;
                Errors.Add(ex.Message + " ,,  " + ex.InnerException + ",,  ");
                return (IsValid, Errors);
            }

        }


















































        //public override string SetKeyName(string keyName, object id)
        //{
        //    var key = keyName + id;
        //    return key;
        //}




        //public override (bool succsseded, List<string> result) ValidateAcceptLanguageHeader()
        //{
        //    bool IsValid = true;
        //    List<string> Errors = new List<string>();

        //    var headerq = GetAcceptLanguageHeader();
        //    var header = _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();

        //    //english or farsi or arab
        //    if (header != "en-US" && header != "fa-IR" && header != "ar")
        //    {
        //        IsValid = false;
        //        Errors.Add("Invalid Header");
        //        return (IsValid, Errors);
        //    }
        //    return (IsValid, Errors);
        //}




        ///// <summary>
        ///// گرفتن header acept language
        ///// </summary>
        ///// <returns></returns>
        //public override string GetAcceptLanguageHeader()
        //{
        //    var userLangs = _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();
        //    //heade context.Request.Headers["Accept-Language"].ToString();
        //    var firstLang = userLangs.Split(',').FirstOrDefault();
        //    var defaultLang = string.IsNullOrEmpty(firstLang) ? "en-US" : firstLang;

        //    return defaultLang;

        //    //var userLangs = _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();
        //    //return userLangs;
        //}






        ///// <summary>
        ///// get all Data Anotation  key value
        ///// </summary>
        ///// <returns></returns>
        //public override List<KeyValueDTO> GetDataAnotationAndErrorMessages()
        //{
        //    //var currentAcceptLAnguageHeader = GetAcceptLanguageHeader();

        //    //var data = new DataAnotationAndErrorMessageDTO(_localizerShared);
        //    //var model = new List<KeyValueDTO>();
        //    //foreach (var prop in data.GetType().GetProperties())
        //    //{
        //    //    var obj = new KeyValueDTO()
        //    //    {
        //    //        Name = prop.Name,
        //    //        Value = prop.GetValue(data, null).ToString(),
        //    //    };
        //    //    model.Add(obj);
        //    //}
        //    //return model;





        //    //var currentAcceptLAnguageHeader = GetAcceptLanguageHeader();

        //    //var model = new List<KeyValueDTO>();
        //    //foreach (var prop in data.GetType().GetProperties())
        //    //{
        //    //    var obj = new KeyValueDTO()
        //    //    {
        //    //        Name = prop.Name,
        //    //        Value = prop.GetValue(data, null).ToString(),
        //    //    };
        //    //    model.Add(obj);
        //    //}



        //    //روش 2
        //    ////////////var sssss = _localizerShared.WithCulture(new CultureInfo("fa-IR")).GetAllStrings().Select(x => new KeyValueDTO()
        //    ////////////{
        //    ////////////    Name = x.Name,
        //    ////////////    Value = x.Value
        //    ////////////}).ToList();

        //    ////////////return sssss;

        //    //var resourceSet = _localizerShared.WithCulture(new CultureInfo(""))
        //    //                .GetAllStrings().Select(x => x.Name);

        //    //var address = GetCurrentResourceFileAddress();
        //    ResourceManager myManager = new ResourceManager(typeof(ShareResourcefa_IR));
        //    var currentAcceptLAnguageHeader = GetAcceptLanguageHeader();
        //    if (currentAcceptLAnguageHeader == "fa-IR")
        //        myManager = new ResourceManager(typeof(ShareResourcefa_IR));
        //    //myManager = new ResourceManager("ShareResource.fa-IR", Assembly.GetAssembly(typeof(Domain.ShareResourcefa_IR)), typeof(Domain.ShareResourcefa_IR));

        //    else if (currentAcceptLAnguageHeader == "en-US")
        //        myManager = new ResourceManager(typeof(ShareResourceen_US));
        //    //myManager = new ResourceManager("ShareResource.en-US", Assembly.GetAssembly(typeof(Domain.ShareResourceen_US)), typeof(ShareResourceen_US));

        //    else if (currentAcceptLAnguageHeader == "ar")
        //        //myManager = new ResourceManager("ShareResource.ar", Assembly.GetAssembly(typeof(Domain.ShareResource_ar)), typeof(ShareResource_ar));
        //        myManager = new ResourceManager(typeof(Domain.ShareResource_ar));

        //    //address = @"..\Domain\ShareResource.ar.resx";

        //    ////////myManager = new ResourceManager(typeof(Domain.ShareResource) );


        //    ////////var saValue = myManager.GetString("ForniddenMessage");
        //    //string myString = myManager.GetString("StringKey");
        //    var dataAnotationAndErrorMessageDTO = new DataAnotationAndErrorMessageDTO(_localizerShared);
        //    var model = new List<KeyValueDTO>();



        //    foreach (var item in dataAnotationAndErrorMessageDTO.GetType().GetProperties())
        //    {
        //        var obj = new KeyValueDTO()
        //        {
        //            Name = item.Name.ToString() /*entry.Key.ToString()*/,
        //            //Value = item.GetValue(dataAnotationAndErrorMessageDTO, null)?.ToString() /*entry.Value.ToString()*/,
        //            //Value = myManager.GetString(item.Name)  /*entry.Value.ToString()*/,
        //            Value = _localizerShared[item.Name].Value.ToString()   /*entry.Value.ToString()*/,
        //        };
        //        model.Add(obj);

        //        // do stuff here
        //    }


        //    //////////List<string> keys = new List<string>();
        //    //////////List<string> keys2 = new List<string>();
        //    //////////List<string> values2 = new List<string>();
        //    //////////string resxFile = @"..\Domain\asasa.resx";
        //    //List<Automobile> autos = new List<Automobile>();
        //    //SortedList headers = new SortedList();

        //    //////////using (var resxReader = new System.Resources.ResourceReader(address))
        //    //////////{
        //    //////////    foreach (DictionaryEntry entry in resxReader)
        //    //////////    {
        //    //////////        var obj = new KeyValueDTO()
        //    //////////        {
        //    //////////            Name = entry.Key.ToString(),
        //    //////////            Value = entry.Value.ToString(),
        //    //////////        };
        //    //////////        model.Add(obj);

        //    //////////        //if (((string)entry.Key).StartsWith(nameof(entry.Key)  "EarlyAuto"))
        //    //////////        //    keys.Add(entry.Value.ToString());
        //    //////////        ////autos.Add((Automobile)entry.Value);
        //    //////////        //else if (((string)entry.Key).StartsWith("Header"))
        //    //////////        //{

        //    //////////        //    keys2.Add((string)entry.Key);
        //    //////////        //    values2.Add((string)entry.Value);
        //    //////////        //}
        //    //////////        //headers.Add((string)entry.Key, (string)entry.Value);
        //    //////////    }
        //    //////////}

        //    return model;
        //}


        ///// <summary>
        ///// گرفتن ادرس فایل ریسورس فعلی
        ///// </summary>
        ///// <returns></returns>
        //public override string GetCurrentResourceFileAddress()
        //{
        //    var currentAcceptLAnguageHeader = GetAcceptLanguageHeader();
        //    var address = "";
        //    if (currentAcceptLAnguageHeader == "fa-IR")
        //    {
        //        //address = @"..\Domain\ShareResource.fa-IR.resx";
        //        //address = @"..\Domain\ShareResource.fa-IR.resx";
        //        //address = @"..\fa-IR\Domain.resources.dll";
        //        address = @"..\wwwroot\fa-IR\Domain.resources.dll";

        //    }
        //    else if (currentAcceptLAnguageHeader == "en-US")
        //    {
        //        //address = @"..\Domain\ShareResource.en-US.resx";
        //        address = @"..\wwwroot\wwwroot\ShareResource.en-US.resx";
        //    }
        //    else if (currentAcceptLAnguageHeader == "ar")
        //    {

        //        //address = @"..\Domain\ShareResource.ar.resx";
        //        address = @"Domain\ShareResource.ar.resx";


        //    }
        //    return address;
        //}



        //public override (bool succsseded, List<string> result) AddToShareResource(EditDataAnotationAndErrorMessageDTO model)
        //{

        //    bool IsValid = true;
        //    List<string> Errors = new List<string>();

        //    if (model == null)
        //    {
        //        IsValid = false;
        //        Errors.Add("Invalid data");
        //        return (IsValid, Errors);
        //    }

        //    var address = GetCurrentResourceFileAddress();

        //    try
        //    {


        //        //using (ResXResourceWriter resx = new ResXResourceWriter(@"..\Domain\ShareResource.en-ca.resx"))
        //        using (ResXResourceWriter resx = new ResXResourceWriter(address))
        //        {
        //            resx.AddResource(nameof(model.BlockUserMessage), model.BlockUserMessage);
        //            resx.AddResource(nameof(model.cardNameAlreadyExist), model.cardNameAlreadyExist);
        //            resx.AddResource(nameof(model.ConfirmPhoneMessage), model.ConfirmPhoneMessage);
        //            resx.AddResource(nameof(model.EditableProfileNotAllowed), model.EditableProfileNotAllowed);
        //            resx.AddResource(nameof(model.ErrorMessage), model.ErrorMessage);
        //            resx.AddResource(nameof(model.ForniddenMessage), model.ForniddenMessage);
        //            resx.AddResource(nameof(model.HasReserveRequest), model.HasReserveRequest);
        //            resx.AddResource(nameof(model.InCorrectPassword), model.InCorrectPassword);
        //            resx.AddResource(nameof(model.InternalServerMessage), model.InternalServerMessage);
        //            resx.AddResource(nameof(model.InvaliAmountForTransaction), model.InvaliAmountForTransaction);
        //            resx.AddResource(nameof(model.InvalidAnswer), model.InvalidAnswer);



        //            resx.AddResource(nameof(model.InvalidaServiceCategory), model.InvalidaServiceCategory);
        //            resx.AddResource(nameof(model.InvalidAttamp), model.InvalidAttamp);
        //            resx.AddResource(nameof(model.InvalidDiscointCode), model.InvalidDiscointCode);
        //            resx.AddResource(nameof(model.InvalidInput), model.InvalidInput);
        //            resx.AddResource(nameof(model.InvalidPackageType), model.InvalidPackageType);
        //            resx.AddResource(nameof(model.InvalidPhoneNumber), model.InvalidPhoneNumber);
        //            resx.AddResource(nameof(model.InvalidQuestion), model.InvalidQuestion);
        //            resx.AddResource(nameof(model.InValidServiceType), model.InValidServiceType);
        //            resx.AddResource(nameof(model.LockedOutMessage), model.LockedOutMessage);
        //            resx.AddResource(nameof(model.Noquestoin), model.Noquestoin);
        //            resx.AddResource(nameof(model.NotEnoughtBalance), model.NotEnoughtBalance);
        //            resx.AddResource(nameof(model.NotFound), model.NotFound);

        //            resx.AddResource(nameof(model.NumberOfFreeMessagesCompleted), model.NumberOfFreeMessagesCompleted);
        //            resx.AddResource(nameof(model.PhoneNumberAlreadyExist), model.PhoneNumberAlreadyExist);
        //            resx.AddResource(nameof(model.problemUploadingTheFileMessage), model.problemUploadingTheFileMessage);
        //            resx.AddResource(nameof(model.ProfileRejectedMessage), model.ProfileRejectedMessage);
        //            resx.AddResource(nameof(model.ProviderIsUnAvailableMessage), model.ProviderIsUnAvailableMessage);
        //            resx.AddResource(nameof(model.RequestNotConfirmedMessgaes), model.RequestNotConfirmedMessgaes);
        //            resx.AddResource(nameof(model.ServiceIsDisabled), model.ServiceIsDisabled);
        //            resx.AddResource(nameof(model.ServiceNotFound), model.ServiceNotFound);
        //            resx.AddResource(nameof(model.somethingWentWrongForChaing), model.somethingWentWrongForChaing);
        //            resx.AddResource(nameof(model.SuccessMessage), model.SuccessMessage);
        //            resx.AddResource(nameof(model.TextIsRequired), model.TextIsRequired);

        //            resx.AddResource(nameof(model.TheFileIsInValid), model.TheFileIsInValid);
        //            resx.AddResource(nameof(model.TheFileIsRequired), model.TheFileIsRequired);
        //            resx.AddResource(nameof(model.TheFileIsTooLarge), model.TheFileIsTooLarge);
        //            resx.AddResource(nameof(model.TicketIsClosedMessage), model.TicketIsClosedMessage);

        //            resx.AddResource(nameof(model.UnauthorizedMessage), model.UnauthorizedMessage);
        //            resx.AddResource(nameof(model.UnMathPhoneNumberPassword), model.UnMathPhoneNumberPassword);
        //            resx.AddResource(nameof(model.VoiceIsrequired), model.VoiceIsrequired);
        //            resx.AddResource(nameof(model.VoiceIsTooLarge), model.VoiceIsTooLarge);
        //            resx.AddResource(nameof(model.YouAreProviderOfThisService), model.YouAreProviderOfThisService);
        //            resx.AddResource(nameof(model.YouCantRequestToACompay), model.YouCantRequestToACompay);
        //            resx.AddResource(nameof(model.YouCantRequestToYourSelf), model.YouCantRequestToYourSelf);


        //            resx.AddResource(nameof(model.YouCurrentlyHaveAnActivePackage), model.YouCurrentlyHaveAnActivePackage);
        //            resx.AddResource(nameof(model.YouDOntAnyWallet), model.YouDOntAnyWallet);
        //            resx.AddResource(nameof(model.YouDontHaveAnyPackage), model.YouDontHaveAnyPackage);
        //            resx.AddResource(nameof(model.YouHaveReservedService), model.YouHaveReservedService);
        //            resx.AddResource(nameof(model.YourPackageExpired), model.YourPackageExpired);
        //            resx.AddResource(nameof(model.YourPackageExpiredOrNoPlan), model.YourPackageExpiredOrNoPlan);





        //            //resx.AddResource("HeaderString1", "Make222222");
        //            //resx.AddResource("HeaderString2", "Model");
        //            //resx.AddResource("HeaderString3", "Year");
        //            //resx.AddResource("HeaderString4", "Doors");
        //            //resx.AddResource("HeaderString5", "Cylinders");
        //            ////resx.AddResource("Information", SystemIcons.Information);
        //            //resx.AddResource("EarlyAuto1", "12");
        //            //resx.AddResource("EarlyAuto2", "1");

        //            resx.Close();

        //        }

        //        return (IsValid, Errors);



        //    }
        //    catch (Exception ex)
        //    {
        //        IsValid = false;
        //        Errors.Add(ex.Message + " ,,  " + ex.InnerException + ",,  ");
        //        return (IsValid, Errors);
        //    }

        //}






    }
}
