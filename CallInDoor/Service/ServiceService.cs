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





        /// <summary>
        /// get   All services
        /// </summary>
        public Task<List<ListServiceDTO>> GetAll()
        {
            return _context.ServiceTBL.AsNoTracking().Select(c => new ListServiceDTO
            {
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
                //IsEnabled=model.Isena
            };

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

                var result = await _context.SaveChangesAsync();
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
