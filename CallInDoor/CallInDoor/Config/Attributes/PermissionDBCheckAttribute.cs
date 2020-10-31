using Domain;
using Domain.DTO.Response;
using Domain.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CallInDoor.Config.Attributes
{
    public class PermissionDBCheckAttribute : Attribute, IActionFilter
    {

        public bool IsAdmin { get; set; }
        public string[] requiredPermission { get; set; }


        public void OnActionExecuted(ActionExecutedContext context) { }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var _context = context.HttpContext.RequestServices.GetRequiredService<DataContext>();
            var _localizerShared = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<ShareResource>>();


            var currentRole = context.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            var currentUserName = context.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var currentPermission = context.HttpContext?.User?.Claims?.Where(x => x.Type == PublicPermissions.Permission).ToList();
            var currentSerialNumber = context.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == PublicHelper.SerialNumberClaim)?.Value;

            var userFromDB = _context.Users.Where(c => c.UserName == currentUserName && c.SerialNumber == currentSerialNumber)
                    .Select(c => new { c.Id, c.LockoutEnd }).FirstOrDefault();

            if (string.IsNullOrEmpty(userFromDB.Id))
            {
                context.Result = new UnauthorizedObjectResult(new ApiResponse(401, ErrorMessage(_localizerShared, IsAdmin)));
                return;
            }

            if (userFromDB.LockoutEnd != null && userFromDB.LockoutEnd > DateTime.Now)
            {
                var errors = new List<string>() {
                _localizerShared["BlockUserMessage"].Value.ToString()
                };
                context.Result = new BadRequestObjectResult(new ApiBadRequestResponse(errors));
                return;
            }


            if (requiredPermission.Count() != 0)
            {
                var query1 = (from ur in _context.UserRoles.Where(c => c.UserId == userFromDB.Id)
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

                var res = query1.ToList();

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

                if (!HavePermission)
                {
                    context.Result = new UnauthorizedObjectResult(new ApiResponse(403, ErrorMessage(_localizerShared, IsAdmin)));
                }
            }


        }


        private string ErrorMessage(IStringLocalizer<ShareResource> localizerShared, bool IsAdmin)
        {
            var errorMessage = "";
            if (!IsAdmin)
                errorMessage = localizerShared["UnauthorizedMessage"].Value.ToString();
            else
                errorMessage = PubicMessages.UnAuthorizeMessage;
            return errorMessage;
        }


    }
}
