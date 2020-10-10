using Domain.DTO.Account;
using Domain.Entities;
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
        string GetcurrentSerialNumber();

        Task<AppUser> FindUserByPhonenumber(string PhoneNumber);


        Task<(int status, List<string> erros)> CheckVeyficatioCode(VerifyDTO model);



        Task<SignInResult> CheckPasswordAsync(AppUser User, string Password);



        //generate password resete token
        Task<string> GeneratePasswordResetToken(AppUser user);





        /// check Token paload(serialNUmber) Is valid
        Task<bool> CheckTokenIsValid();

        Task<(bool status, string username)> CheckTokenIsValidForAdminRole();

        Task<AppUser> CheckIsCurrentUserName(string Id);

        //Task<string> CheckTokenIsValid2();

        Task<ProfileGetDTO> ProfileGet();

        Task<(bool succsseded, List<string> result)> ValidateUpdateProfile(UpdateProfileDTO model);


    }
}
