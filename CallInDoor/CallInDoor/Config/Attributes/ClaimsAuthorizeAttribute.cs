using Domain;
using Domain.DTO.Account;
using Domain.DTO.Response;
using Domain.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CallInDoor.Config.Attributes
{
    public class ClaimsAuthorizeAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }
        public  void OnActionExecuting(ActionExecutingContext context)
        {
            var _context = context.HttpContext.RequestServices.GetRequiredService<DataContext>();

            var currentSerialNumber = context.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == PublicHelper.SerialNumberClaim)?.Value;
            var currentUserName = context.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var IsExist =  _context.Users
                    .Any(x => x.SerialNumber == currentSerialNumber && x.UserName == currentUserName);
            if (!IsExist)
                context.Result = new UnauthorizedObjectResult(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));
        }

    }
}
