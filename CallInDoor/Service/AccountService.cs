﻿using AutoMapper;
using Domain;
using Domain.DTO.Account;
using Domain.Entities;
using Domain.Utilities;
using Microsoft.AspNetCore.Hosting;
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
using System.IO;
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
        private readonly IHostingEnvironment _hostingEnvironment;


        //private IStringLocalizer<ShareResource> _localizerShared;
        private IStringLocalizer<AccountService> _localizerAccount;
        private readonly IMapper _mapper;


        public AccountService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            DataContext context,
               IJwtManager jwtGenerator,
               IHostingEnvironment hostingEnvironment,
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
            _hostingEnvironment = hostingEnvironment;
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


        /// <summary>
        ///  گرفتن نام کاربری کاربر فعلی
        /// </summary>
        /// <returns></returns>
        public string GetcurrentSerialNumber()
        {
            var currentSerialNumber = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == PublicHelper.SerialNumberClaim)?.Value;
            return currentSerialNumber;
        }



        /// check Token paload(serialNUmber) Is valid
        public async Task<bool> CheckTokenIsValid()
        {
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




        /// check Token paload(serialNUmber) Is valid
        public async Task<bool> CheckHasPermission(List<string> requiredPermission)
        {
            //var requiredPermission = new List<string>() {
            // PublicPermissions.User.GetAll,
            // PublicPermissions.User.userEdit
            //};


            var currentRole = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            var currentUserName = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var currentPermission = _httpContextAccessor.HttpContext?.User?.Claims?.Where(x => x.Type == PublicPermissions.Permission).ToList();

            var query1 = (from u in _context.Users.Where(c => c.UserName == currentUserName)
                          join ur in _context.UserRoles
                          on u.Id equals ur.UserId
                          join r in _context.Roles
                         on ur.RoleId equals r.Id
                          join rp in _context.Role_Permission
                          on r.Id equals rp.RoleId
                          join per in _context.Permissions
                          on rp.PermissionId equals per.Id
                          select new
                          {
                              //rp.PermissionId,
                              per.ActionName
                          }).AsQueryable();


            var res = await query1.ToListAsync();
            var HavePermission = false;
            foreach (var item in res)
            {
                if (HavePermission)
                    break;
                foreach (var perm in requiredPermission)
                {
                    if (item.ActionName.ToLower() == perm.ToLower())
                    {
                        HavePermission = true;
                        break;
                    }
                }
            }

            return HavePermission;

        }




        /// check Token paload(serialNUmber) Is valid
        public async Task<(bool status, string username)> CheckTokenIsValidForAdminRole()
        {
            //var currentUsername = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var currentSerialNumber = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == PublicHelper.SerialNumberClaim)?.Value;
            var currentUserName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var currentUserRole = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.UserName == currentUserName
                    && x.SerialNumber == currentSerialNumber);

            if (user == null)
                return (false, null);

            var role = await _userManager.IsInRoleAsync(user, PublicHelper.ADMINROLE);
            if (!role)
                return (false, user.UserName);
            return (true, user.UserName);
        }



        public async Task<AppUser> CheckIsCurrentUserName(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return null;

            var currentSerialNumber = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == PublicHelper.SerialNumberClaim)?.Value;

            var user = await _context.Users.Where(x => x.SerialNumber == currentSerialNumber && x.Id == Id)
                ////////////.Include(c => c.UsersFields)
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



        /// <summary>
        /// get current user username
        /// </summary>
        /// <returns></returns>
        public async Task<ProfileGetDTO> ProfileGet()
        {
            var currentusername = GetCurrentUserName();
            if (string.IsNullOrEmpty(currentusername))
                return null;

            var servicetypesids = await _context.BaseMyServiceTBL.Where(c => c.UserName == currentusername && c.IsDeleted == false && c.ServiceId != null)
                  .Select(c => c.ServiceId).Distinct().ToListAsync();

            ////////var servicetypesid = new list<int> { 1, 2, 3, 4 };

            var profilecertificates = new List<ProfileCertificateDTO>();

            foreach (var id in servicetypesids)
            {
                var result = await _context.ServiceTBL.Where(c => c.Id == id).FirstOrDefaultAsync();
                profilecertificates.Add(new ProfileCertificateDTO()
                {
                    ServiceId = result?.Id,
                    ServiceName = result?.Name,
                    ServicePersianName = result?.PersianName,
                  RequiredCertificate = result?.ServidceTypeRequiredCertificatesTBL.Select(c => new RequiredCertificate { FileName = c.FileName, PersianFileName = c.PersianFileName }).ToList()
                });
            }



            //////var servicetypesIds = (from bs in _context.BaseMyServiceTBL.Where(c => c.UserName == currentusername && c.IsDeleted == false && c.ServiceId != null)
            //////                       join s in _context.ServiceTBL
            //////                       on bs.ServiceId equals s.Id
            //////                       select bs.ServiceId).Distinct().ToList();


            var userFromDB = await _context.Users.Where(c => c.UserName == currentusername)
                .Select(c => new ProfileGetDTO
                {
                    Username = c.UserName,
                    Name = c.Name,
                    Email = c.Email,
                    LastName = c.LastName,
                    Bio = c.Bio,
                    ImageAddress = c.ImageAddress,
                    VideoAddress = c.VideoAddress,
                    ProfileCertificate = profilecertificates,
                    Fields = c.Fields.Select(r => new FiledsDTO { DegreeType = r.DegreeType, Title = r.Title }).ToList()
                }).FirstOrDefaultAsync();

            return userFromDB;
        }







        /// <summary>
        /// ولیدیت کردن آبجکت آپدیت پروفابل
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public (bool succsseded, List<string> result) ValidateUpdateProfile(UpdateProfileDTO model)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();

            if (model.Image != null)
            {
                //string uniqueFileName = null;
                if (!model.Image.IsImage())
                {
                    IsValid = false;
                    Errors.Add(_localizerAccount["InValidImageFormat"].Value.ToString());
                }
                if (model.Image.Length > 5000000)
                {
                    IsValid = false;
                    Errors.Add(_localizerAccount["FileIsTooLarge"].Value.ToString());
                }
                //if (model.File.Length > 0)
                //{

                //    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Upload/User");
                //    uniqueFileName = (Guid.NewGuid().ToString().GetImgUrlFriendly() + "_" + model.File.FileName);
                //    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                //    using (var stream = new FileStream(filePath, FileMode.Create))
                //    {
                //        await model.File.CopyToAsync(stream);
                //    }

                //    //model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

                //    //Delete LastImage Image
                //    if (!string.IsNullOrEmpty(model.ImageAddress))
                //    {
                //        var LastImagePath = model.ImageAddress.Substring(1);
                //        LastImagePath = Path.Combine(_hostingEnvironment.WebRootPath, LastImagePath);
                //        if (System.IO.File.Exists(LastImagePath))
                //        {
                //            System.IO.File.Delete(LastImagePath);
                //        }
                //    }
                //    //update Newe Pic Address To database
                //    model.ImageAddress = "/Upload/User/" + uniqueFileName;
                //}
            }



            return (IsValid, Errors);
        }





    }
}
