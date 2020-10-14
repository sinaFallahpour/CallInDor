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
        private readonly IAccountService _accountService;
        private readonly IJwtManager _jwtGenerator;
        private IStringLocalizer<AccountController> _localizer;
        private IStringLocalizer<ShareResource> _localizerShared;

        public AccountController(UserManager<AppUser> userManager,
            DataContext context,
               IJwtManager jwtGenerator,
               IStringLocalizer<AccountController> localizer,
                IStringLocalizer<ShareResource> localizerShared,
                IAccountService accountService
            )
        {
            _context = context;
            _userManager = userManager;
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


        #endregion 

    }
}
