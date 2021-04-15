using CallInDoor.Config.Attributes;
using Domain;
using Domain.DTO.Resource;
using Domain.DTO.Response;
using Domain.Enums;
using Domain.Utilities;
using ICSharpCode.Decompiler.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Service.Interfaces.Company;
using Service.Interfaces.Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace CallInDoor.Controllers
{

    //[Route("api/[controller]")]
    //[ApiController]
    public class ResourceController : BaseControlle
    {

        #region ctor


        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly ICommonService _commonService;
        private readonly IResourceServices _resourceServices;

        private IStringLocalizer<ShareResource> _localizerShared;


        private readonly IWebHostEnvironment _hostingEnvironment;

        public ResourceController(

                    DataContext context,
                   IAccountService accountService,
                   ICommonService commonService,
                   IResourceServices resourceServices,
                 IStringLocalizer<ShareResource> localizerShared,
                IWebHostEnvironment hostingEnvironment

            )
        {
            _context = context;
            _accountService = accountService;
            _commonService = commonService;
            _resourceServices = resourceServices;
            _localizerShared = localizerShared;

            _hostingEnvironment = hostingEnvironment;
        }


        #endregion ctor



        [HttpGet("GetErrorMessages")]
        public ActionResult GetErrorMessages(LanguageHeader languageHeader)
        {
            List<KeyValueDTO> keyValueDTO = _resourceServices.GetAllErrorMessage(languageHeader);
            return Ok(_commonService.OkResponse(keyValueDTO, true));
        }


        [HttpPost("EditErrorMessagess")]
        [ClaimsAuthorize(IsAdmin = true)]
        public ActionResult EditErrorMessages([FromBody] EditErrorMessageDTO2 model)
        {
            (bool succsseded, List<string> result) res = (true, new List<string>());

            if (model.LanguageHeader == LanguageHeader.Persian)
            {
                res = _resourceServices.EditJsonResource(model, "fa-IR");
            }
            else if (model.LanguageHeader == LanguageHeader.English)
            {
                res = _resourceServices.EditJsonResource(model, "en-US");
            }
            else if (model.LanguageHeader == LanguageHeader.Arab)
            {
                res = _resourceServices.EditJsonResource(model, "ar");
            }

            if (!res.succsseded)
            {
                return BadRequest(new ApiBadRequestResponse(res.result));
            }

            //string towrite = JsonConvert.SerializeObject(errorMessageDictionary);
            //System.IO.File.WriteAllText(filePath, towrite);
            return Ok(_commonService.OkResponse(null, PubicMessages.SuccessMessage));

        }



        ///// <summary>
        ///// گرفتن مقادیر دیتا انئتیشن ها
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("GetDataAnotationAndErrorMessages")]
        //[ClaimsAuthorize(IsAdmin = true)]
        //public ActionResult GetDataAnotationAndErrorMessages()
        //{
        //    var data = _resourceServices.GetDataAnotationAndErrorMessages();
        //    return Ok(_commonService.OkResponse(data, PubicMessages.SuccessMessage));
        //}




        ///////// <summary>
        ///////// آ=دیت کردن فایل ریسورس مربوط به دیتا انوتیشن ها و ارور مسیج ها
        ///////// </summary>
        ///////// <param name="model"></param>
        ///////// <returns></returns>
        //////[HttpPost("EditDataAnotationAndErrorMessages")]
        ////////[Authorize]
        //////[ClaimsAuthorize(IsAdmin = true)]
        //////public ActionResult EditDataAnotationAndErrorMessages([FromBody] EditDataAnotationAndErrorMessageDTO model)
        //////{
        //////    //////////var asasa = nameof(model.BlockUserMessage);
        //////    //////////var saass = asasa;
        //////    ///
        //////    var userLangs = Request.HttpContext.Request.Headers["Accept-Language"].ToString();

        //////    var res = _resourceServices.ValidateAcceptLanguageHeader();
        //////    if (!res.succsseded)
        //////    {
        //////        return BadRequest(new ApiBadRequestResponse(res.result));
        //////    }
        //////    //var currentUsername = _accountService.GetCurrentUserName();

        //////    //try
        //////    //{

        //////    var result = _resourceServices.AddToShareResource(model);
        //////    if (result.succsseded == false)
        //////    {
        //////        return BadRequest(new ApiBadRequestResponse(result.result));
        //////    }
        //////    return Ok(_commonService.OkResponse(null, true));
        //////    //}
        //////    //catch
        //////    //{
        //////    //    List<string> error = new List<string> { PubicMessages.InternalServerMessage };
        //////    //    return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(error, 500));
        //////    //}
        //////}




        ///////// <summary>
        ///////// گرفتن مقادیر دیتا انئتیشن ها
        ///////// </summary>
        ///////// <returns></returns>
        //////[HttpGet("GetDataAnotationAndErrorMessages")]
        //////[ClaimsAuthorize(IsAdmin = true)]
        //////public ActionResult GetDataAnotationAndErrorMessages()
        //////{
        //////    var data = _resourceServices.GetDataAnotationAndErrorMessages();
        //////    return Ok(_commonService.OkResponse(data, PubicMessages.SuccessMessage));






        //////    //Create a .resx file
        //////    //var currentUsername = _accountService.GetCurrentUserName();


        //////    // Instantiate an Automobile object.
        //////    //Automobile car1 = new Automobile("Ford", "Model N", 1906, 0, 4);
        //////    //Automobile car2 = new Automobile("Ford", "Model T", 1909, 2, 4);
        //////    // Define a resource file named CarResources.resx.
        //////    //using (ResXResourceWriter resx = new ResXResourceWriter(@".\CarResources.resx"))
        //////    //using (ResXResourceWriter resx = new ResXResourceWriter(@"..\Domain\asasa.resx"))
        //////    //using (ResXResourceWriter resx = new ResXResourceWriter(@"..\Domain\ShareResource.en-ca.resx"))
        //////    //{
        //////    //    resx.AddResource("Title", "Classic American Cars");
        //////    //    resx.AddResource("HeaderString1", "Make222222");
        //////    //    resx.AddResource("HeaderString2", "Model");
        //////    //    resx.AddResource("HeaderString3", "Year");
        //////    //    resx.AddResource("HeaderString4", "Doors");
        //////    //    resx.AddResource("HeaderString5", "Cylinders");
        //////    //    //resx.AddResource("Information", SystemIcons.Information);
        //////    //    resx.AddResource("EarlyAuto1", "12");
        //////    //    resx.AddResource("EarlyAuto2", "1");

        //////    //}







        //////    //    List<string> keys = new List<string>();
        //////    //    List<string> keys2 = new List<string>();
        //////    //    List<string> values2 = new List<string>();
        //////    //    string resxFile = @"..\Domain\asasa.resx";
        //////    //    //List<Automobile> autos = new List<Automobile>();
        //////    //    //SortedList headers = new SortedList();

        //////    //    using (var resxReader = new System.Resources.ResourceReader(resxFile))
        //////    //    {
        //////    //        foreach (DictionaryEntry entry in resxReader)
        //////    //        {






        //////    //            if (((string)entry.Key).StartsWith(nameof(entry.Key)  "EarlyAuto"))
        //////    //                keys.Add(entry.Value.ToString());
        //////    //            //autos.Add((Automobile)entry.Value);
        //////    //            else if (((string)entry.Key).StartsWith("Header"))
        //////    //            {

        //////    //                keys2.Add((string)entry.Key);
        //////    //                values2.Add((string)entry.Value);
        //////    //            }
        //////    //            //headers.Add((string)entry.Key, (string)entry.Value);
        //////    //        }
        //////    //    }
        //////    //    //string[] headerColumns = new string[headers.Count];
        //////    //    //headers.GetValueList().CopyTo(headerColumns, 0);
        //////    //    //Console.WriteLine("{0,-8} {1,-10} {2,-4}   {3,-5}   {4,-9}\n",
        //////    //    //                  headerColumns);
        //////    //    //foreach (var auto in autos)
        //////    //    //    Console.WriteLine("{0,-8} {1,-10} {2,4}   {3,5}   {4,9}",
        //////    //    //                      auto.Make, auto.Model, auto.Year,
        //////    //    //                      auto.Doors, auto.Cylinders);
        //////    //    return Ok();
        //////    //}
























        //////    //[HttpGet("res")]
        //////    //public async Task<ActionResult> AddToResourse()
        //////    //{


        //////    //    var d = nameof(ssss.Heloo);

        //////    //    //Create a .resx file
        //////    //    var currentUsername = _accountService.GetCurrentUserName();


        //////    //    // Instantiate an Automobile object.
        //////    //    //Automobile car1 = new Automobile("Ford", "Model N", 1906, 0, 4);
        //////    //    //Automobile car2 = new Automobile("Ford", "Model T", 1909, 2, 4);
        //////    //    // Define a resource file named CarResources.resx.
        //////    //    //using (ResXResourceWriter resx = new ResXResourceWriter(@".\CarResources.resx"))
        //////    //    //using (ResXResourceWriter resx = new ResXResourceWriter(@"..\Domain\asasa.resx"))
        //////    //    using (ResXResourceWriter resx = new ResXResourceWriter(@"..\Domain\ShareResource.en-ca.resx"))
        //////    //    {
        //////    //        resx.AddResource("Title", "Classic American Cars");
        //////    //        resx.AddResource("HeaderString1", "Make222222");
        //////    //        resx.AddResource("HeaderString2", "Model");
        //////    //        resx.AddResource("HeaderString3", "Year");
        //////    //        resx.AddResource("HeaderString4", "Doors");
        //////    //        resx.AddResource("HeaderString5", "Cylinders");
        //////    //        //resx.AddResource("Information", SystemIcons.Information);
        //////    //        resx.AddResource("EarlyAuto1", "12");
        //////    //        resx.AddResource("EarlyAuto2", "1");
        //////    //    }
        //////    //    return Ok();
        //////    //}




        //////    ////Enumerate resources
        //////    //[HttpGet("EnumerateResources")]
        //////    //public async Task<ActionResult> EnumerateResources()
        //////    //{

        //////    //    //Create a .resx file
        //////    //    //var currentUsername = _accountService.GetCurrentUserName();


        //////    //    // Instantiate an Automobile object.
        //////    //    //Automobile car1 = new Automobile("Ford", "Model N", 1906, 0, 4);
        //////    //    //Automobile car2 = new Automobile("Ford", "Model T", 1909, 2, 4);
        //////    //    // Define a resource file named CarResources.resx.
        //////    //    //using (ResXResourceWriter resx = new ResXResourceWriter(@".\CarResources.resx"))
        //////    //    //using (ResXResourceWriter resx = new ResXResourceWriter(@"..\Domain\asasa.resx"))
        //////    //    //using (ResXResourceWriter resx = new ResXResourceWriter(@"..\Domain\ShareResource.en-ca.resx"))
        //////    //    //{
        //////    //    //    resx.AddResource("Title", "Classic American Cars");
        //////    //    //    resx.AddResource("HeaderString1", "Make222222");
        //////    //    //    resx.AddResource("HeaderString2", "Model");
        //////    //    //    resx.AddResource("HeaderString3", "Year");
        //////    //    //    resx.AddResource("HeaderString4", "Doors");
        //////    //    //    resx.AddResource("HeaderString5", "Cylinders");
        //////    //    //    //resx.AddResource("Information", SystemIcons.Information);
        //////    //    //    resx.AddResource("EarlyAuto1", "12");
        //////    //    //    resx.AddResource("EarlyAuto2", "1");

        //////    //    //}







        //////    //    List<string> keys = new List<string>();
        //////    //    List<string> keys2 = new List<string>();
        //////    //    List<string> values2 = new List<string>();
        //////    //    string resxFile = @"..\Domain\asasa.resx";
        //////    //    //List<Automobile> autos = new List<Automobile>();
        //////    //    //SortedList headers = new SortedList();

        //////    //    using (var resxReader = new System.Resources.ResourceReader(resxFile))
        //////    //    {
        //////    //        foreach (DictionaryEntry entry in resxReader)
        //////    //        {
        //////    //            if (((string)entry.Key).StartsWith(nameof(entry.Key)  "EarlyAuto"))
        //////    //                keys.Add(entry.Value.ToString());
        //////    //            //autos.Add((Automobile)entry.Value);
        //////    //            else if (((string)entry.Key).StartsWith("Header"))
        //////    //            {

        //////    //                keys2.Add((string)entry.Key);
        //////    //                values2.Add((string)entry.Value);
        //////    //            }
        //////    //            //headers.Add((string)entry.Key, (string)entry.Value);
        //////    //        }
        //////    //    }
        //////    //    //string[] headerColumns = new string[headers.Count];
        //////    //    //headers.GetValueList().CopyTo(headerColumns, 0);
        //////    //    //Console.WriteLine("{0,-8} {1,-10} {2,-4}   {3,-5}   {4,-9}\n",
        //////    //    //                  headerColumns);
        //////    //    //foreach (var auto in autos)
        //////    //    //    Console.WriteLine("{0,-8} {1,-10} {2,4}   {3,5}   {4,9}",
        //////    //    //                      auto.Make, auto.Model, auto.Year,
        //////    //    //                      auto.Doors, auto.Cylinders);
        //////    //    return Ok();
        //////    //}







        //////    ////Enumerate resources
        //////    //[HttpGet("GEtResource")]
        //////    //public async Task<ActionResult> GEtResource()
        //////    //{

        //////    //    var ssss = nameof(ssss.Heloo)
        //////    //    //HttpContext.Request.Headers;

        //////    //    StringValues hederval;
        //////    //    string headerName = string.Empty;
        //////    //    HttpContext.Request.Headers.TryGetValue("Accept-Language", out hederval);


        //////    //    var dsdsd = _localizerShared["card name already exist"].Value.ToString();
        //////    //    var sa = _localizerShared["card name already exist"].Value.ToString();

        //////    //    return Ok();
        //////    //}





        //////    ////Enumerate resources
        //////    //[HttpGet("DBRedource")]
        //////    //public async Task<ActionResult> DBRedource()
        //////    //{

        //////    //    var isPersian = _commonService.IsPersianLanguage();


        //////    //    var sdsds = _context.ServiceTBL.Where(c => c.IsEnabled == true).Select(c => new
        //////    //    {
        //////    //        sss = _localizerShared["cardNameAlreadyExist"].Value.ToString(),
        //////    //        sss21 = _localizerShared[c.Name].Value.ToString(),
        //////    //    }).FirstOrDefault();

        //////    //    //HttpContext.Request.Headers;

        //////    //    //StringValues hederval;
        //////    //    //string headerName = string.Empty;
        //////    //    //HttpContext.Request.Headers.TryGetValue("Accept-Language", out hederval);


        //////    //    var dsdsd = _localizerShared["card name already exist"].Value.ToString();
        //////    //    var sa = _localizerShared["card name already exist"].Value.ToString();

        //////    //    return Ok();
        //////    //}


        //////}




    }
}
