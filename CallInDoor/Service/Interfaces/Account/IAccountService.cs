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
        Task<AppUser> FindUserByPhonenumber(string PhoneNumber);


        Task<(int status, List<string> erros)> CheckVeyficatioCode(VerifyDTO model);



        Task<SignInResult> CheckPasswordAsync(AppUser User, string Password);



        //generate password resete token
        Task<string> GeneratePasswordResetToken(AppUser user);


    }
}
