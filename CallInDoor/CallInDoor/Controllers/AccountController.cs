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



        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICommonService _CommonService;
        private readonly IAccountService _accountService;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtManager _jwtGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IStringLocalizer<AccountController> _localizer;
        private IStringLocalizer<ShareResource> _localizerShared;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            DataContext context,
               IJwtManager jwtGenerator,
               IHttpContextAccessor httpContextAccessor,
               IStringLocalizer<AccountController> localizer,
                IStringLocalizer<ShareResource> localizerShared,
                ICommonService commonService,
                IAccountService accountService
            )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _localizerShared = localizerShared;
            _CommonService = commonService;
            _accountService = accountService;
        }





        #region  login

        //[AllowAnonymous]
        //[HttpPost("login")]
        //public async Task<ActionResult> Login(LoginDTO model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = new List<string>();
        //        foreach (var item in ModelState.Values)
        //        {
        //            foreach (var err in item.Errors)
        //            {
        //                errors.Add(err.ErrorMessage);
        //            }
        //        }
        //        return BadRequest(new ApiBadRequestResponse(errors));

        //        //return new JsonResult(new { Status = 0, Message = "bad request", Data = errors });
        //    }


        //    //var user = await _userManager.FindByNameAsync(model.UserName);
        //    var user = await _context.Users.Where(c => c.UserName == model.UserName).FirstOrDefaultAsync();
        //    if (user == null)
        //    {
        //        return new JsonResult(new { Status = 0, Message = " نام کاربری یا رمز عبور اشتباست " });
        //    }

        //    var result = await _signInManager
        //        .CheckPasswordSignInAsync(user, model.Password, false);
        //    if (result.Succeeded)
        //    {
        //        var SerialNumber = Guid.NewGuid().ToString().GetHash();

        //        var userRoles = await _userManager.GetRolesAsync(user);
        //        var role = userRoles?.First();
        //        user.SerialNumber = SerialNumber;
        //        await _context.SaveChangesAsync();

        //        // TODO: generate token
        //        var userInfo = new User
        //        {
        //            Id = user.Id,
        //            Token = _jwtGenerator.CreateToken(user, role),
        //            UserName = user.UserName,
        //            Email = user.Email

        //        };
        //        return new JsonResult(new { Status = 1, Message = "ورود موفقیت آمیز", Data = userInfo });
        //    }
        //    return new JsonResult(new { Status = 0, Message = " نام کاربری یا رمز عبور اشتباست " });
        //}

        #endregion






        #region register

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var item in ModelState.Values)
                {
                    foreach (var err in item.Errors)
                    {
                        errors.Add(err.ErrorMessage);
                    }
                }
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            var user = await _accountService.FindUserByPhonenumber(model.PhoneNumber);
            //await _context.Users.Where(x => x.PhoneNumber == model.PhoneNumber).FirstOrDefaultAsync();


            var random = new Random();
            var code = random.Next(100000, 999999);

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
                PhoneNumber = model.PhoneNumber,
                Role = PublicHelper.USERROLE,
                verificationCode = code,
                verificationCodeExpireTime = DateTime.UtcNow.AddMinutes(3)
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
            //if (result.IsNotAllowed) { 
            ////logic  تایید شمار ه تماس
            //}
            return Unauthorized(new ApiResponse(401, _localizerShared["UnMathPhoneNumberPassword"].Value.ToString()));
        }


        #endregion





        #region  veryfication code

        [AllowAnonymous]
        [HttpPost("VerifyCode")]
        public async Task<IActionResult> Verify([FromBody] VerifyDTO model)
        {
            //if (!ModelState.IsValid)
            //{
            //    var errors = new List<string>();
            //    foreach (var item in ModelState.Values)
            //    {
            //        foreach (var err in item.Errors)
            //        {
            //            errors.Add(err.ErrorMessage);
            //        }
            //    }
            //    return BadRequest(new ApiBadRequestResponse(errors));
            //}
            var res = await _accountService.CheckVeyficatioCode(model);
            if (res.status == 0)
            {
                return BadRequest(new ApiBadRequestResponse(res.erros));
            }


            var user = await _context.Users.Where(c => c.PhoneNumber == model.PhoneNumber).FirstOrDefaultAsync();
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








        #region login

        [AllowAnonymous]
        [HttpPost("ForgetPasswod")]
        public async Task<IActionResult> ForgetPasswod([FromBody] ForgetPasswordDTO model)
        {

            var user = await _accountService.FindUserByPhonenumber(model.PhoneNumber);
            if (user == null)
                return Unauthorized(new ApiResponse(401, _localizerShared["InvalidPhoneNumber"].Value.ToString()));

            /*................................................. question.......................................*/
            if (!await _userManager.IsPhoneNumberConfirmedAsync(user))
            {
                return Unauthorized(new ApiResponse(401, _localizerShared["InvalidPhoneNumber"].Value.ToString()));
            }



            var random = new Random();
            var code = random.Next(100000, 999999);

            user.verificationCode = code;
            user.verificationCodeExpireTime = DateTime.UtcNow.AddMinutes(3);

            await _context.SaveChangesAsync();

            //send code


            //var token = await _accountService.GeneratePasswordResetToken(user);

            //var passwordResetLink = Url.Action("ResetPassword", "Account",
            //    new { PhoneNumber = model.PhoneNumber, token = token }, Request.Scheme);

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







    }
}
