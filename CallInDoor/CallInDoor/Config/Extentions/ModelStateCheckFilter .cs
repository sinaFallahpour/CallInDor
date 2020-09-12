
using Domain.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallInDoor.Config.Extentions
{
    public class ModelStateCheckFilter : IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context) { }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var modelState = context.ModelState;
                var errors = new List<string>();
                foreach (var item in modelState.Values)
                {
                    foreach (var err in item.Errors)
                    {
                        errors.Add(err.ErrorMessage);
                    }
                }
                //return BadRequest(new ApiBadRequestResponse(errors));

                context.Result = new BadRequestObjectResult(new ApiBadRequestResponse(errors));
            }
        }
    }
}
