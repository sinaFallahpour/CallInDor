using Domain;
using Domain.DTO.Account;
using Domain.DTO.Service;
using Domain.Entities;
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

        public ServiceService(
            DataContext context
               )
        {
            _context = context;
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
        public Task<List<ListServiceDTO>> GetAllActive()
        {
            return _context.ServiceTBL.Where(c => c.IsEnabled).AsNoTracking().Select(c => new ListServiceDTO
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

            var servicetags = new List<ServiceTags>();
            var tags = model?.Tags?.Split(",").ToList();


            foreach (var item in tags)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var tag = new ServiceTags()
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
                        var tag = new ServiceTags()
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

                var servicetags = new List<ServiceTags>();
                List<string> tags = null;
                if (model.Tags != null)
                {
                    tags = model.Tags.Split(",").ToList();


                    foreach (var item in tags)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var tag = new ServiceTags()
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
                            var tag = new ServiceTags()
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
        public async Task<(int statusCode, string message, bool result)> Update(int Id)
        {
            try
            {
                var service = await _context.ServiceTBL.Where(c => c.Id == Id).FirstOrDefaultAsync();
                if (service == null) return (404, "Service Not Found", false);

                service.PersianName = service.PersianName;
                service.Name = service.Name;
                service.IsEnabled = service.IsEnabled;
                service.Color = service.Color;
                service.MinPriceForService = service.MinPriceForService;
                service.MinSessionTime = service.MinSessionTime;

                var result = await _context.SaveChangesAsync();
                return (200, "Successful registration", true);
            }
            catch
            {
                return (500, "Fail registration", false);
            }

        }






    }
}
