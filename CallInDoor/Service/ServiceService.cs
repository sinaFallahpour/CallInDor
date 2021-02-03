using Domain;
using Domain.DTO.Account;
using Domain.DTO.Service;
using Domain.Entities;
using Domain.Enums;
using Domain.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.ServiceType;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceService : IServiceService
    {
        #region ctor
        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly RoleManager<AppRole> _roleManager;


        private IStringLocalizer<ServiceService> _localizer;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ServiceService(
            DataContext context,
            IAccountService accountService,
            RoleManager<AppRole> roleManager,
            IStringLocalizer<ServiceService> localizer,
            IHostingEnvironment hostingEnvironment
               )
        {
            _context = context;
            _accountService = accountService;
            _roleManager = roleManager;
            _hostingEnvironment = hostingEnvironment;
            _localizer = localizer;
        }

        #endregion



        /// <summary>
        /// get by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ServiceTBL GetById(int Id)
        {
            return _context.ServiceTBL.Find(Id);
        }






        public async Task<ServiceTBL> GetByIdWithJoin(int Id)
        {
            return await _context.ServiceTBL
                .Where(c => c.Id == Id)
                .Include(c => c.TopTenPackageTBL)
                .Include(c => c.Tags)
                .Include(c => c.ServidceTypeRequiredCertificatesTBL)
               .Include(c => c.TopTenPackageTBL)
                .FirstOrDefaultAsync();
        }





        /// <summary>
        /// get All servicesType
        /// </summary>
        public Task<List<ListServiceDTO>> GetAllActiveService()
        {
            return _context.ServiceTBL.Where(c => c.IsEnabled)
                .AsNoTracking()
                .Select(c => new ListServiceDTO
                {
                    Id = c.Id,
                    Color = c.Color,
                    IsEnabled = c.IsEnabled,
                    Name = c.Name,
                    PersianName = c.PersianName,
                    RoleName = c.AppRole.Name,
                    SitePercent = c.SitePercent,
                    IsProfileOptional = c.IsProfileOptional,
                    AcceptedMinPriceForNative = c.AcceptedMinPriceForNative,
                    AcceptedMinPriceForNonNative = c.AcceptedMinPriceForNonNative,
                    MinPriceForService = c.MinPriceForService,
                    MinSessionTime = c.MinSessionTime,
                }).ToListAsync();
        }





        /// <summary>
        /// Create
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task<bool> Create(CreateServiceDTO model)
        {
            if (model == null) return false;

            var serviceType = new ServiceTBL()
            {
                Color = model.Color,
                Name = model.Name,
                IsEnabled = model.IsEnabled,
                PersianName = model.PersianName,
                RoleId = model.RoleId,
                MinPriceForService = model.MinPriceForService,
                MinSessionTime = (double)model.MinSessionTime,
                AcceptedMinPriceForNative = (double)model.AcceptedMinPriceForNative,
                AcceptedMinPriceForNonNative = (double)model.AcceptedMinPriceForNonNative,
                SitePercent = (int)model.SitePercent,
            };


            ///add top-ten
            var topTenPackageTBL = new TopTenPackageTBL()
            {
                CreateDate = DateTime.Now,
                Count = (int)model.UsersCount,
                //ServiceTbl = serviceType,
                DayCount = model.DayCount,
                HourCount = model.HourCount,
                Price = (double)model.TopTenPackagePrice,
            };

            serviceType.TopTenPackageTBL = new List<TopTenPackageTBL>() { topTenPackageTBL };



            var servicetags = new List<ServiceTagsTBL>();
            var tags = model?.Tags?.Split(",").ToList();


            foreach (var item in tags)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var tag = new ServiceTagsTBL()
                    {
                        IsEnglisTags = true,
                        TagName = item.Trim(),
                        Service = serviceType,
                    };
                    servicetags.Add(tag);
                }
            }

            var persinaTags = model?.PersinaTags?.Split(",").ToList();
            if (persinaTags != null)
            {
                foreach (var item in persinaTags)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var tag = new ServiceTagsTBL()
                        {
                            IsEnglisTags = false,
                            PersianTagName = item.Trim(),
                            Service = serviceType,
                        };
                        servicetags.Add(tag);
                    }
                }
            }
            serviceType.Tags = servicetags;

            if (model.RequiredFiles != null)
            {
                serviceType.ServidceTypeRequiredCertificatesTBL = new List<ServidceTypeRequiredCertificatesTBL>();
                foreach (var item in model.RequiredFiles)
                {
                    var requiredFile = new ServidceTypeRequiredCertificatesTBL()
                    {
                        FileName = item.FileName,
                        PersianFileName = item.PersianFileName,
                        Isdeleted = false,
                        Service = serviceType
                    };
                    serviceType.ServidceTypeRequiredCertificatesTBL.Add(requiredFile);
                }
            }

            try
            {
                await _context.ServiceTBL.AddAsync(serviceType);
                //await _context.TopTenPackageTBL.AddAsync(topTenPackageTBL);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        /// <summary>
        ///Update
        /// </summary>
        /// <param name="Service"></param>
        /// <returns></returns>
        public async Task<bool> Update(ServiceTBL serviceFromDB, CreateServiceDTO model)
        {
            try
            {
                //var serviceFromDb = await _context.ServiceTBL.FindAsync(model.Id);
                if (model == null) return false;
                serviceFromDB.PersianName = model.PersianName;
                serviceFromDB.Name = model.Name;
                serviceFromDB.IsEnabled = model.IsEnabled;
                serviceFromDB.SitePercent = (int)model.SitePercent;
                serviceFromDB.Color = model.Color;
                serviceFromDB.MinPriceForService = (double)model.MinPriceForService;
                serviceFromDB.MinSessionTime = (double)model.MinSessionTime;
                serviceFromDB.AcceptedMinPriceForNative = (double)model.AcceptedMinPriceForNative;
                serviceFromDB.AcceptedMinPriceForNonNative = (double)model.AcceptedMinPriceForNonNative;
                serviceFromDB.RoleId = model.RoleId;

                serviceFromDB.TopTenPackageTBL.FirstOrDefault().Count = (int)model.UsersCount;
                serviceFromDB.TopTenPackageTBL.FirstOrDefault().DayCount = model.DayCount;
                serviceFromDB.TopTenPackageTBL.FirstOrDefault().HourCount = model.HourCount;
                serviceFromDB.TopTenPackageTBL.FirstOrDefault().Price = (double)model.TopTenPackagePrice;



                //serviceFromDB.Tags.Clear();
                #region add update Tags
                var servicetags = new List<ServiceTagsTBL>();
                List<string> tags = null;
                if (model.Tags != null)
                {
                    tags = model.Tags.Split(",").ToList();
                    foreach (var item in tags)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var tag = new ServiceTagsTBL()
                            {
                                IsEnglisTags = true,
                                TagName = item.Trim(),
                                Service = serviceFromDB,
                            };
                            servicetags.Add(tag);
                        }
                    }
                }



                var persinaTags = model?.PersinaTags?.Split(",").ToList();
                if (persinaTags != null)
                {
                    foreach (var item in persinaTags)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var tag = new ServiceTagsTBL()
                            {
                                IsEnglisTags = false,
                                PersianTagName = item.Trim(),
                                Service = serviceFromDB,
                            };
                            servicetags.Add(tag);
                        }
                    }
                }


                var alltags = serviceFromDB.Tags.Where(c => c.IsEnglisTags == true).ToList();
                var allPersianTags = serviceFromDB.Tags.Where(c => c.IsEnglisTags == false).ToList();
                alltags.AddRange(allPersianTags);
                _context.ServiceTags.RemoveRange(alltags);
                serviceFromDB.Tags = servicetags;
                #endregion

                //_context.ServidceTypeRequiredCertificatesTBL.RemoveRange(serviceFromDB.ServidceTypeRequiredCertificatesTBL);

                #region Update  ServidceTypeRequiredCertificatesTBL
                var idsShouldBeRemoved = new List<int>();
                idsShouldBeRemoved = serviceFromDB.ServidceTypeRequiredCertificatesTBL.Select(c => c.Id).ToList();

                if (model.RequiredFiles != null && model.RequiredFiles.Count != 0)
                {

                    foreach (var item in model.RequiredFiles)
                    {
                        if (item.Id != null)
                        {
                            //in dar ram ast
                            var currentRequireFile = serviceFromDB.ServidceTypeRequiredCertificatesTBL.Where(c => c.Id == item.Id).FirstOrDefault();

                            //In yani agar RequireFile  ferestade  ghablan bood faghat FileName,PersianFileName dare change mikone   
                            if (currentRequireFile != null)
                            {
                                idsShouldBeRemoved.Remove(currentRequireFile.Id);
                                currentRequireFile.FileName = item.FileName;
                                currentRequireFile.PersianFileName = item.PersianFileName;
                            }
                        }
                        //new requireFile added 
                        else
                        {
                            var newRequiredCertificatesTBL = new ServidceTypeRequiredCertificatesTBL()
                            {
                                FileName = item.FileName,
                                PersianFileName = item.PersianFileName,
                                ServiceId = model.Id,
                            };
                            await _context.ServidceTypeRequiredCertificatesTBL.AddAsync(newRequiredCertificatesTBL);
                        }
                    }
                }

                //delete requredFile from db ما حذف نمیکنیم فقط isdelete =true میکنیم
                foreach (var item in idsShouldBeRemoved)
                {
                    var reqFile = serviceFromDB.ServidceTypeRequiredCertificatesTBL.Where(c => c.Id == item).FirstOrDefault();
                    reqFile.Isdeleted = true;
                    //_context.ServidceTypeRequiredCertificatesTBL.Remove(reqFile);
                }

                #endregion


                ////////////if (model.RequiredFiles != null)
                ////////////{
                ////////////    var requirecertification = new List<ServidceTypeRequiredCertificatesTBL>();
                ////////////    foreach (var item in model.RequiredFiles)
                ////////////    {
                ////////////        var newFile = new ServidceTypeRequiredCertificatesTBL()
                ////////////        {
                ////////////            FileName = item.FileName,
                ////////////            PersianFileName = item.PersianFileName,
                ////////////            Isdeleted = false,
                ////////////            Service = serviceFromDB,
                ////////////        };
                ////////////        requirecertification.Add(newFile);
                ////////////    }
                ////////////    serviceFromDB.ServidceTypeRequiredCertificatesTBL = requirecertification;
                ////////////}


                var result = await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }






        /// <summary>
        /// ولیدیت کردن آبجکت  چت سرویس
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool succsseded, List<string> result)> ValidateChatService(AddChatServiceForUsersDTO model, ServiceTBLVM serviceFromDb)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();

            var IsInPackage = Enum.IsDefined(typeof(PackageType), model.PackageType);
            if (!IsInPackage)
            {
                IsValid = false;
                Errors.Add(_localizer["package Type Not Exist"].Value.ToString());
            }


            var IsInServiceType = Enum.IsDefined(typeof(ServiceType), model.ServiceType);
            if (!IsInServiceType)
            {
                IsValid = false;
                Errors.Add(_localizer["service Type Not Exist"].Value.ToString());
            }

            if (model.ServiceType == ServiceType.ChatVoice)
            {
                if (model.PackageType == PackageType.limited)
                {
                    if (model.MessageCount == null || model.MessageCount == 0)
                    {
                        IsValid = false;
                        Errors.Add(_localizer["MessageCount is Required"].Value.ToString());
                    }
                }
            }

            ///اگراز نوع وویس کال یا ویدیو کال بود دگه زمان باید بررسی شود
            if (model.ServiceType == ServiceType.VideoCal || model.ServiceType == ServiceType.VoiceCall)
            {
                if (model.PackageType == PackageType.limited)
                    //ما دیگه توی  ویدیو کال یا وویس کال   فری نداریم
                    if (model.Duration == null || model.Duration == 0)
                    {
                        if (model.FreeMessageCount == null || model.FreeMessageCount == 0)
                        {
                            IsValid = false;
                            Errors.Add(_localizer["Duration is Required"].Value.ToString());
                        }
                    }
            }


            if (model.ServiceType == ServiceType.Service || model.ServiceType == ServiceType.Course)
            {
                IsValid = false;
                Errors.Add(_localizer["Invalid ServiceType Type"].Value.ToString());
            }



            //validate serviceTypes
            //var serviceFromDb = await _context
            //.ServiceTBL
            //.Where(c => c.Id == model.ServiceId)
            //.Select(c => new { c.Id, c.AcceptedMinPriceForNative, c.AcceptedMinPriceForNonNative, c.Name })
            //.FirstOrDefaultAsync();

            if (serviceFromDb == null)
            {
                IsValid = false;
                Errors.Add(_localizer["service Not Exist"].Value.ToString());
            }

            else if (model.PriceForNativeCustomer < serviceFromDb.AcceptedMinPriceForNative)
            {
                string err = "";
                if (IsPersianLanguage())
                    err = $"قیمت برای کاربران بومی باید بیشتر از {serviceFromDb.AcceptedMinPriceForNative} باشد";
                else
                    err = string.Format($"Price For Native Customer must be more than {serviceFromDb.AcceptedMinPriceForNative}");
                IsValid = false;
                Errors.Add(err);
            }
            if (serviceFromDb != null)
                if (model.PriceForNonNativeCustomer < serviceFromDb.AcceptedMinPriceForNonNative)
                {
                    string err = "";
                    if (IsPersianLanguage())
                        err = $"قیمت برای کاربران غیر بومی باید بیشتر از {serviceFromDb.AcceptedMinPriceForNonNative} باشد";
                    else
                        err = string.Format($"Price For Non Native Customer must be more than {serviceFromDb.AcceptedMinPriceForNonNative}");
                    //err = _localizer[string.Format("{0} must be more than {1}", "Price For Non Native Customer", serviceFromDb.AcceptedMinPriceForNonNative)].Value.ToString();
                    IsValid = false;
                    Errors.Add(err);
                }


            if (model.CatId != null)
            {
                var isCatExist = await _context.CategoryTBL.AnyAsync(c => c.Id == model.CatId);
                if (!isCatExist)
                {
                    IsValid = false;
                    Errors.Add($"Invalid category");
                }
            }
            if (model.SubCatId != null)
            {
                var isCatExist = await _context.CategoryTBL.AnyAsync(c => c.Id == model.SubCatId);
                if (!isCatExist)
                {
                    IsValid = false;
                    Errors.Add($"Invalid sub category");
                }
            }


            var currentUsername = _accountService.GetCurrentUserName();
            var isUserExist = await _context.Users.AnyAsync(c => c.UserName == currentUsername);
            if (!isUserExist)
            {
                string err = "";
                if (IsPersianLanguage())
                    err = string.Format("کاربری با نام کاربری {0} یافت نشد", currentUsername);
                else
                    err = string.Format("No user with the name {0} was found", currentUsername);
                IsValid = false;
                Errors.Add(err);
            }

            return (IsValid, Errors);
        }



        /// <summary>
        /// ولیدیت کردن آبجکت  چت سرویس
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool succsseded, List<string> result)> ValidateEditChatService(EditChatServiceForUsersDTO model, BaseMyServiceTBL serviceFromDB)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();

            //var IsInPackage = Enum.IsDefined(typeof(PackageType), model.PackageType);
            //if (!IsInPackage)
            //{
            //    IsValid = false;
            //    Errors.Add(_localizer["package Type Not Exist"].Value.ToString());
            //}


            //var IsInServiceType = Enum.IsDefined(typeof(ServiceType),serviceFromDB.ServiceType  model.ServiceType);
            //if (!IsInServiceType)
            //{
            //    IsValid = false;
            //    Errors.Add(_localizer["service Type Not Exist"].Value.ToString());
            //}

            if (serviceFromDB.ServiceType == ServiceType.ChatVoice)
            {
                if (serviceFromDB.MyChatsService.PackageType == PackageType.limited)
                {
                    if (model.MessageCount == null || model.MessageCount == 0)
                    {
                        IsValid = false;
                        Errors.Add(_localizer["MessageCount is Required"].Value.ToString());
                    }
                }
            }

            ///اگراز نوع وویس کال یا ویدیو کال بود دگه زمان باید بررسی شود
            if (serviceFromDB.ServiceType == ServiceType.VideoCal || serviceFromDB.ServiceType == ServiceType.VoiceCall)
            {
                if (serviceFromDB.MyChatsService.PackageType == PackageType.limited)
                    //ما دیگه توی  ویدیو کال یا وویس کال   فری نداریم
                    if (model.Duration == null || model.Duration == 0)
                    {
                        if (model.FreeMessageCount == null || model.FreeMessageCount == 0)
                        {
                            IsValid = false;
                            Errors.Add(_localizer["Duration is Required"].Value.ToString());
                        }
                    }
            }

            if (serviceFromDB.ServiceType == ServiceType.Service || serviceFromDB.ServiceType == ServiceType.Course)
            {
                IsValid = false;
                Errors.Add(_localizer["Invalid ServiceType Type"].Value.ToString());
            }



            //validate serviceTypes
            var serviceCategory = await _context
            .ServiceTBL
            .Where(c => c.Id == serviceFromDB.ServiceId)
            .Select(c => new { c.Id, c.AcceptedMinPriceForNative, c.AcceptedMinPriceForNonNative, c.Name })
            .FirstOrDefaultAsync();

            if (serviceCategory == null)
            {
                IsValid = false;
                Errors.Add(_localizer["service Not Exist"].Value.ToString());
            }

            else if (model.PriceForNativeCustomer < serviceCategory.AcceptedMinPriceForNative)
            {
                string err = "";
                if (IsPersianLanguage())
                    err = $"قیمت برای کاربران بومی باید بیشتر از {serviceCategory.AcceptedMinPriceForNative} باشد";
                else
                    err = string.Format($"Price For Native Customer must be more than {serviceCategory.AcceptedMinPriceForNative}");
                IsValid = false;
                Errors.Add(err);
            }
            if (serviceCategory != null)
                if (model.PriceForNonNativeCustomer < serviceCategory.AcceptedMinPriceForNonNative)
                {
                    string err = "";
                    if (IsPersianLanguage())
                        err = $"قیمت برای کاربران غیر بومی باید بیشتر از {serviceCategory.AcceptedMinPriceForNonNative} باشد";
                    else
                        err = string.Format($"Price For Non Native Customer must be more than {serviceCategory.AcceptedMinPriceForNonNative}");
                    //err = _localizer[string.Format("{0} must be more than {1}", "Price For Non Native Customer", serviceFromDb.AcceptedMinPriceForNonNative)].Value.ToString();
                    IsValid = false;
                    Errors.Add(err);
                }


            //if (model.CatId != null)
            //{
            //    var isCatExist = await _context.CategoryTBL.AnyAsync(c => c.Id == model.CatId);
            //    if (!isCatExist)
            //    {
            //        IsValid = false;
            //        Errors.Add($"Invalid category");
            //    }
            //}
            //if (model.SubCatId != null)
            //{
            //    var isCatExist = await _context.CategoryTBL.AnyAsync(c => c.Id == model.SubCatId);
            //    if (!isCatExist)
            //    {
            //        IsValid = false;
            //        Errors.Add($"Invalid sub category");
            //    }
            //}


            //var currentUsername = _accountService.GetCurrentUserName();
            //var isUserExist = await _context.Users.AnyAsync(c => c.UserName == currentUsername);
            //if (!isUserExist)
            //{
            //    string err = "";
            //    if (IsPersianLanguage())
            //        err = string.Format("کاربری با نام کاربری {0} یافت نشد", currentUsername);
            //    else
            //        err = string.Format("No user with the name {0} was found", currentUsername);
            //    IsValid = false;
            //    Errors.Add(err);
            //}

            return (IsValid, Errors);
        }








        /// <summary>
        /// ولیدیت کردن آبجکت   سرویس سرویس
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool succsseded, List<string> result)> ValidateServiceService(AddServiceServiceForUsersDTO model, ServiceTBLVM serviceFromDb)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();


            var IsInServiceType = Enum.IsDefined(typeof(ServiceType), model.ServiceType);
            if (!IsInServiceType)
            {
                IsValid = false;
                Errors.Add(_localizer["service Type Not Exist"].Value.ToString());
            }
            if (model.ServiceType != ServiceType.Service)
            {
                IsValid = false;
                //Errors.Add($"Invalid ServiceType Type");
                Errors.Add(_localizer["Invalid ServiceType Type"].Value.ToString());
            }


            //validate serviceTypes
            //var serviceFromDb = await _context
            //.ServiceTBL
            //.Where(c => c.Id == model.ServiceId)
            //.Select(c => new { c.Id, c.MinPriceForService, c.Name })
            //.FirstOrDefaultAsync();

            if (serviceFromDb == null)
            {
                IsValid = false;
                Errors.Add(_localizer["service Not Exist"].Value.ToString());
            }
            else if (model.Price < serviceFromDb.MinPriceForService)
            {
                string err = "";
                if (IsPersianLanguage())
                    err = $"قیمت باید بیشتر از {serviceFromDb.MinPriceForService} باشد";
                else
                    err = string.Format($"Price must be more than {serviceFromDb.MinPriceForService}");
                IsValid = false;
                Errors.Add(err);
            }


            //validate Area and  Speciality
            if (model.AreaId == null)
            {
                string err = "";
                if (IsPersianLanguage())
                    err = $"حوضه تخصص الزامیست";
                else
                    err = $"Area Is Required";
                IsValid = false;
                Errors.Add(err);
            }
            else
            {
                var areaFromDb = await _context
                    .AreaTBL
                    .Where(c => c.Id == model.AreaId)
                    .Select(c => new
                    {
                        c.Id,
                        c.IsProfessional,
                        ids = c.Specialities.Select(c => c.Id).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (areaFromDb == null)
                {
                    string err = "";

                    if (IsPersianLanguage())
                        err = $"تخصصی با آیدی {model.AreaId} یافت نشد";
                    else
                        Errors.Add($"area with id {model.AreaId} Not Found");
                    IsValid = false;
                    Errors.Add(err);
                }

                else if (areaFromDb.IsProfessional)
                {
                    if (model.SpecialityId == null)
                    {
                        string err2 = "";
                        if (IsPersianLanguage())
                            err2 = $"تخصص الزامیست";
                        else
                            err2 = "Speciality Is Required";
                        IsValid = false;
                        Errors.Add(err2);
                    }
                    else
                    {
                        if (!areaFromDb.ids.Contains((int)model.SpecialityId))
                        {
                            string err3 = "";
                            if (IsPersianLanguage())
                                err3 = "تخصص نامعتبر";
                            else
                                err3 = "Invalid Speciality";
                            IsValid = false;
                            Errors.Add(err3);

                        }

                    }
                }
            }


            //vaidate category
            if (model.CatId != null)
            {
                var isCatExist = await _context.CategoryTBL.AnyAsync(c => c.Id == model.CatId);
                if (!isCatExist)
                {
                    string err = "";
                    if (IsPersianLanguage())
                        err = $"دسته بندی با آیدی ${model.CatId} یافت نشد";
                    else
                        err = $"category with id {model.CatId} Not Found";
                    IsValid = false;
                    Errors.Add(err);
                }
            }

            //vaidate sub category
            if (model.SubCatId != null)
            {
                var isCatExist = await _context.CategoryTBL.AnyAsync(c => c.Id == model.SubCatId);
                if (!isCatExist)
                {
                    string err = "";
                    if (IsPersianLanguage())
                        err = $" زیر دسته نا معتبر است";
                    else
                        err = $"Invalid Sub Category";
                    IsValid = false;
                    Errors.Add(err);
                }
            }
            var currentUsername = _accountService.GetCurrentUserName();
            var isUserExist = await _context.Users.AnyAsync(c => c.UserName == currentUsername);
            if (!isUserExist)
            {
                string err = "";
                if (IsPersianLanguage())
                    err = string.Format("کاربری با نام کاربری {0} یافت نشد", currentUsername);
                else
                    err = string.Format("No user with the name {0} was found", currentUsername);
                IsValid = false;
                Errors.Add(err);

            }

            return (IsValid, Errors);
        }





        /// <summary>
        /// ولیدیت کردن آبجکت   سرویس سرویس
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool succsseded, List<string> result)> ValidateCourseService(AddCourseServiceForUsersDTO model, ServiceTBLVM serviceFromDb)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();


            //validate preview
            #region validate preview
            var res = ValidatePreviewFile(model.PreviewFile);
            if (!res.succsseded)
                Errors.AddRange(res.result);
            #endregion

            //validate preview
            #region validate Tpics
            if (model.Topics == null)
            {
                string err = "";
                if (IsPersianLanguage())
                    err = $"حداقل یک سرفصل الزامیست";
                else
                    err = string.Format($"At least one topic is required");
                IsValid = false;
                Errors.Add(err);

            }
            #endregion 

            //validate serviceTypes
            #region  validate serviceTypes
            //var serviceFromDb = await _context
            //.ServiceTBL
            //.Where(c => c.Id == model.ServiceId)
            //.Select(c => new { c.Id, c.MinPriceForService, c.Name })
            //.FirstOrDefaultAsync();

            if (serviceFromDb == null)
            {
                IsValid = false;
                Errors.Add(_localizer["service Not Exist"].Value.ToString());
            }
            if (model.Price < 0)
            {
                string err = "";
                if (IsPersianLanguage())
                    err = $"قیمت نامعتبر";
                else
                    err = string.Format($"Invalid Price");
                IsValid = false;
                Errors.Add(err);
            }
            #endregion
            //vaidate category
            #region  vaidate category
            if (model.CatId != null)
            {
                var isCatExist = await _context.CategoryTBL.AnyAsync(c => c.Id == model.CatId);
                if (!isCatExist)
                {
                    string err = "";
                    if (IsPersianLanguage())
                        err = $"دسته بندی نامعتبر";
                    else
                        err = $"Invalid category";
                    IsValid = false;
                    Errors.Add(err);
                }
            }

            #endregion

            //vaidate user
            #region  vaidate user 
            var curentUsername = _accountService.GetCurrentUserName();
            var isUserExist = await _context.Users.AnyAsync(c => c.UserName == curentUsername);
            if (!isUserExist)
            {
                string err = "";
                if (IsPersianLanguage())
                    err = string.Format("کاربری با نام کاربری {0} یافت نشد", curentUsername);
                else
                    err = string.Format("No user with the name {0} was found", curentUsername);
                IsValid = false;
                Errors.Add(err);

            }
            #endregion
            return (IsValid, Errors);
        }





        private (bool succsseded, List<string> result) ValidatePreviewFile(IFormFile file)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();

            //string uniqueFileName = null;
            if (file != null)
            {
                //if (!model.Photo.IsImage())
                //{
                //    ModelState.AddModelError("", "به فرمت عکس وارد کنید");
                //    return View(model);
                //}

                //1GiG
                if (file.Length > 1000000000)
                {
                    string err = "";
                    if (IsPersianLanguage())
                        err = $"حجم فایل زیاد است";
                    else
                        err = string.Format($"File size is large");
                    IsValid = false;
                    Errors.Add(err);
                }
                if (file.Length == 0)
                {
                    string err = "";
                    if (IsPersianLanguage())
                        err = $"فایل نامعتبر";
                    else
                        err = string.Format($"Invalid file");
                    IsValid = false;
                    Errors.Add(err);
                }
            }

            return (IsValid, Errors);
        }






        //private (bool succsseded, List<string> result) ValidateTopics(List<AddCourseTopic> topics)
        //{
        //    bool IsValid = true;
        //    List<string> Errors = new List<string>();


        //    foreach (var topic in topics)
        //    {
        //        if (topic.File != null)
        //        {
        //        }
        //    }

        //    //string uniqueFileName = null;
        //    if (file != null)
        //    {
        //        //if (!model.Photo.IsImage())
        //        //{
        //        //    ModelState.AddModelError("", "به فرمت عکس وارد کنید");
        //        //    return View(model);
        //        //}

        //        //1GiG
        //        if (file.Length > 1000000000)
        //        {
        //            string err = "";
        //            if (IsPersianLanguage())
        //                err = $"حجم فایل زیاد است";
        //            else
        //                err = string.Format($"File size is large");
        //            IsValid = false;
        //            Errors.Add(err);
        //        }
        //        if (file.Length == 0)
        //        {
        //            string err = "";
        //            if (IsPersianLanguage())
        //                err = $"فایل نامعتبر";
        //            else
        //                err = string.Format($"Invalid file");
        //            IsValid = false;
        //            Errors.Add(err);

        //            //var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Upload/CoursePreview");
        //            //uniqueFileName = (Guid.NewGuid().ToString().GetImgUrlFriendly() + "_" + model.PreviewFile.FileName);
        //            //string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //            //using (var stream = new FileStream(filePath, FileMode.Create))
        //            //{
        //            //    model.PreviewFile.CopyTo(stream);
        //            //}
        //            //model.PhotoAddress = "/Upload/Slider/" + uniqueFileName;
        //        }
        //    }

        //    return (IsValid, Errors);
        //}






        public async Task<ServiceProviderResponseTypeDTO> GetAllProvideServicesForAdmin(int? page, int? perPage,
                    string searchedWord, DateTime createDate, ServiceType? serviceType, ConfirmedServiceType? confirmedServiceType)
        {

            var QueryAble = _context.BaseMyServiceTBL
                                    .AsQueryable();

            if (!string.IsNullOrEmpty(searchedWord))
            {
                QueryAble = QueryAble
                    .Where(c =>
                    c.UserName.ToLower().StartsWith(searchedWord.ToLower()) ||
                    c.UserName.ToLower().Contains(searchedWord.ToLower())
                    ||
                      c.ServiceName.ToLower().StartsWith(searchedWord.ToLower()) ||
                      c.ServiceName.ToLower().Contains(searchedWord.ToLower())
                    );
            };

            if (createDate != null)
                QueryAble = QueryAble.Where(c => c.CreateDate > createDate);

            if (serviceType != null)
                QueryAble = QueryAble.Where(c => c.ServiceType == serviceType);

            if (confirmedServiceType != null)
                QueryAble = QueryAble.Where(c => c.ConfirmedServiceType == confirmedServiceType);

            var query = QueryAble.Select(c => new ProvideServicesDTO
            {
                Id = c.Id,
                CreateDate = c.CreateDate,
                ConfirmedServiceType = c.ConfirmedServiceType,
                ServiceName = c.ServiceName,
                UserName = c.UserName,
                ServiceType = c.ServiceType,
                ServiceTypeName = c.ServiceTbl.Name,
                IsDisabledByCompany = c.IsDisabledByCompany
            });


            if (perPage == 0)
                perPage = 1;
            page = page ?? 0;
            perPage = perPage ?? 10;

            var count = QueryAble.Count();
            double len = (double)count / (double)perPage;
            var totalPages = Math.Ceiling((double)len);

            var ProvidedServices = await query
              .OrderByDescending(c => c.CreateDate)
              .Skip((int)page * (int)perPage)
             .Take((int)perPage)
             .ToListAsync();

            var data = new ServiceProviderResponseTypeDTO()
            {
                ProvidesdService = ProvidedServices,
                TotalPages = totalPages
            };
            return data;
        }





        public async Task<ServiceProviderResponseTypeDTO> GetAllProvideServicesForNotAdmin(int? page, int? perPage, string searchedWord,
            DateTime createDate, ServiceType? serviceType, ConfirmedServiceType? confirmedServiceType)
        {
            var currentRole = _accountService.GetCurrentRole();
            var roleFromDB = await _roleManager.FindByNameAsync(currentRole);
            var roleId = roleFromDB.Id;

            var QueryAble = _context.BaseMyServiceTBL
                       .Where(c => c.ServiceTbl.RoleId == roleId).AsQueryable();


            if (!string.IsNullOrEmpty(searchedWord))
            {
                QueryAble = QueryAble
                    .Where(c =>
                    c.UserName.ToLower().StartsWith(searchedWord.ToLower()) ||
                    c.UserName.ToLower().Contains(searchedWord.ToLower())
                    ||
                      c.ServiceName.ToLower().StartsWith(searchedWord.ToLower()) ||
                      c.ServiceName.ToLower().Contains(searchedWord.ToLower())
                    );
            };

            if (createDate != null)
                QueryAble = QueryAble.Where(c => c.CreateDate > createDate);

            if (serviceType != null)
                QueryAble = QueryAble.Where(c => c.ServiceType == serviceType);

            if (confirmedServiceType != null)
                QueryAble = QueryAble.Where(c => c.ConfirmedServiceType == confirmedServiceType);


            var query = QueryAble.Select(c => new ProvideServicesDTO
            {
                Id = c.Id,
                CreateDate = c.CreateDate,
                ConfirmedServiceType = c.ConfirmedServiceType,
                ServiceName = c.ServiceName,
                UserName = c.UserName,
                ServiceType = c.ServiceType,
                ServiceTypeName = c.ServiceTbl.Name,
                IsDisabledByCompany = c.IsDisabledByCompany,
            });


            if (perPage == 0)
                perPage = 1;
            page = page ?? 0;
            perPage = perPage ?? 10;

            var count = QueryAble.Count();
            double len = (double)count / (double)perPage;
            var totalPages = Math.Ceiling((double)len);

            var ProvidedServices = await query
             .OrderByDescending(c => c.CreateDate)
             .Skip((int)page * (int)perPage)
             .Take((int)perPage)
             .ToListAsync();

            var data = new ServiceProviderResponseTypeDTO()
            {
                ProvidesdService = ProvidedServices,
                TotalPages = totalPages
            };
            return data;
        }






        public string SvaeFileToHost(string path, IFormFile file)
        {
            try
            {
                if (file == null)
                    return null;
                string uniqueFileName = null;
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, path);
                uniqueFileName = (Guid.NewGuid().ToString().GetImgUrlFriendly() + "_" + file.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                //model.PhotoAddress = "/Upload/Slider/" + uniqueFileName;
                return path + uniqueFileName;
            }
            catch
            {
                return null;
            }
        }




        public bool IsPersianLanguage()
        {
            if (CultureInfo.CurrentCulture.Name == PublicHelper.persianCultureName)
                return true;
            return false;
        }




        public async Task<ResponseDTO> SearchService(SearchDTO model)
        {


            var currentusername = _accountService.GetCurrentUserName();
            //var isPersian = _commonService.IsPersianLanguage();
            //var QueryAble = _context.BaseMyServiceTBL.Include(c=>c.MyChatsService).Where(c=>c.MyChatsService)

            var QueryAble = _context.BaseMyServiceTBL
                                .Where(c => c.IsDeleted == false && c.IsDisabledByCompany == false
                                && c.ConfirmedServiceType == ConfirmedServiceType.Confirmed
                                &&
                                c.ProfileConfirmType == ProfileConfirmType.Confirmed
                                )
                                .AsNoTracking()
                                .AsQueryable();

            if (!string.IsNullOrEmpty(model.ServiceName))
            {
                QueryAble = QueryAble.Where(c =>
                c.ServiceName.ToLower().StartsWith(model.ServiceName.ToLower())
                            || c.ServiceName.ToLower().Contains(model.ServiceName.ToLower()));
            }

            if (model.ServiceCatgoryId != null)
            {
                QueryAble = QueryAble.Where(c => c.ServiceId == model.ServiceCatgoryId);
            }
            QueryAble = QueryAble.Where(c => c.ServiceTbl.IsEnabled == true);


            if (model.CategoryId != null)
            {

                //QueryAble = QueryAble.Include(c => c.CategoryTBL).Where(c => c.CatId == model.SubCateGoryId && c.CategoryTBL.IsEnabled == true);

                QueryAble = QueryAble.Where(c => c.CatId == model.SubCateGoryId && c.CategoryTBL.IsEnabled == true);

            }

            if (model.SubCateGoryId != null)
            {
                //QueryAble = QueryAble.Include(c => c.SubCategoryTBL)
                //        .Where(c => c.SubCatId == model.SubCateGoryId && c.SubCategoryTBL.IsEnabled == true);
                QueryAble = QueryAble
                        .Where(c => c.SubCatId == model.SubCateGoryId && c.SubCategoryTBL.IsEnabled == true);
            }


            if (model.ServiceType != null)
            {
                QueryAble = QueryAble.Where(c => c.ServiceType == model.ServiceType);
            }


            if (model.MinPrice != null)
            {
                if (model.ServiceType != null)
                {
                    if (model.ServiceType == ServiceType.VoiceCall || model.ServiceType == ServiceType.ChatVoice || model.ServiceType == ServiceType.VoiceCall)
                    {
                        //QueryAble = QueryAble.Include(c => c.MyChatsService)
                        //    .Where(c => c.MyChatsService.PriceForNativeCustomer >= model.MinPrice);
                        QueryAble = QueryAble
                          .Where(c => c.MyChatsService.PriceForNativeCustomer >= model.MinPrice);

                        //////////if (model.IsPriceDesc)
                        //////////    QueryAble = QueryAble.OrderByDescending(c => c.MyChatsService.PriceForNativeCustomer);
                    }
                    if (model.ServiceType == ServiceType.Service)
                    {
                        //QueryAble = QueryAble.Include(c => c.MyServicesService)
                        //       .Where(c => c.MyServicesService.Price >= model.MinPrice);
                        QueryAble = QueryAble
                               .Where(c => c.MyServicesService.Price >= model.MinPrice);


                        ////////if (model.IsPriceDesc)
                        ////////    QueryAble = QueryAble.OrderByDescending(c => c.MyServicesService.Price);
                    }
                    if (model.ServiceType == ServiceType.Course)
                    {
                        //    QueryAble = QueryAble.Include(c => c.MyCourseService)
                        //           .Where(c => c.MyCourseService.Price >= model.MinPrice);


                        QueryAble = QueryAble.Include(c => c.MyCourseService)
                               .Where(c => c.MyCourseService.Price >= model.MinPrice);

                        //////////if (model.IsPriceDesc)
                        //////////    QueryAble = QueryAble.OrderByDescending(c => c.MyCourseService.Price);
                    }
                }
            }


            if (model.MaxPrice != null)
            {
                if (model.ServiceType != null)
                {
                    if (model.ServiceType != ServiceType.Course || model.ServiceType != ServiceType.Service)
                    {
                        //QueryAble = QueryAble.Include(c => c.MyChatsService)
                        //    .Where(c => c.MyChatsService.PriceForNativeCustomer <= model.MaxPrice);
                        QueryAble = QueryAble
                           .Where(c => c.MyChatsService.PriceForNativeCustomer <= model.MaxPrice);
                        //if (model.IsPriceDesc)
                        //    QueryAble = QueryAble.OrderByDescending(c => c.MyChatsService.PriceForNativeCustomer);
                    }
                    if (model.ServiceType == ServiceType.Service)
                    {
                        //QueryAble = QueryAble.Include(c => c.MyServicesService)
                        //       .Where(c => c.MyServicesService.Price <= model.MaxPrice);

                        QueryAble = QueryAble
                              .Where(c => c.MyServicesService.Price <= model.MaxPrice);

                        //if (model.IsPriceDesc)
                        //    QueryAble = QueryAble.OrderByDescending(c => c.MyServicesService.Price);
                    }
                    if (model.ServiceType == ServiceType.Course)
                    {
                        //QueryAble = QueryAble.Include(c => c.MyCourseService)
                        //       .Where(c => c.MyCourseService.Price <= model.MaxPrice);

                        QueryAble = QueryAble
                              .Where(c => c.MyCourseService.Price <= model.MaxPrice);

                        //if (model.IsPriceDesc)
                        //   QueryAble = QueryAble.OrderByDescending(c => c.MyCourseService.Price);

                    }
                }
            }


            //c.ServiceName.ToLower().StartsWith(model.ServiceName.ToLower())


            if (model.PackageType != null)
            {
                //QueryAble = QueryAble.Include(c => c.MyChatsService)
                //    .Where(c => c.MyChatsService.PackageType == model.PackageType);

                QueryAble = QueryAble
                  .Where(c => c.MyChatsService.PackageType == model.PackageType);
            }



            var users = _context.Users.AsNoTracking().AsQueryable();
            if (model.OnlyOnlineProvider)
                users = users.Where(c => c.IsOnline == true);
            if (model.OnlyCompanyProvider)
                users = users.Where(c => c.IsCompany == true);
            //if (model.OnlyTrustedProvider)
            // some changes


            var query = (from u in users
                         join q in QueryAble
                         on u.UserName equals q.UserName
                         join s in _context.BaseMyServiceTBL
                         on u.UserName equals s.UserName
                         select new
                         {
                             u,
                             //q,
                             //q.StarCount,

                             categoryTitle = s.CategoryTBL.Title,
                             categoryPersianTitle = s.CategoryTBL.PersianTitle,
                             subCategoryTitle = s.SubCategoryTBL.Title,
                             subCategoryPersianTitle = s.SubCategoryTBL.PersianTitle,
                             ServiceTypes = s.ServiceType
                         });


            if (model.PerPage == 0)
                model.PerPage = 1;
            model.Page = model.Page ?? 0;
            model.PerPage = model.PerPage ?? 10;

            var list = await query.ToListAsync();
            var count = list.GroupBy(x => x.u.UserName).Count();
            double len = (double)count / (double)model.PerPage;
            var totalPages = Math.Ceiling((double)len);


            var items = list.GroupBy(x => x.u.UserName)
                .Select(x => new SearchResponseDTO
                {
                    UserId = x.FirstOrDefault().u.Id,
                    Username = x.Key,
                    Name = x.FirstOrDefault().u.Name,
                    LastName = x.FirstOrDefault().u.LastName,
                    Bio = x.FirstOrDefault().u.Bio,
                    ImageAddress = x.FirstOrDefault().u.ImageAddress,

                    CategoryName = x.FirstOrDefault().categoryTitle,
                    CategoryPersianName = x.FirstOrDefault().categoryPersianTitle,

                    SubCategoryName = x.FirstOrDefault().subCategoryTitle,
                    SubCategoryPersianName = x.FirstOrDefault().subCategoryPersianTitle,
                    ServiceTypes = x.Select(y => y.ServiceTypes).Distinct().ToList(),
                    //StarCount = x.FirstOrDefault().StarCount
                    StarCount = x.FirstOrDefault().u.StarCount
                    //Username = c.Key,
                })
                .Skip((int)model.Page * (int)model.PerPage)
                 .Take((int)model.PerPage)
                 .ToList();
            //.ToListAsync();

            var response = new ResponseDTO
            {
                Users = items,
                TotalPages = totalPages,
            };

            return response;



        }
    }
}
