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


            return Ok(new ApiOkResponse(new DataFormat() { Status = 0, Message = _localizerShared["ErrorMessage"].Value.ToString() }, _localizerShared["ErrorMessage"].Value.ToString()));
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



        #region  Role


        /// <summary>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetRoleByIdInAdmin")]
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
        [HttpGet("GetAllRolesInAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> GetAllServiceForAdmin()
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
        [HttpPost("CreateRoleInAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> CreateRoleInAdmin([FromBody] RoleDTO model)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var role = new AppRole(model.Name);
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
        [HttpPut("UpdateRoleInAdmin")]
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
