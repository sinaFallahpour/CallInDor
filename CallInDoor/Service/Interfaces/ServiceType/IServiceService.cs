﻿using Domain.DTO.Account;
using Domain.DTO.Service;
using Domain.Entities;
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

        Task<(bool succsseded, List<string> result)> ValidateChatService(AddChatServiceForUsersDTO model);
        Task<(bool succsseded, List<string> result)> ValidateServiceService(AddServiceServiceForUsersDTO model);

        Task<(bool succsseded, List<string> result)> ValidateCourseService(AddCourseServiceForUsersDTO model);

        //آدرس فایل
        string SvaeFileToHost(string path, IFormFile file);

    }
}
