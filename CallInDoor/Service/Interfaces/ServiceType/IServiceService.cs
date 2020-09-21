using Domain.DTO.Account;
using Domain.DTO.Service;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.ServiceType
{
    public interface IServiceService
    {
        ServiceTBL GetById(int Id);
        Task<List<ListServiceDTO>> GetAllActive();
        Task<bool> Create(CreateServiceDTO model);
        //Task<bool> Update(ServiceTBL model);
        Task<bool> Update(ServiceTBL serviceFromDB, CreateServiceDTO model);
        Task<(int statusCode, string message, bool result)> Update(int Id);



    }
}
