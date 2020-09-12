using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Errors;
using Domain.DTO.Response;
using Domain.test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CallInDoor.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class testController : ControllerBase
    {

        private IStringLocalizer<test> _localizer;

        public testController(IStringLocalizer<test> localizer)
        {
            _localizer = localizer;
        }


        [HttpGet("")]
        public ActionResult test()
        {
            var username = _localizer["login"];
            var usernameVal = _localizer["login"].Value.ToString();
            return new JsonResult(new { us = username, u2 = usernameVal });
        }





        [HttpPost("testse")]
        public ActionResult testpost([FromBody] test test)
        {
            if (!ModelState.IsValid)
            {
                //return NotFound(new ApiResponse(404, "product not found"));

                var errors = new List<string>();
                foreach (var item in ModelState.Values)
                {
                    foreach (var err in item.Errors)
                    {
                        errors.Add(err.ErrorMessage);
                    }
                }
                return NotFound(new ApiResponse(404,"sadsds"));
                //return BadRequest(new ApiBadRequestResponse(errors));
            }

            var nn = new { Status = 1, message = "12121", data = test };
            return Ok(new ApiOkResponse(nn, "ss"));
        }



        [HttpPost("testError")]
        public ActionResult errors([FromBody] test test)
        {
            //return NotFound(new JsonResult(new { sss = "12" }));
            throw new RestException(HttpStatusCode.BadRequest, new { Attendance = "You cannot remove yourself as host" });
        }




        [HttpPost("testError1")]
        public ActionResult errors2([FromBody] test test)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return BadRequest(new JsonResult(new { sss = "12" }));
            //throw new RestException(HttpStatusCode.BadRequest, new { Attendance = "You cannot remove yourself as host" });
        }
    }





}

