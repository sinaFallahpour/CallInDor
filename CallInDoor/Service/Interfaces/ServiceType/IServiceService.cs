using Domain.DTO.Account;
using Domain.DTO.Service;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Service.Interfaces.ServiceType
{
    public interface IServiceService
    {
        ServiceTBL GetById(int Id);
        Task<ServiceTBL> GetByIdWithJoin(int Id);
        Task<List<ListServiceDTO>> GetAllActiveService();
        Task<bool> Create(CreateServiceDTO model);
        //Task<bool> Update(ServiceTBL model);
        Task<bool> Update(ServiceTBL serviceFromDB, CreateServiceDTO model);
        //Task<(int statusCode, string message, bool result)> Update(int Id);

        Task<(bool succsseded, List<string> result)> ValidateChatService(AddChatServiceForUsersDTO model, ServiceTBLVM serviceFromDb);
        Task<(bool succsseded, List<string> result)> ValidateEditChatService(EditChatServiceForUsersDTO model, BaseMyServiceTBL serviceFromDB);

        Task<(bool succsseded, List<string> result)> ValidateServiceService(AddServiceServiceForUsersDTO model, ServiceTBLVM serviceFromDb);

        Task<(bool succsseded, List<string> result)> ValidateCourseService(AddCourseServiceForUsersDTO model, ServiceTBLVM serviceFromDb);

        Task<ResponseDTO> SearchService(SearchDTO model);

        #region  get al serve Provide Foer admin and none admin 
        Task<ServiceProviderResponseTypeDTO> GetAllProvideServicesForNotAdmin(int? page, int? perPage,
                                        string searchedWord, DateTime createDate, Domain.Enums.ServiceType? serviceType, string serviceTypes, ConfirmedServiceType? confirmedServiceType);


        //Task<ServiceProviderResponseTypeDTO> GetAllProvideServicesForAdmin(int? page, int? perPage,
        //          string searchedWord, DateTime createDate, Domain.Enums.ServiceType? serviceType, ConfirmedServiceType? confirmedServiceType);


        Task<ServiceProviderResponseTypeDTO> GetAllProvideServicesForAdmin(int? page, int? perPage,
                    string searchedWord, DateTime createDate, Domain.Enums.ServiceType? serviceType, string serviceTypes, ConfirmedServiceType? confirmedServiceType);









        #endregion


        //آدرس فایل
        string SvaeFileToHost(string path, IFormFile file);
        Task<string> SaveFileToHost(string path, string lastPath, IFormFile file);

    }
}
