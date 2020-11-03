using Domain;
using Domain.DTO.Account;
using Domain.DTO.Service;
using Domain.Entities;
using Domain.Enums;
using Domain.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
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
        private readonly DataContext _context;
        private IStringLocalizer<ServiceService> _localizer;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ServiceService(
            DataContext context,
            IStringLocalizer<ServiceService> localizer,
            IHostingEnvironment hostingEnvironment
               )
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _localizer = localizer;
        }





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
                .Include(c => c.Tags)
                .Include(c => c.ServidceTypeRequiredCertificatesTBL)
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
                MinSessionTime = model.MinSessionTime,
                AcceptedMinPriceForNative = model.AcceptedMinPriceForNative,
                AcceptedMinPriceForNonNative = model.AcceptedMinPriceForNonNative,
                SitePercent = model.SitePercent
            };

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

            //serviceType.Tags = servicetags;


            //var servicePersiantags = new List<ServiceTags>();
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
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
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
                serviceFromDB.SitePercent = model.SitePercent;
                serviceFromDB.Color = model.Color;
                serviceFromDB.MinPriceForService = model.MinPriceForService;
                serviceFromDB.MinSessionTime = model.MinSessionTime;
                serviceFromDB.AcceptedMinPriceForNative = model.AcceptedMinPriceForNative;
                serviceFromDB.AcceptedMinPriceForNonNative = model.AcceptedMinPriceForNonNative;
                serviceFromDB.RoleId = model.RoleId;

                //serviceFromDB.Tags.Clear();

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


                _context.ServidceTypeRequiredCertificatesTBL.RemoveRange(serviceFromDB.ServidceTypeRequiredCertificatesTBL);

                if (model.RequiredFiles != null)
                {
                    var requirecertification = new List<ServidceTypeRequiredCertificatesTBL>();
                    foreach (var item in model.RequiredFiles)
                    {
                        var newFile = new ServidceTypeRequiredCertificatesTBL()
                        {
                            FileName = item.FileName,
                            PersianFileName = item.PersianFileName,
                            Isdeleted = false,
                            Service = serviceFromDB,
                        };
                        requirecertification.Add(newFile);
                    }
                    serviceFromDB.ServidceTypeRequiredCertificatesTBL = requirecertification;
                }


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
        public async Task<(bool succsseded, List<string> result)> ValidateChatService(AddChatServiceForUsersDTO model)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();

            var IsInPackage = Enum.IsDefined(typeof(PackageType), model.PackageType);
            if (!IsInPackage)
            {
                IsValid = false;
                Errors.Add(_localizer["package Type Not Exist"].Value.ToString());
            }
            if (model.PackageType == PackageType.Free)
            {
                if (model.FreeMessageCount == null || model.FreeMessageCount == 0)
                {
                    IsValid = false;
                    Errors.Add(_localizer["FreeMessageCount is Required"].Value.ToString());
                }
            }
            else
            {
                if (model.Duration == null || model.Duration == 0)
                {
                    IsValid = false;
                    Errors.Add(_localizer["Duration is Required"].Value.ToString());
                }
            }
            var IsInServiceType = Enum.IsDefined(typeof(ServiceType), model.ServiceType);
            if (!IsInServiceType)
            {
                IsValid = false;
                Errors.Add(_localizer["service Type Not Exist"].Value.ToString());
            }
            if (model.ServiceType == ServiceType.Service || model.ServiceType == ServiceType.Course)
            {
                IsValid = false;
                Errors.Add(_localizer["Invalid ServiceType Type"].Value.ToString());
            }



            //validate serviceTypes
            var serviceFromDb = await _context
            .ServiceTBL
            .Where(c => c.Id == model.ServiceId)
            .Select(c => new { c.Id, c.AcceptedMinPriceForNative, c.AcceptedMinPriceForNonNative, c.Name })
            .FirstOrDefaultAsync();

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
                        err = string.Format($"Price For Native Customer must be more than {serviceFromDb.AcceptedMinPriceForNonNative}");
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
                var isCatExist = await _context.CategoryTBL.AnyAsync(c => c.Id == model.SubCatId && c.IsSubCategory);
                if (!isCatExist)
                {
                    IsValid = false;
                    Errors.Add($"Invalid sub category");
                }
            }
            //if (model.CatId == null)
            //{
            //    IsValid = false;
            //    Errors.Add(_localizer["Category Is Required"].Value.ToString());
            //}


            var isUserExist = await _context.Users.AnyAsync(c => c.UserName == model.UserName);
            if (!isUserExist)
            {
                string err = "";
                if (IsPersianLanguage())
                    err = string.Format("کاربری با نام کاربری {0} یافت نشد", model.UserName);
                else
                    err = string.Format("No user with the name {0} was found", model.UserName);
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
        public async Task<(bool succsseded, List<string> result)> ValidateServiceService(AddServiceServiceForUsersDTO model)
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
            var serviceFromDb = await _context
            .ServiceTBL
            .Where(c => c.Id == model.ServiceId)
            .Select(c => new { c.Id, c.MinPriceForService, c.Name })
            .FirstOrDefaultAsync();

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
                                err3 = "تخصص نا معتبر";
                            else
                                err3 = "Invalid Speciality";
                            IsValid = false;
                            Errors.Add(err3);

                        }

                    }//var specialityFromDb = await _context.SpecialityTBL.whe
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
                var isCatExist = await _context.CategoryTBL.AnyAsync(c => c.Id == model.SubCatId && c.IsSubCategory);
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

            var isUserExist = await _context.Users.AnyAsync(c => c.UserName == model.UserName);
            if (!isUserExist)
            {
                string err = "";
                if (IsPersianLanguage())
                    err = string.Format("کاربری با نام کاربری {0} یافت نشد", model.UserName);
                else
                    err = string.Format("No user with the name {0} was found", model.UserName);
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
        public async Task<(bool succsseded, List<string> result)> ValidateCourseService(AddCourseServiceForUsersDTO model)
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
            var serviceFromDb = await _context
            .ServiceTBL
            .Where(c => c.Id == model.ServiceId)
            .Select(c => new { c.Id, c.MinPriceForService, c.Name })
            .FirstOrDefaultAsync();

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
            var isUserExist = await _context.Users.AnyAsync(c => c.UserName == model.UserName);
            if (!isUserExist)
            {
                string err = "";
                if (IsPersianLanguage())
                    err = string.Format("کاربری با نام کاربری {0} یافت نشد", model.UserName);
                else
                    err = string.Format("No user with the name {0} was found", model.UserName);
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



    }
}
