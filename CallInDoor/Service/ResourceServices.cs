//using Domain.DTO.Resource;
//using ICSharpCode.Decompiler.Util;
//using Microsoft.AspNetCore.Http;
//using Service.Interfaces.Resource;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace Service
//{
//    public class ResourceServices : IResourceServices
//    {


//        #region  ctor

//        private readonly IHttpContextAccessor _httpContextAccessor;


//        //public ResourceServices(IHttpContextAccessor httpContextAccessor)
//        //{
//        //    _httpContextAccessor = httpContextAccessor;
//        //}




//        #endregion

//        public override string SetKeyName(string keyName, object id)
//        {
//            var key = keyName + id;
//            return key;
//        }




//        public override (bool succsseded, List<string> result) ValidateAcceptLanguageHeader()
//        {
//            bool IsValid = true;
//            List<string> Errors = new List<string>();

//            var header = GetAcceptLanguageHeader();

//            //english or farsi or arab
//            if (header != "en-US" || header != "fa-IR" || header != "ar")
//            {
//                IsValid = false;
//                Errors.Add("Invalid Header");
//                return (IsValid, Errors);
//            }
//            return (IsValid, Errors);
//        }




//        /// <summary>
//        /// گرفتن header acept language
//        /// </summary>
//        /// <returns></returns>
//        public override string GetAcceptLanguageHeader()
//        {
//            var userLangs = _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();
//            return userLangs;
//        }





//        public override async Task<bool> AddToShareResource(DataAnotationAndErrorMessageDTO model)
//        {
//            if (model == null) return false;

//            var acceptLanguage = GetAcceptLanguageHeader();
//            var address = "";
//            if (acceptLanguage == "")
//                address = @"..\Domain\ShareResource.en-ca.resx";
//            else if(acceptLanguage == "")
//                address = @"..\Domain\ShareResource.en-ca.resx";
//            else if(acceptLanguage == "" )
//                address = @"..\Domain\ShareResource.en-ca.resx";

//            using (ResXResourceWriter resx = new ResXResourceWriter(@"..\Domain\ShareResource.en-ca.resx"))
//            {
//                resx.AddResource("Title", "Classic American Cars");
//                resx.AddResource("HeaderString1", "Make222222");
//                resx.AddResource("HeaderString2", "Model");
//                resx.AddResource("HeaderString3", "Year");
//                resx.AddResource("HeaderString4", "Doors");
//                resx.AddResource("HeaderString5", "Cylinders");
//                //resx.AddResource("Information", SystemIcons.Information);
//                resx.AddResource("EarlyAuto1", "12");
//                resx.AddResource("EarlyAuto2", "1");
//            }
//            return Ok();





//            var serviceType = new ServiceTBL()
//            {
//                Color = model.Color,
//                Name = model.Name,
//                IsEnabled = model.IsEnabled,
//                PersianName = model.PersianName,
//                RoleId = model.RoleId,
//                MinPriceForService = model.MinPriceForService,
//                MinSessionTime = (double)model.MinSessionTime,
//                AcceptedMinPriceForNative = (double)model.AcceptedMinPriceForNative,
//                AcceptedMinPriceForNonNative = (double)model.AcceptedMinPriceForNonNative,
//                SitePercent = (int)model.SitePercent,
//            };


//            ///add top-ten
//            var topTenPackageTBL = new TopTenPackageTBL()
//            {
//                CreateDate = DateTime.Now,
//                Count = (int)model.UsersCount,
//                //ServiceTbl = serviceType,
//                DayCount = model.DayCount,
//                HourCount = model.HourCount,
//                Price = (double)model.TopTenPackagePrice,
//            };

//            serviceType.TopTenPackageTBL = new List<TopTenPackageTBL>() { topTenPackageTBL };



//            var servicetags = new List<ServiceTagsTBL>();
//            var tags = model?.Tags?.Split(",").ToList();


//        }
//    }
//}
