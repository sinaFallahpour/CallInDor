using AutoMapper;
using Domain;
using Domain.DTO.Account;
using Domain.Entities;
using Domain.Enums;
using Domain.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.JwtManager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service
{
    public class AccountService : IAccountService
    {
        #region ctor

        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

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
            RoleManager<AppRole> roleManager,
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
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _localizerAccount = localizerAccount;
            //_CommonService = commonService;
            _mapper = mapper;
        }


        #endregion




        public Task<AppUser> GetUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;
            return _userManager.FindByNameAsync(userName);
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



        /// <summary>
        ///  گرفتن نقش فعلی
        /// </summary>
        /// <returns></returns>
        public string GetCurrentRole()
        {
            var currentRole = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            return currentRole;
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



        public async Task<ResponseType> GetListOfUserForVerification(int? page, int? perPage,
                string searchedWord, ProfileConfirmType? ProfileConfirmType)
        {
            var currentRole = GetCurrentRole();
            var currentUsername = GetCurrentUserName();

            var roleFromDB = await _roleManager.FindByNameAsync(currentRole);
            var QueryAble = (from us in _context.Users
                             join bm in _context.BaseMyServiceTBL
                             on us.UserName equals bm.UserName
                             join bs in _context.ServiceTBL.Where(c => c.RoleId == roleFromDB.Id)
                            on bm.ServiceId equals bs.Id
                             select new GetListOfUsersForVerification()
                             {
                                 UserName = us.UserName,
                                 Name = us.Name,
                                 CreateDate = us.CreateDate,
                                 LastName = us.LastName,
                                 IsCompany = us.IsCompany,
                                 ProfileConfirmType = us.ProfileConfirmType,
                                 IsEditableProfile = us.IsEditableProfile,
                                 //PhoneNumber = us.PhoneNumber,
                                 PhoneNumberConfirmed = us.PhoneNumberConfirmed,
                                 CountryCode = us.CountryCode,
                                 ImageAddress = us.ImageAddress,
                                 isLockOut = (us.LockoutEnd != null && us.LockoutEnd > DateTime.Now),

                             }).AsNoTracking().AsQueryable();


            if (!string.IsNullOrEmpty(searchedWord))
            {
                QueryAble = QueryAble
                    .Where(c =>
                    c.Name.ToLower().StartsWith(searchedWord.ToLower()) ||
                    c.Name.ToLower().Contains(searchedWord.ToLower())

                    ||
                      c.LastName.ToLower().StartsWith(searchedWord.ToLower()) ||
                      c.LastName.ToLower().Contains(searchedWord.ToLower())

                    ||
                       c.UserName.ToLower().StartsWith(searchedWord.ToLower()) ||
                      c.UserName.ToLower().Contains(searchedWord.ToLower())
                    );
            };

            if (perPage == 0)
                perPage = 1;
            page = page ?? 0;
            perPage = perPage ?? 10;

            var count = QueryAble.Count();
            double len = (double)count / (double)perPage;
            var totalPages = Math.Ceiling((double)len);

            var users = await QueryAble
             .OrderByDescending(c => c.CreateDate)
                .Skip((int)page * (int)perPage)
             .Take((int)perPage)

             .ToListAsync();

            var data = new ResponseType()
            {
                Users = users,
                TotalPages = totalPages
            };
            return data;
        }





        public async Task<ResponseType> GetListOfUserForVerificationForAdmin(int? page, int? perPage,
            string searchedWord, ProfileConfirmType? ProfileConfirmType)
        {
            var currentRole = GetCurrentRole();
            var currentUsername = GetCurrentUserName();

            var QueryAble = _context.Users.AsNoTracking().Select(c => new GetListOfUsersForVerification()
            {
                UserName = c.UserName,
                Name = c.Name,
                CreateDate = c.CreateDate,
                LastName = c.LastName,
                IsCompany = c.IsCompany,
                ProfileConfirmType = c.ProfileConfirmType,
                IsEditableProfile = c.IsEditableProfile,
                //PhoneNumber = c.PhoneNumber,
                PhoneNumberConfirmed = c.PhoneNumberConfirmed,
                CountryCode = c.CountryCode,
                ImageAddress = c.ImageAddress,
                isLockOut = (c.LockoutEnd != null && c.LockoutEnd > DateTime.Now),
            }).AsQueryable();

            if (!string.IsNullOrEmpty(searchedWord))
            {
                QueryAble = QueryAble
                    .Where(c =>
                    c.Name.ToLower().StartsWith(searchedWord.ToLower()) ||
                    c.Name.ToLower().Contains(searchedWord.ToLower())

                    ||
                      c.LastName.ToLower().StartsWith(searchedWord.ToLower()) ||
                      c.LastName.ToLower().Contains(searchedWord.ToLower())

                    ||
                       c.UserName.ToLower().StartsWith(searchedWord.ToLower()) ||
                      c.UserName.ToLower().Contains(searchedWord.ToLower())
                    );
            };

            if (ProfileConfirmType != null)
            {
                QueryAble = QueryAble.Where(c => c.ProfileConfirmType == ProfileConfirmType);
            }



            if (perPage == 0)
                perPage = 1;
            page = page ?? 0;
            perPage = perPage ?? 10;

            var count = QueryAble.Count();
            double len = (double)count / (double)perPage;
            var totalPages = Math.Ceiling((double)len);

            var users = await QueryAble
              .OrderByDescending(c => c.CreateDate)
             .Skip((int)page * (int)perPage)
             .Take((int)perPage)
             .ToListAsync();

            var data = new ResponseType()
            {
                Users = users,
                TotalPages = totalPages
            };
            return data;
        }




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
        /// get ProfileInformation
        /// </summary>
        /// <returns></returns>
        public async Task<ProfileGetDTO> ProfileGet()
        {
            var currentusername = GetCurrentUserName();
            if (string.IsNullOrEmpty(currentusername))
                return null;




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
                    BirthDate = c.BirthDate,
                    IsCompany = c.IsCompany,
                    IsEditableProfile = c.IsEditableProfile,
                    NationalCode = c.NationalCode,
                    Gender = c.Gender,
                    ProfileCertificate = _context.ProfileCertificateTBL
                                .Where(c => c.UserName == currentusername)
                                    .Select(c => new ProfileCertificateDTO
                                    {
                                        //Id = c.Id,
                                        //////////////Id = c.RequiredCertificatesId,
                                        //Id= c.Id,
                                        Id = c.RequiredCertificatesId,
                                        FileAdress = c.FileAddress,
                                        ServiceId = c.ServiceId,
                                        Username = c.UserName
                                    }).ToList(),
                    ProfileConfirmType = c.ProfileConfirmType,
                    Fields = c.Fields.Select(r => new FiledsDTO { DegreeType = r.DegreeType, Title = r.Title }).ToList()
                }).FirstOrDefaultAsync();

            return userFromDB;
        }



        /// <summary>
        /// گرفتن اطلاعات پروفایل شرکتی
        /// </summary>
        /// <returns></returns>
        public async Task<ProfileFirmGetDTO> ProfileFirmGet()
        {
            var currentusername = GetCurrentUserName();
            if (string.IsNullOrEmpty(currentusername))
                return null;

            var userFromDB = await _context.Users.Where(c => c.UserName == currentusername)
                .Select(c => new ProfileFirmGetDTO
                {

                    Id = c.Id,
                    Username = c.UserName,
                    Email = c.Email,
                    Bio = c.Bio,
                    //ImageAddress = c.ImageAddress,
                    IsCompany = c.IsCompany,
                    IsEditableProfile = c.IsEditableProfile,




                    FirmName = c.FirmProfile.FirmName,
                    FirmAddress = c.FirmProfile.FirmAddress,
                    CodePosti = c.FirmProfile.CodePosti,
                    FirmManagerName = c.FirmProfile.FirmManagerName,
                    FirmState = c.FirmProfile.FirmState,
                    FirmCountry = c.FirmProfile.FirmCountry,
                    FirmLogo = c.FirmProfile.FirmLogo,
                    NationalCode = c.FirmProfile.NationalCode,
                    FirmNationalID = c.FirmProfile.FirmNationalID,
                    FirmRegistrationID = c.FirmProfile.FirmRegistrationID,
                    FirmDateOfRegistration = c.FirmProfile.FirmDateOfRegistration,
                    //ProfileConfirmType = c.ProfileConfirmType,
                }).FirstOrDefaultAsync();

            return userFromDB;
        }










        public async Task<bool> UpdateProfile(AppUser userFromDB, List<ProfileCertificateTBL> certificationFromDB, UpdateProfileDTO model)
        {
            try
            {
                //var serviceFromDb = await _context.ServiceTBL.FindAsync(model.Id);
                if (model == null) return false;

                userFromDB.Bio = model.Bio;
                userFromDB.Email = model.Email;
                userFromDB.Name = model.Name;
                userFromDB.LastName = model.LastName;
                userFromDB.BirthDate = model.BirthDate;
                userFromDB.Gender = model.Gender;
                userFromDB.NationalCode = model.NationalCode;

                //upload immage
                if (model.Image != null && model.Image.Length > 0 && model.Image.IsImage())
                {
                    var imageAddress = await SaveFileToHost("Upload/User/", userFromDB.ImageAddress, model.Image);
                    userFromDB.ImageAddress = imageAddress;
                }

                //upload video
                if (model.Video != null && model.Video.Length > 0 && model.Video.IsVideo())
                {
                    var videoAddress = await SaveFileToHost("Upload/User/", userFromDB.VideoAddress, model.Video);
                    userFromDB.VideoAddress = videoAddress;
                }

                _context.FieldTBL.RemoveRange(userFromDB.Fields);
                if (model.Fields != null)
                {
                    userFromDB.Fields = new List<FieldTBL>();
                    foreach (var item in model.Fields)
                    {
                        var newFiled = new FieldTBL()
                        {
                            Title = item.Title,
                            DegreeType = item.DegreeType
                        };
                        userFromDB.Fields.Add(newFiled);
                    }
                }


                //var certifications = new List<ProfileCertificateTBL>();
                var currentUserName = GetCurrentUserName();

                var idsShouldBeRemoved = new List<int?>();

                ////////////idsShouldBeRemoved = certificationFromDB.Select(c => c.RequiredCertificatesId).ToList();
                //////////////////////////////////////////////idsShouldBeRemoved = certificationFromDB.Select(c => c.Id).ToList();
                if (model.RequiredFile != null)
                {

                    foreach (var item in model.RequiredFile)
                    {
                        if (!item.AddNew  /*item.FileId != null*/)
                        {
                            //in dar ram ast
                            var existcertificate = certificationFromDB.Where(c => c.RequiredCertificatesId == item.FileId && c.ServiceId == item.ServiceId).FirstOrDefault();

                            //In yani agar certificate fetestade  ghablan bood faghat filesho dare change mikone   
                            if (existcertificate != null && item.File != null)
                            {
                                idsShouldBeRemoved.Remove(existcertificate.Id);
                                var fileaddress = await SaveFileToHost("Upload/ProfileCertificate/", existcertificate.FileAddress, item.File);
                                //_context.ProfileCertificateTBL.Remove(existcertificate);
                                existcertificate.FileAddress = fileaddress;

                                //var newCertificate = new ProfileCertificateTBL()
                                //{
                                //    ServiceId = item.ServiceId,
                                //    UserName = currentUserName,
                                //    ///uploadFile
                                //    FileAddress = fileaddress,
                                //};
                                //await _context.ProfileCertificateTBL.AddAsync(newCertificate);
                            }
                            //else
                            //{
                            //    //in certificate haee ast ke ghablan vared kard alan nist
                            //    var notExistCertificate = certificationFromDB.Where(c => c.Id != item.FileId & c.ServiceId == item.ServiceId).FirstOrDefault();
                            //    DeleteFileFromHost("Upload/ProfileCertificate", notExistCertificate.FileAddress);
                            //    _context.ProfileCertificateTBL.Remove(notExistCertificate);
                            //    //foreach (var item2 in notExistCertificate)
                            //    //{
                            //    //    DeleteFileFromHost("Upload/ProfileCertificate", item2.FileAddress);
                            //    //    _context.ProfileCertificateTBL.Remove(item2);
                            //    //}
                            //}
                        }
                        //new Certifivcaatefile added 
                        else
                        {
                            //var certificate = certificationFromDB.Where(c => c.Id == item.FileId).FirstOrDefault();
                            var fileaddress = await SaveFileToHost("Upload/ProfileCertificate/", null, item.File);
                            var newCertificate = new ProfileCertificateTBL()
                            {
                                ServiceId = item.ServiceId,
                                UserName = currentUserName,
                                //RequiredCertificatesId = model,
                                ///uploadFile
                                RequiredCertificatesId = item.FileId,
                                FileAddress = fileaddress,
                                ProfileConfirmType = ProfileConfirmType.Pending,
                            };
                            await _context.ProfileCertificateTBL.AddAsync(newCertificate);
                        }
                    }
                }



                ////var existcertificate = certificationFromDB.Where(c => c.Id == item.FileId && c.ServiceId == item.ServiceId).FirstOrDefault();
                //foreach (var item in idsShouldBeRemoved)
                //{
                //    var certi = certificationFromDB.Where(c => c.Id == item).FirstOrDefault();
                //    DeleteFileFromHost(certi.FileAddress);
                //    _context.ProfileCertificateTBL.Remove(certi);
                //}

                await _context.SaveChangesAsync();
                return true;



            }
            catch
            {
                return false;
            }
        }







        




        public async Task<bool> UpdateFirmProfile(AppUser userFromDB, UpdateFirmProfileDTO model)
        {
            try
            {
                if (model == null) return false;

                userFromDB.Bio = model.Bio;
                userFromDB.Email = model.Email;
                userFromDB.NationalCode = model.NationalCode;
                
                
                userFromDB.FirmProfile.NationalCode = model.NationalCode;
                userFromDB.FirmProfile.FirmName = model.FirmName;
                userFromDB.FirmProfile.FirmManagerName = model.FirmManagerName;
                userFromDB.FirmProfile.FirmAddress = model.FirmAddress;
                userFromDB.FirmProfile.FirmState = model.FirmState;
                userFromDB.FirmProfile.FirmCountry = model.FirmCountry;
                userFromDB.FirmProfile.FirmNationalID = model.FirmNationalID;
                userFromDB.FirmProfile.FirmDateOfRegistration = model.FirmDateOfRegistration;
                userFromDB.FirmProfile.FirmRegistrationID = model.FirmRegistrationID;
                userFromDB.FirmProfile.CodePosti= model.CodePosti;
                userFromDB.FirmProfile.FirmRegistrationID = model.FirmRegistrationID;

                //upload immage
                if (model.FirmLogo != null && model.FirmLogo.Length > 0 && model.FirmLogo.IsImage())
                {
                    var imageAddress = await SaveFileToHost("Upload/User/", userFromDB.FirmProfile.FirmLogo, model.FirmLogo);
                    userFromDB.FirmProfile.FirmLogo = imageAddress;
                }

                await _context.SaveChangesAsync();
                return true;



            }
            catch
            {
                return false;
            }
        }






        /// <summary>
        /// ولیدیت کردن آبجکت آپدیت پروفابل
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool succsseded, List<string> result)> ValidateUpdateProfile(UpdateProfileDTO model)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();

            var IsInGender = Enum.IsDefined(typeof(Gender), model.Gender);
            if (!IsInGender)
            {
                IsValid = false;
                Errors.Add(_localizerAccount["InValidGender"].Value.ToString());
            }


            if (model.Fields != null)
            {
                if (model.Fields.Count > 2)
                {
                    IsValid = false;
                    Errors.Add("Fields count is too much");
                }
            }

            //validate RequiredCertificate
            if (model.RequiredFile != null)
            {
                foreach (var item in model.RequiredFile)
                {
                    //if (item.ServiceId == null)
                    //{
                    //    IsValid = false;
                    //    Errors.Add(_localizerAccount["Service Is required"].Value.ToString());
                    //}
                    var exist = await _context.ServiceTBL.AnyAsync(c => c.Id == item.ServiceId);
                    if (!exist)
                    {
                        IsValid = false;
                        Errors.Add(_localizerAccount["InvalidServiceType"].Value.ToString());
                    }

                    var exist2 = await _context.ServidceTypeRequiredCertificatesTBL.AnyAsync(c => c.Id == item.FileId && c.ServiceId == item.ServiceId);
                    if (!exist2)
                    {
                        IsValid = false;
                        Errors.Add(_localizerAccount["InvalidRequiredServiocTypeCertification"].Value.ToString());
                    }


                    //********************************** holy fucking validation *********************************************
                    //if (model.RequiredFile != null)
                    //{
                    //    if (item.AddNew)
                    //    {
                    //        var exist3 = await _context.ProfileCertificateTBL.AnyAsync(c => c.RequiredCertificatesId == item.FileId && c.ServiceId == item.ServiceId);
                    //        if (exist3)
                    //        {
                    //            IsValid = false;
                    //            Errors.Add(_localizerAccount["ProfileCertficateAlreadyExist"].Value.ToString());
                    //        }
                    //    }
                    //}


                    if (item.File == null)
                    {
                        IsValid = false;
                        Errors.Add(_localizerAccount["FileIsRequired"].Value.ToString());
                    }
                    else
                    {
                        if (!item.File.IsPdfOrImage())
                        {
                            IsValid = false;
                            Errors.Add(_localizerAccount["InValidFileFormat"].Value.ToString());
                        }
                    }
                }
            }

            //validate video 
            if (model.Video != null)
            {
                if (!model.Video.IsVideo())
                {
                    IsValid = false;
                    Errors.Add(_localizerAccount["InValidVideoFormat"].Value.ToString());
                }
                if (model.Image.Length > 50000000)
                {
                    IsValid = false;
                    Errors.Add(_localizerAccount["FileIsTooLarge"].Value.ToString());
                }
            }


            //validate Image
            if (model.Image != null)
            {
                //string uniqueFileName = null;
                if (!model.Image.IsImage())
                {
                    IsValid = false;
                    Errors.Add(_localizerAccount["InValidImageFormat"].Value.ToString());
                }
                if (model.Image.Length > 4000000)
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







        /// <summary>
        /// ولیدیت کردن آبجکت پروفایل شرکتی
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public (bool succsseded, List<string> result) ValidateUpdateFirmProfile(UpdateFirmProfileDTO model)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();

            //validate Image
            if (model.FirmLogo != null)
            {
                //string uniqueFileName = null;
                if (!model.FirmLogo.IsImage())
                {
                    IsValid = false;
                    Errors.Add(_localizerAccount["InValidImageFormat"].Value.ToString());
                }
                if (model.FirmLogo.Length > 4000000)
                {
                    IsValid = false;
                    Errors.Add(_localizerAccount["FileIsTooLarge"].Value.ToString());
                }
            }


            return (IsValid, Errors);
        }



        public async Task<string> SaveFileToHost(string path, string lastPath, IFormFile file)
        {

            try
            {
                string uniqueVideoFileName = null;
                if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
                {
                    _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, path);
                uniqueVideoFileName = (Guid.NewGuid().ToString().GetImgUrlFriendly() + "_" + file.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueVideoFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                //Delete LastImage Image
                if (!string.IsNullOrEmpty(lastPath))
                {
                    //var LastVideoPath = lastPath?.Substring(1);
                    var LastPath = Path.Combine(_hostingEnvironment.WebRootPath, lastPath);
                    if (System.IO.File.Exists(LastPath))
                    {
                        System.IO.File.Delete(LastPath);
                    }
                }
                //update Newe video Address To database
                //user.VideoAddress = "/Upload/User/" + uniqueVideoFileName;

                return path + uniqueVideoFileName;

            }
            catch
            {
                return null;
            }
        }




        public bool DeleteFileFromHost(string path)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
                {
                    _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }

                //Delete LastImage Image
                if (!string.IsNullOrEmpty(path))
                {
                    //var LastPath = path?.Substring(1);
                    var LastPath = Path.Combine(_hostingEnvironment.WebRootPath, path);
                    if (System.IO.File.Exists(LastPath))
                    {
                        System.IO.File.Delete(LastPath);
                    }
                }
                //update Newe video Address To database
                //user.VideoAddress = "/Upload/User/" + uniqueVideoFileName;
                return true;

            }
            catch
            {
                return false;
            }
        }



        /// <summary>
        ///آیا 2کاربر هم کشورند یا خیر
        /// </summary>
        /// <param name="clinet"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public bool IsNative(AppUser clinet, AppUser provider)
        {
            if (clinet == null || provider == null)
                return false;
            if (clinet?.CountryCode.ToLower() != provider.CountryCode.ToLower())
                return false;
            return true;
        }


        /// <summary>
        ///آیا 2کاربر هم کشورند یا خیر
        /// </summary>
        /// <param name="clinet"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public async Task<bool> IsNative(string clinetUserName, string providerUserName)
        {
            var usersFromDB = await _context.Users
                   .Where(c => c.UserName == clinetUserName || c.UserName == providerUserName)
                   .Select(c => new { c.CountryCode, c.UserName })
                   .ToListAsync();

            var clientFromDB = usersFromDB.Where(c => c.UserName == clinetUserName).FirstOrDefault();
            var providerFromDB = usersFromDB.Where(c => c.UserName == providerUserName).FirstOrDefault();

            if (clientFromDB == null || providerUserName == null)
                return false;
            if (clientFromDB?.CountryCode.ToLower() != providerFromDB.CountryCode.ToLower())
                return false;
            return true;
        }





        public bool IsPersianLanguage()
        {
            if (CultureInfo.CurrentCulture.Name == PublicHelper.persianCultureName)
                return true;
            return false;
        }



    }
}
