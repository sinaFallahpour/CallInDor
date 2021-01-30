using Domain.DTO.Account;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Account
{
    public interface IAccountService
    {
        string GetCurrentUserName();
        public Task<AppUser> GetUserByUserName(string userName);
        string GetcurrentSerialNumber();
        string GetCurrentRole();
        Task<AppUser> FindUserByPhonenumber(string PhoneNumber);
        Task<(int status, List<string> erros)> CheckVeyficatioCode(VerifyDTO model);
        Task<SignInResult> CheckPasswordAsync(AppUser User, string Password);
        //generate password resete token
        Task<string> GeneratePasswordResetToken(AppUser user);
        /// check Token paload(serialNUmber) Is valid
        Task<bool> CheckTokenIsValid();
        Task<bool> CheckHasPermission(List<string> requiredPermissions);
        Task<(bool status, string username)> CheckTokenIsValidForAdminRole();
        Task<ResponseType> GetListOfUserForVerification(int? page, int? perPage, string searchedWord, ProfileConfirmType? ProfileConfirmType);
        Task<ResponseType> GetListOfUserForVerificationForAdmin(int? page, int? perPage, string searchedWord, ProfileConfirmType? ProfileConfirmType);
        Task<AppUser> CheckIsCurrentUserName(string Id);
        //Task<string> CheckTokenIsValid2();
        Task<ProfileGetDTO> ProfileGet();
        Task<ProfileFirmGetDTO> ProfileFirmGet();
        Task<bool> UpdateProfile(AppUser userFromDB, List<ProfileCertificateTBL> certificationFromDB, UpdateProfileDTO model);
        Task<bool> UpdateFirmProfile(AppUser userFromDB, UpdateFirmProfileDTO model);


        Task<string> SaveFileToHost(string path, string lastPath, IFormFile file);
        Task<(bool succsseded, List<string> result)> ValidateUpdateProfile(UpdateProfileDTO model);
        (bool succsseded, List<string> result) ValidateUpdateFirmProfile(UpdateFirmProfileDTO model);

        bool IsNative(AppUser clinet, AppUser provider);
        Task<bool> IsNative(string clinetUserName, string providerUserName);
        bool IsPersianLanguage();
        
    }
}
