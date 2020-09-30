using Domain;
using Domain.DTO.Account;
using Domain.DTO.Service;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.ServiceType;
using System;
using System.Collections.Generic;
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
        /// get All services
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
                Errors.Add($"package Type Not Exist");
            }
            var IsInServiceType = Enum.IsDefined(typeof(ServiceType), model.ServiceType);
            if (!IsInServiceType)
            {
                IsValid = false;
                Errors.Add($"service Type Not Exist");
            }
            if (model.ServiceType == ServiceType.Service || model.ServiceType == ServiceType.Course)
            {
                IsValid = false;
                Errors.Add($"Invalid ServiceType Type");
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
                Errors.Add($"Please Select Category");
            }



            var IsServiceExist = _context.ServiceTBL.Any(c => c.Id == model.ServiceId);
            if (!IsServiceExist)
            {
                IsValid = false;
                Errors.Add($"service Not Exist");
            }

            var isUserExist = await _context.Users.AnyAsync(c => c.UserName == model.UserName);
            if (!isUserExist)
            {
                IsValid = false;
                Errors.Add($"No user with the name {model.UserName} was found");
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



            /////////Area   
            //////Speciality



            var IsInServiceType = Enum.IsDefined(typeof(ServiceType), model.ServiceType);
            if (!IsInServiceType)
            {
                IsValid = false;
                Errors.Add($"service Type Not Exist");
            }
            if (model.ServiceType != ServiceType.Service)
            {
                IsValid = false;
                Errors.Add($"Invalid ServiceType Type");
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
                Errors.Add($"service Not Exist");
            }
            else if (model.Price < serviceFromDb.MinPriceForService)
            {
                var err = _localizer[string.Format("{0} must be more than {1}", "price", serviceFromDb.MinPriceForService)].Value.ToString();

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
                        c.ServiceId,
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
                    if (serviceFromDb != null)
                    {
                        if (cats.ServiceId != serviceFromDb.Id)
                        {
                            IsValid = false;
                            Errors.Add($"Category {cats.Id} is not specific to {serviceFromDb.Id} ");
                        }
                    }

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
                Errors.Add($"Please Select Category");
            }




            var isUserExist = await _context.Users.AnyAsync(c => c.UserName == model.UserName);
            if (!isUserExist)
            {
                IsValid = false;
                Errors.Add($"No user with the name {model.UserName} was found");
            }

            return (IsValid, Errors);
        }


    }
}
