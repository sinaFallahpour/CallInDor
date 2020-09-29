using AutoMapper;
using Domain;
using Domain.DTO.Account;
using Domain.Entities;
using Domain.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Service.Interfaces.JwtManager;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        //private readonly ICommonService _CommonService;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtManager _jwtGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private IStringLocalizer<ShareResource> _localizerShared;
        private IStringLocalizer<AccountService> _localizerAccount;
        private readonly IMapper _mapper;


        public AccountService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            DataContext context,
               IJwtManager jwtGenerator,
               IHttpContextAccessor httpContextAccessor,
                IStringLocalizer<AccountService> localizerAccount,
                //ICommonService commonService,
                IMapper mapper
               )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _httpContextAccessor = httpContextAccessor;
            _localizerAccount = localizerAccount;
            //_CommonService = commonService;
            _mapper = mapper;
        }





        /// <summary>
        ///  گرفتن نام کاربری کاربر فعلی
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUserName()
        {
            var currentUserName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            return currentUserName;
        }





        /// check Token paload(serialNUmber) Is valid
        public async Task<bool> CheckTokenIsValid()
        {
            var ser = _httpContextAccessor.HttpContext.User.Claims;
            //var currentUsername = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var currentSerialNumber = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == PublicHelper.SerialNumberClaim)?.Value;
            var currentUserName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var IsExist = await _context.Users
                    .AnyAsync(x => x.SerialNumber == currentSerialNumber && x.UserName == currentUserName);
            //.Where(x => x.SerialNumber == currentSerialNumber && x.UserName == currentUserName)
            //.AnyAsync();
            //.Select(c => c.UserName)
            //.FirstOrDefaultAsync();
            if (!IsExist)
                return false;
            return true;

            //if (string.IsNullOrEmpty(username))
            //    return false;
            //return true;
        }




        public async Task<AppUser> CheckIsCurrentUserName(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return null;

            var currentSerialNumber = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == PublicHelper.SerialNumberClaim)?.Value;

            var user = await _context.Users.Where(x => x.SerialNumber == currentSerialNumber && x.Id == Id)
                .Include(c => c.UsersDegrees)
                .FirstOrDefaultAsync();

            return user;
        }






        //public async Task<string> CheckTokenIsValid2()
        //{
        //    //var currentUsername = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        //    var currentSerialNumber = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == PublicHelper.SerialNumberClaim)?.Value;

        //    var username = await _context.Users.Where(x => x.SerialNumber == currentSerialNumber)
        //        .Select(c => c.UserName)
        //        .FirstOrDefaultAsync();
        //    return username;
        //}






        public async Task<AppUser> FindUserByPhonenumber(string PhoneNumber)
        {
            var user = await _context.Users.Where(x => x.PhoneNumber == PhoneNumber).FirstOrDefaultAsync();
            return user;
        }






        public async Task<(int status, List<string> erros)> CheckVeyficatioCode(VerifyDTO model)
        {
            var phoneNumber = model.CountryCode.ToString().Trim() + model.PhoneNumber.Trim();
            var errors = new List<string>();

            var User = await _context.Users.Where(c => c.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
            if (User == null)
            {
                //_localizer[string.Format("username  {0} Already Registered", model.Username)].Value.ToString()
                errors.Add(_localizerAccount["User not Found With PhoneNumber"].Value.ToString());
                //errors.Add(_localizerAccount[string.Format("User notFound With PhoneNumber {0}", model.PhoneNumber)].Value.ToString());
                return (0, errors);
            }
            if (User.PhoneNumberConfirmed == true)
            {
                errors.Add(_localizerAccount["this account Is Confirmed"].Value.ToString());
                return (0, errors);
            }
            if (User.verificationCode != model.VerifyCode)
            {
                errors.Add(_localizerAccount["InValid verification Code"].Value.ToString());
                return (0, errors);
            }

            if (User.verificationCodeExpireTime < DateTime.UtcNow)
            {
                errors.Add(_localizerAccount["timeOut"].Value.ToString());
                return (0, errors);
            }
            return (1, null);
        }






        public async Task<SignInResult> CheckPasswordAsync(AppUser user, string Password)
        {
            return await _signInManager
                        .CheckPasswordSignInAsync(user, Password, false);
        }




        public async Task<string> GeneratePasswordResetToken(AppUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }




        public async Task<ProfileGetDTO> ProfileGet(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;
            var user = await _context
                        .Users.Where(c => c.UserName == username)
                        .Include(c => c.UsersDegrees)
                        .FirstOrDefaultAsync();
            var Profile = _mapper.Map<AppUser, ProfileGetDTO>(user);
            return Profile;
        }





    }
}
