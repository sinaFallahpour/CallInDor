using Domain;
using Domain.DTO.Response;
using Domain.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace CallInDoor.Config.Attributes
{
    public class ClaimsAuthorizeAttribute : Attribute, IActionFilter
    {
        public bool IsAdmin { get; set; }

        public void OnActionExecuted(ActionExecutedContext context) { }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var _context = context.HttpContext.RequestServices.GetRequiredService<DataContext>();
            var _localizerShared = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<ShareResource>>();

            var currentSerialNumber = context.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == PublicHelper.SerialNumberClaim)?.Value;
            var currentUserName = context.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var IsExist = _context.Users.AsNoTracking()
                        .Any(x => x.SerialNumber == currentSerialNumber && x.UserName == currentUserName);
            if (!IsExist)
            {

                ///این if دیگر بی کاربرد است چون همه ریسپانس ها یکدست شد
                if (!IsAdmin)
                {
                    List<string> errors = new List<string> { _localizerShared["UnauthorizedMessage"].Value.ToString() };
                    context.Result = new UnauthorizedObjectResult(new ApiBadRequestResponse(errors, 401));
                   
                    return;
                    //return Unauthorized(new ApiBadRequestResponse(erroses, 401));
                }

                List<string> errors2 = new List<string> { PubicMessages.UnAuthorizeMessage };
                context.Result = new UnauthorizedObjectResult(new ApiBadRequestResponse(errors2, 401));
                //////context.Result = new UnauthorizedObjectResult(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));
            }
        }


        //private string ErrorMessage(IStringLocalizer<ShareResource> localizerShared, bool IsAdmin)
        //{
        //    var errorMessage = "";
        //    if (!IsAdmin)
        //        errorMessage = localizerShared["UnauthorizedMessage"].Value.ToString();
        //    else
        //        errorMessage = PubicMessages.UnAuthorizeMessage;
        //    return errorMessage;
        //}


    }
}
