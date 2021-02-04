using Domain;
using ICSharpCode.Decompiler.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Primitives;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Service.Interfaces.Company;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
        private readonly ICompanyService _companyService;
        private IStringLocalizer<ShareResource> _localizerShared;

        public ResourceController(

                    DataContext context,
                   IAccountService accountService,
                   ICommonService commonService,
                   ICompanyService companyService,
                IStringLocalizer<ShareResource> localizerShared
            )
        {
            _context = context;
            _accountService = accountService;
            _commonService = commonService;
            _companyService = companyService;
            _localizerShared = localizerShared;

        }


        #endregion ctor


        [HttpGet("res")]
        public async Task<ActionResult> AddToResourse()
        {

            //Create a .resx file
            var currentUsername = _accountService.GetCurrentUserName();


            // Instantiate an Automobile object.
            //Automobile car1 = new Automobile("Ford", "Model N", 1906, 0, 4);
            //Automobile car2 = new Automobile("Ford", "Model T", 1909, 2, 4);
            // Define a resource file named CarResources.resx.
            //using (ResXResourceWriter resx = new ResXResourceWriter(@".\CarResources.resx"))
            //using (ResXResourceWriter resx = new ResXResourceWriter(@"..\Domain\asasa.resx"))
            using (ResXResourceWriter resx = new ResXResourceWriter(@"..\Domain\ShareResource.en-ca.resx"))
            {
                resx.AddResource("Title", "Classic American Cars");
                resx.AddResource("HeaderString1", "Make222222");
                resx.AddResource("HeaderString2", "Model");
                resx.AddResource("HeaderString3", "Year");
                resx.AddResource("HeaderString4", "Doors");
                resx.AddResource("HeaderString5", "Cylinders");
                //resx.AddResource("Information", SystemIcons.Information);
                resx.AddResource("EarlyAuto1", "12");
                resx.AddResource("EarlyAuto2", "1");
            }
            return Ok();
        }




        //Enumerate resources
        [HttpGet("EnumerateResources")]
        public async Task<ActionResult> EnumerateResources()
        {

            //Create a .resx file
            //var currentUsername = _accountService.GetCurrentUserName();


            // Instantiate an Automobile object.
            //Automobile car1 = new Automobile("Ford", "Model N", 1906, 0, 4);
            //Automobile car2 = new Automobile("Ford", "Model T", 1909, 2, 4);
            // Define a resource file named CarResources.resx.
            //using (ResXResourceWriter resx = new ResXResourceWriter(@".\CarResources.resx"))
            //using (ResXResourceWriter resx = new ResXResourceWriter(@"..\Domain\asasa.resx"))
            //using (ResXResourceWriter resx = new ResXResourceWriter(@"..\Domain\ShareResource.en-ca.resx"))
            //{
            //    resx.AddResource("Title", "Classic American Cars");
            //    resx.AddResource("HeaderString1", "Make222222");
            //    resx.AddResource("HeaderString2", "Model");
            //    resx.AddResource("HeaderString3", "Year");
            //    resx.AddResource("HeaderString4", "Doors");
            //    resx.AddResource("HeaderString5", "Cylinders");
            //    //resx.AddResource("Information", SystemIcons.Information);
            //    resx.AddResource("EarlyAuto1", "12");
            //    resx.AddResource("EarlyAuto2", "1");

            //}







            List<string> keys = new List<string>();
            List<string> keys2 = new List<string>();
            List<string> values2 = new List<string>();
            string resxFile = @"..\Domain\asasa.resx";
            //List<Automobile> autos = new List<Automobile>();
            //SortedList headers = new SortedList();

            using (var resxReader = new System.Resources.ResourceReader(resxFile))
            {
                foreach (DictionaryEntry entry in resxReader)
                {
                    if (((string)entry.Key).StartsWith("EarlyAuto"))
                        keys.Add(entry.Value.ToString());
                    //autos.Add((Automobile)entry.Value);
                    else if (((string)entry.Key).StartsWith("Header"))
                    {

                        keys2.Add((string)entry.Key);
                        values2.Add((string)entry.Value);
                    }
                    //headers.Add((string)entry.Key, (string)entry.Value);
                }
            }
            //string[] headerColumns = new string[headers.Count];
            //headers.GetValueList().CopyTo(headerColumns, 0);
            //Console.WriteLine("{0,-8} {1,-10} {2,-4}   {3,-5}   {4,-9}\n",
            //                  headerColumns);
            //foreach (var auto in autos)
            //    Console.WriteLine("{0,-8} {1,-10} {2,4}   {3,5}   {4,9}",
            //                      auto.Make, auto.Model, auto.Year,
            //                      auto.Doors, auto.Cylinders);
            return Ok();
        }







        //Enumerate resources
        [HttpGet("GEtResource")]
        public async Task<ActionResult> GEtResource()
        {


            //HttpContext.Request.Headers;

            StringValues hederval;
            string headerName = string.Empty;
            HttpContext.Request.Headers.TryGetValue("Accept-Language", out hederval);


            var dsdsd = _localizerShared["card name already exist"].Value.ToString();
            var sa = _localizerShared["card name already exist"].Value.ToString();

            return Ok();
        }





        //Enumerate resources
        [HttpGet("DBRedource")]
        public async Task<ActionResult> DBRedource()
        {

            var isPersian = _commonService.IsPersianLanguage();


            var sdsds = _context.ServiceTBL.Where(c => c.IsEnabled == true).Select(c => new
            {
                sss = _localizerShared["card name already exist"].Value.ToString(),
                sss21 = _localizerShared[c.Name].Value.ToString(),
            }).FirstOrDefault();

            //HttpContext.Request.Headers;

            //StringValues hederval;
            //string headerName = string.Empty;
            //HttpContext.Request.Headers.TryGetValue("Accept-Language", out hederval);


            var dsdsd = _localizerShared["card name already exist"].Value.ToString();
            var sa = _localizerShared["card name already exist"].Value.ToString();

            return Ok();
        }


    }
}
