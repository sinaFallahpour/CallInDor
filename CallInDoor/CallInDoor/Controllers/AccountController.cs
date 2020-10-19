using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallInDoor.Models;
using Domain;
using Domain.DTO.Account;
using Domain.DTO.Response;
using Domain.Entities;
using Domain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Service.Interfaces.JwtManager;

namespace CallInDoor.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class AccountController : BaseControlle
    {


        #region ctor


        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        private readonly IAccountService _accountService;
        private readonly IJwtManager _jwtGenerator;
        private IStringLocalizer<AccountController> _localizer;
        private IStringLocalizer<ShareResource> _localizerShared;

        public AccountController(UserManager<AppUser> userManager,
             RoleManager<AppRole> roleManager,
            DataContext context,
               IJwtManager jwtGenerator,
               IStringLocalizer<AccountController> localizer,
                IStringLocalizer<ShareResource> localizerShared,
                IAccountService accountService
            )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _localizer = localizer;
            _localizerShared = localizerShared;
            _accountService = accountService;
            _jwtGenerator = jwtGenerator;
        }

        #endregion ctor

        #region register

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO model)
        {


            var phonenumber = model.CountryCode.ToString().Trim() + model.PhoneNumber.Trim();
            var user = await _accountService.FindUserByPhonenumber(phonenumber);
            //await _context.Users.Where(x => x.PhoneNumber == model.PhoneNumber).FirstOrDefaultAsync();


            var random = new Random();
            //var code = random.Next(100000, 999999);
            var code = 1111;


            if (user != null)
            {
                //کاربری جدید که هم شماره اینم کاربره
                if (user.PhoneNumberConfirmed == true)
                {
                    var errors = new List<string>();
                    errors.Add(_localizer["PhoneNumber  already  exist."].Value.ToString());
                    return BadRequest(new ApiBadRequestResponse(errors));
                    //return new JsonResult(new { Status = 0, Message = _localizer["PhoneNumber  already  exist."].Value.ToString() });
                }

                if (user.PhoneNumberConfirmed == false)
                {
                    user.verificationCode = code;
                    user.verificationCodeExpireTime = DateTime.UtcNow.AddMinutes(3);
                    //send code ;

                    await _context.SaveChangesAsync();

                    //return
                    return Ok(new ApiOkResponse(new DataFormat()
                    {
                        Status = 1,
                        data = new { },
                        Message = _localizerShared["SuccessMessage"].Value.ToString()
                    },
                    _localizerShared["SuccessMessage"].Value.ToString()
                   ));
                }
            }

            var SerialNumber = Guid.NewGuid().ToString().GetHash();

            var newUser = new AppUser
            {
                UserName = model.CountryCode.ToString().Trim() + model.PhoneNumber.Trim(),
                SerialNumber = SerialNumber,
                PhoneNumber = model.CountryCode.ToString() + model.PhoneNumber.Trim(),
                Role = PublicHelper.USERROLE,
                verificationCode = code,
                verificationCodeExpireTime = DateTime.UtcNow.AddMinutes(3),
                CountryCode = model.CountryCode
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {

                //send Code

                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = new { },
                    Message = _localizerShared["SuccessMessage"].Value.ToString()
                },
                _localizerShared["SuccessMessage"].Value.ToString()
                ));

                //return new JsonResult(new { Status = 1, Message = " ثبت نام موفقیت آمیز", Data = userInfo });
            }


            return StatusCode(StatusCodes.Status500InternalServerError,
                                    new ApiResponse(500, _localizerShared["InternalServerMessage"].Value.ToString()));

            //return Ok(new ApiOkResponse(new DataFormat() { Status = 0, Message = _localizerShared["ErrorMessage"].Value.ToString() }, _localizerShared["ErrorMessage"].Value.ToString()));
        }

        #endregion

        #region login

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {

            model.PhoneNumber = model.CountryCode.ToString().Trim() + model.PhoneNumber.Trim();
            var user = await _accountService.FindUserByPhonenumber(model.PhoneNumber);
            if (user == null)
                return Unauthorized(new ApiResponse(401, _localizerShared["InvalidPhoneNumber"].Value.ToString()));

            var result = await _accountService.CheckPasswordAsync(user, model.Password);

            if (result.Succeeded)
            {

                var SerialNumber = Guid.NewGuid().ToString().GetHash();
                user.SerialNumber = SerialNumber;
                await _context.SaveChangesAsync();

                // TODO: generate token
                var userInfo = new User
                {
                    Id = user.Id,
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName,
                };

                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = userInfo,
                    Message = _localizerShared["SuccessMessage"].Value.ToString()
                },
                _localizerShared["SuccessMessage"].Value.ToString()
               ));
            }
            if (result.IsNotAllowed)
            {
                return Unauthorized(new ApiResponse(401, _localizerShared["ConfirmPhoneMessage"].Value.ToString()));

            }
            return Unauthorized(new ApiResponse(401, _localizerShared["UnMathPhoneNumberPassword"].Value.ToString()));
        }


        #endregion

        #region AdminLogin

        [AllowAnonymous]
        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminLogin([FromBody] AdminLoginDTO model)
        {
            var user = await _context.Users.Where(c => c.PhoneNumber == model.PhoneNumber && c.PhoneNumberConfirmed == true).FirstOrDefaultAsync();
            if (user == null)
                return Unauthorized(new ApiResponse(401, "Invalid phone number or password."));


            if (user.Role != PublicHelper.ADMINROLE)
                return Unauthorized(new ApiResponse(401, "Inaccessibility"));

            //model.PhoneNumber = model.CountryCode.ToString().Trim() + model.PhoneNumber.Trim();
            //var user = await _accountService.FindUserByPhonenumber(model.PhoneNumber);

            //if (user == null)
            //    return Unauthorized(new ApiResponse(401, _localizerShared["InvalidPhoneNumber"].Value.ToString()));

            var result = await _accountService.CheckPasswordAsync(user, model.Password);

            if (result.Succeeded)
            {
                var SerialNumber = Guid.NewGuid().ToString().GetHash();
                user.SerialNumber = SerialNumber;
                await _context.SaveChangesAsync();

                // TODO: generate token
                var userInfo = new User
                {
                    Id = user.Id,
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName,
                };

                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = userInfo,
                    Message = PubicMessages.SuccessMessage
                },
                PubicMessages.SuccessMessage
               ));
            }
            if (result.IsNotAllowed)
            {
                return Unauthorized(new ApiResponse(401, "Invalid phone number or password."));
            }
            return Unauthorized(new ApiResponse(401, "Invalid phone number or password."));
        }


        #endregion

        #region IsAdminLoggedIn

        [HttpGet("IsAdminLoggedIn")]
        public async Task<ActionResult<User>> IsAdminLoggedIn()
        {

            var result = await _accountService.CheckTokenIsValidForAdminRole();
            if (!result.status)
                return Unauthorized(new ApiResponse(401, "Unauthorize."));


            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = result.username,
                Message = PubicMessages.SuccessMessage
            },
                PubicMessages.SuccessMessage
           ));



        }

        #endregion


        #region  veryfication code

        [AllowAnonymous]
        [HttpPost("VerifyCode")]
        public async Task<IActionResult> Verify([FromBody] VerifyDTO model)
        {

            var res = await _accountService.CheckVeyficatioCode(model);
            if (res.status == 0)
            {
                return BadRequest(new ApiBadRequestResponse(res.erros));
            }


            var phoneNumber = model.CountryCode.ToString().Trim() + model.PhoneNumber.Trim();
            var user = await _context.Users.Where(c => c.PhoneNumber == phoneNumber).FirstOrDefaultAsync();

            user.PhoneNumberConfirmed = true;
            var SerialNumber = Guid.NewGuid().ToString().GetHash();

            user.SerialNumber = SerialNumber;

            await _context.SaveChangesAsync();

            // TODO: generate token
            var userInfo = new User
            {
                Id = user.Id,
                Token = _jwtGenerator.CreateToken(user),
                UserName = user.UserName,
            };


            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = userInfo,
                Message = _localizerShared["SuccessMessage"].Value.ToString()
            },
            _localizerShared["SuccessMessage"].Value.ToString()
           ));


            //return new JsonResult(new { Status = 1, Message = "ورود موفقیت آمیز", Data = userInfo });
        }

        #endregion


        #region   RefreshToken  

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshDTO model)
        {

            var phoneNumber = model.CountryCode.ToString().Trim() + model.PhoneNumber.Trim();
            var user = await _accountService.FindUserByPhonenumber(phoneNumber);

            if (user == null)
            {
                return Unauthorized(new ApiResponse(401, _localizerShared["InvalidPhoneNumber"].Value.ToString()));
            }

            if (!user.PhoneNumberConfirmed)
            {
                return Unauthorized(new ApiResponse(401, _localizerShared["ConfirmPhoneMessage"].Value.ToString()));
            }


            var random = new Random();
            //var code = random.Next(100000, 999999);
            var code = 1111;

            user.verificationCode = code;
            user.verificationCodeExpireTime = DateTime.UtcNow.AddMinutes(3);

            await _context.SaveChangesAsync();


            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                //data = passwordResetLink,
                data = { },
                Message = _localizerShared["SuccessMessage"].Value.ToString()
            },
             _localizerShared["SuccessMessage"].Value.ToString()
            ));



        }

        #endregion


        #region ForgetPasswod

        [AllowAnonymous]
        [HttpPost("ForgetPasswod")]
        public async Task<IActionResult> ForgetPasswod([FromBody] ForgetPasswordDTO model)
        {

            var phoneNumber = model.CountryCode.ToString().Trim() + model.PhoneNumber.Trim();

            var user = await _accountService.FindUserByPhonenumber(phoneNumber);
            if (user == null)
                return Unauthorized(new ApiResponse(401, _localizerShared["InvalidPhoneNumber"].Value.ToString()));

            /*................................................. question.......................................*/
            if (!await _userManager.IsPhoneNumberConfirmedAsync(user))
            {
                return Unauthorized(new ApiResponse(401, _localizerShared["ConfirmPhoneMessage"].Value.ToString()));
            }


            var newpass = 8.RandomString();

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, newpass);
            if (passwordChangeResult.Succeeded)
            {
                //send Password to user


                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    //data = passwordResetLink,
                    data = { },
                    Message = _localizerShared["SuccessMessage"].Value.ToString()
                },
                 _localizerShared["SuccessMessage"].Value.ToString()
                ));
            }
            else
            {
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 0,
                    //data = passwordResetLink,
                    data = { },
                    Message = _localizerShared["InternalServerMessage"].Value.ToString()
                },
                 _localizerShared["InternalServerMessage"].Value.ToString()
              ));

            }




        }


        #endregion



        #region Admin with Profile



        #region GetAdminByIdInAdmin

        /// <summary>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("admin/GetAdminByIdInAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> GetAdminByIdInAdmin(string id)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));
            var query = (from u in _context.Users.Where(c => c.Id == id)
                         join ur in _context.UserRoles
                         on u.Id equals ur.UserId
                         join r in _context.Roles
                         on ur.RoleId equals r.Id
                         where r.Name != "User"
                         select new
                         {
                             u.Id,
                             //u.UserName,
                             u.Name,
                             u.LastName,
                             u.Email,
                             //PhoneNumber = ReomeveSomeString(u.PhoneNumber, u.CountryCode),
                             //CountryCode = u.CountryCode,
                             //roleName = r.Name,
                             roleId = r.Id
                         }).AsQueryable();



            var admin = await query.FirstOrDefaultAsync();

            if (admin == null)
                return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = admin,
                Message = PubicMessages.SuccessMessage
            },
               PubicMessages.SuccessMessage
              ));

        }




        private static string ReomeveSomeString(string sourceString, string removeString)
        {
            int index = sourceString.IndexOf(removeString);
            string cleanPath = (index < 0)
                ? sourceString
                : sourceString.Remove(index, removeString.Length);
            return cleanPath;
        }

        #endregion

        #region GetAllUserInAdmin

        [HttpGet("admin/GetAllAdminInAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> GetAllAdminInAdmin()
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));
            var query = (from u in _context.Users
                         join ur in _context.UserRoles
                         on u.Id equals ur.UserId
                         join r in _context.Roles
                         on ur.RoleId equals r.Id
                         where r.Name != "User"
                         select new
                         {
                             u.Id,
                             u.UserName,
                             u.Name,
                             u.LastName,
                             u.Email,
                             u.PhoneNumber,
                             u.CountryCode,
                             roleName = r.Name
                         }).AsQueryable();

            var admins = await query.ToListAsync();
            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = admins,
                Message = PubicMessages.SuccessMessage
            },
               PubicMessages.SuccessMessage
              ));
        }

        #endregion

        #region RegisterAdmin
        [HttpPost("admin/RegisterAdminInAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> RegisterAdmin([FromBody] RegisterAdminDTO model)
        {
            var phonenumber = model.CountryCode.ToString().Trim() + model.PhoneNumber.Trim();
            var isExist = _context.Users.Any(C => (C.PhoneNumber == phonenumber && C.PhoneNumberConfirmed)
             ||
             (C.Email == model.Email && C.EmailConfirmed));

            if (isExist)
            {
                var errors = new List<string>();
                errors.Add("phoneNumber number Or email already  exist.");
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            var SerialNumber = Guid.NewGuid().ToString().GetHash();

            var roleFromDB = await _context.Roles.Where(c => c.Id == model.RoleId).FirstOrDefaultAsync();
            if (roleFromDB == null)
            {
                var errors = new List<string>();
                errors.Add("role not exist.");
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            var newUser = new AppUser
            {
                UserName = model.CountryCode.ToString().Trim() + model.PhoneNumber.Trim(),
                Email = model.Email,
                NormalizedEmail = model.Email.Normalize(),
                EmailConfirmed = true,
                SerialNumber = SerialNumber,
                PhoneNumber = model.CountryCode.ToString() + model.PhoneNumber.Trim(),
                Role = PublicHelper.USERROLE,
                Name = model.Name,
                LastName = model.LastName,
                verificationCodeExpireTime = DateTime.UtcNow.AddMinutes(3),
                CountryCode = model.CountryCode
            };



            var userrole = await _userManager.GetRolesAsync(newUser);
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var result = await _userManager.CreateAsync(newUser, model.Password);
                    if (result.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(newUser, roleFromDB.Name);
                        var response = new
                        {
                            newUser.Id,
                            newUser.Name,
                            newUser.LastName,
                            newUser.Email,
                            roleName = roleFromDB.Name
                        };

                        if (roleResult.Succeeded)
                        {

                            transaction.Commit();
                            return Ok(new ApiOkResponse(new DataFormat()
                            {
                                Status = 1,
                                data = response,
                                Message = PubicMessages.SuccessMessage
                            },
                                PubicMessages.SuccessMessage
                          ));
                        }
                        else
                        {
                            transaction.Rollback();
                            return StatusCode(StatusCodes.Status500InternalServerError,
                                          new ApiResponse(500, PubicMessages.InternalServerMessage));
                        }
                    }
                    else if (result.Errors.Any(c => c.Code == "DuplicateUserName}"))
                    {
                        var err = new List<string>();
                        err.Add($" {model.PhoneNumber} is already taken");
                        return BadRequest(new ApiBadRequestResponse(err));
                    }

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return StatusCode(StatusCodes.Status500InternalServerError,
                                 new ApiResponse(500, PubicMessages.InternalServerMessage));
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                                    new ApiResponse(500, PubicMessages.InternalServerMessage));
        }


        #endregion


        #region UpdateAdmin
        // /api/Profile/UpdateProfile
        [HttpPut("admin/UpdateAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> UpdateAdmin([FromBody] UpdateAdminDTO model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var result = await _accountService.CheckTokenIsValidForAdminRole();
                    if (!result.status)
                        return Unauthorized(new ApiResponse(401, "Unauthorize."));

                    //var admin = await _userManager.FindByIdAsync(model.Id);
                    var admin = await _context.Users.Where(c =>
                     c.Id == model.Id).FirstOrDefaultAsync();
                    if (admin == null)
                        return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));

                    var roleFromDB = await _roleManager.FindByIdAsync(model.RoleId);
                    if (roleFromDB == null)
                    {
                        var err = new List<string>();
                        err.Add("invalid role");
                        return BadRequest(new ApiBadRequestResponse(err));
                    }


                    var userRoles = await _userManager.GetRolesAsync(admin);
                    var res = await _userManager.RemoveFromRolesAsync(admin, userRoles);

                    if (!res.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        var identitErrors = res.Errors.Select(c => c.Description).ToList();
                        return BadRequest(new ApiBadRequestResponse(identitErrors));
                    }
                    res = await _userManager.AddToRoleAsync(admin, roleFromDB.Name);
                    if (!res.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        var identitErrors = res.Errors.Select(c => c.Description).ToList();
                        return BadRequest(new ApiBadRequestResponse(identitErrors));
                    }

                    var emailExist = await _context.Users.AnyAsync(c => c.Email == model.Email && c.Id != model.Id);
                    if (emailExist)
                    {
                        var err = new List<string>();
                        err.Add($"Email ${model.Email} already taken.");
                        return BadRequest(new ApiBadRequestResponse(err));
                    }

                    admin.Name = model.Name;
                    admin.LastName = model.LastName;
                    admin.Email = model.Email;

                    var resposne = new
                    {
                        admin.Id,
                        admin.Name,
                        admin.LastName,
                        admin.Email,
                        roleName = roleFromDB.Name,
                    };

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Ok(new ApiOkResponse(new DataFormat()
                    {
                        Status = 1,
                        data = resposne,
                        Message = _localizerShared["SuccessMessage"].Value.ToString()
                    },
                     _localizerShared["SuccessMessage"].Value.ToString()
                    ));

                }
                catch
                {
                    await transaction.RollbackAsync();

                    return StatusCode(StatusCodes.Status500InternalServerError,
                         new ApiResponse(500, PubicMessages.InternalServerMessage));
                }
            }



        }

        #endregion

        #endregion





        #region  Role


        /// <summary>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("Role/GetRoleByIdInAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> GetRoleByIdInAdmin(string id)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = role,
                Message = PubicMessages.SuccessMessage
            },
            PubicMessages.SuccessMessage
           ));

        }






        // GET: api/GetAllServiceForAdmin
        [HttpGet("Role/GetAllActiveRolesInAdmin")]
        [Authorize]
        public async Task<ActionResult> GetAllActiveRolesInAdmin()
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var roles = await _roleManager.Roles.Where(c=>c.IsEnabled).ToListAsync();

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = roles,
                Message = PubicMessages.SuccessMessage
            },
          PubicMessages.SuccessMessage
         ));

        }






        // GET: api/GetAllServiceForAdmin
        [HttpGet("Role/GetAllRolesInAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> GetAllRoleForAdmin()
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var roles = await _roleManager.Roles.ToListAsync();

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = roles,
                Message = PubicMessages.SuccessMessage
            },
          PubicMessages.SuccessMessage
         ));

        }







        /// <summary>
        /// ایجاد رول
        /// </summary>
        /// <param name="CreateServiceDTO"></param>
        /// <returns></returns>
        [HttpPost("Role/CreateRoleInAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> CreateRoleInAdmin([FromBody] RoleDTO model)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var role = new AppRole(model.Name);
            role.IsEnabled = model.IsEnabled;
            role.NormalizedName = model.Name.Normalize();

            var roleResult = await _roleManager.CreateAsync(role);
            if (roleResult.Succeeded)
            {
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = role,
                    Message = PubicMessages.SuccessMessage
                },
                   PubicMessages.SuccessMessage
                  ));
            }

            if (roleResult.Errors.Any(c => c.Code == "DuplicateRoleName"))
            {
                var err = new List<string>();
                err.Add($"Role name {model.Name} is already taken");
                return BadRequest(new ApiBadRequestResponse(err));
            }


            return StatusCode(StatusCodes.Status500InternalServerError,
              new ApiResponse(500, PubicMessages.InternalServerMessage)
            );

            //return badrequest(new ApiResponse(401, _localizerShared["InvalidPhoneNumber"].Value.ToString()));

        }








        /// <summary>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("Role/UpdateRoleInAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> UpdateRoleInAdmin([FromBody] RoleDTO model)
        {

            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            var roleFromDB = await _roleManager.FindByIdAsync(model.Id);
            if (roleFromDB == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

            roleFromDB.Name = model.Name;
            roleFromDB.NormalizedName = model.Name.Normalize();
            roleFromDB.IsEnabled = model.IsEnabled;


            try
            {
                await _context.SaveChangesAsync();
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = roleFromDB,
                    Message = PubicMessages.SuccessMessage
                },
                   PubicMessages.SuccessMessage
                  ));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                new ApiResponse(500, _localizerShared["InternalServerMessage"].Value.ToString()));
            }

        }








        #endregion 

    }
}
