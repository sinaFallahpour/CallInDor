using Domain;
using Domain.DTO.Account;
using Domain.DTO.Service;
using Domain.Entities;
using Domain.Enums;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.ServiceType;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceService : IServiceService
    {
        private readonly DataContext _context;
        private IStringLocalizer<ServiceService> _localizer;

        public ServiceService(
            DataContext context,
            IStringLocalizer<ServiceService> localizer
               )
        {
            _context = context;
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
            return await _context.ServiceTBL.Where(c => c.Id == Id).Include(c => c.Tags).FirstOrDefaultAsync();
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
                MinPriceForService = model.MinPriceForService,
                MinSessionTime = model.MinSessionTime,
                AcceptedMinPriceForNative = model.AcceptedMinPriceForNative,
                AcceptedMinPriceForNonNative = model.AcceptedMinPriceForNonNative
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

            try
            {
                await _context.ServiceTBL.AddAsync(serviceType);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
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
                serviceFromDB.Color = model.Color;
                serviceFromDB.MinPriceForService = model.MinPriceForService;
                serviceFromDB.MinSessionTime = model.MinSessionTime;
                serviceFromDB.AcceptedMinPriceForNative = model.AcceptedMinPriceForNative;
                serviceFromDB.AcceptedMinPriceForNonNative = model.AcceptedMinPriceForNonNative;
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
                var cats = await _context.CategoryTBL
                    .AsNoTracking()
                    .Where(c => c.Id == model.CatId)
                    .Select(c => new
                    {
                        c.Id,
                        SubCatIds = c.Children.Select(r => r.Id).ToList()
                    }).FirstOrDefaultAsync();

                if (cats == null)
                {
                    //return (false, fileName)
                    IsValid = false;
                    Errors.Add($"category with id {model.CatId} Not Found");
                }
                else
                {
                    if (model.SubCatId != null)
                    {
                        var ds = cats.SubCatIds?.Contains((int)model.SubCatId);
                        if (ds == null || (ds != null && !(bool)ds))
                        {
                            IsValid = false;
                            Errors.Add($"Sub Category with id {model.SubCatId} Not Found");
                        }
                    }
                }
            }

            if (model.CatId == null)
            {
                IsValid = false;
                Errors.Add(_localizer["Category Is Required"].Value.ToString());
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
                        err = $"تخصصی با آیدی ${model.AreaId} یافت نشد";
                    else
                        Errors.Add($"area with id {model.AreaId} Not Found");
                    IsValid = false;
                    Errors.Add(err);
                }

                if (areaFromDb.IsProfessional)
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


            if (model.CatId != null)
            {
                var cats = await _context.CategoryTBL
                    .AsNoTracking()
                    .Where(c => c.Id == model.CatId)
                    .Select(c => new
                    {
                        c.Id,
                        c.ServiceId,
                        SubCatIds = c.Children.Select(r => r.Id).ToList()
                    }).FirstOrDefaultAsync();

                if (cats == null)
                {

                    string err = "";
                    if (IsPersianLanguage())
                        err = $"دسته بندی با آیدی ${model.CatId} یافت نشد";
                    else
                        err = $"category with id {model.CatId} Not Found";
                    IsValid = false;
                    Errors.Add(err);



                }
                else
                {
                    if (serviceFromDb != null)
                    {
                        if (cats.ServiceId != serviceFromDb.Id)
                        {
                            string err = "";
                            if (IsPersianLanguage())
                                err = $"دسته بندی نا معتبر است";
                            else
                                err = $"Invalid Category";
                            IsValid = false;
                            Errors.Add(err);
                        }
                    }

                    if (model.SubCatId != null)
                    {
                        var ds = cats.SubCatIds?.Contains((int)model.SubCatId);
                        if (ds == null || (ds != null && !(bool)ds))
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
                }
            }

            if (model.CatId == null)
            {
                IsValid = false;
                Errors.Add(_localizer["Category Is Required"].Value.ToString());
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


                //IsValid = false;
                //Errors.Add($"No user with the name {model.UserName} was found");
            }

            return (IsValid, Errors);
        }



        public bool IsPersianLanguage()
        {
            if (CultureInfo.CurrentCulture.Name == PublicHelper.persianCultureName)
                return true;
            return false;
        }



    }
}
