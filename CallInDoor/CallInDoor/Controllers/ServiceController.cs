using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Entities;
using Domain.DTO.Account;
using Microsoft.AspNetCore.Authorization;
using Domain.Utilities;
using Service.Interfaces.Account;
using Domain.DTO.Response;
using Microsoft.Extensions.Localization;
using Service.Interfaces.ServiceType;

namespace CallInDoor.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ServiceController : BaseControlle
    {
        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly IServiceService _servicetypeService;

        private IStringLocalizer<ShareResource> _localizerShared;

        public ServiceController(DataContext context,
             IStringLocalizer<ShareResource> localizerShared,
             IAccountService accountService,
              IServiceService servicetypeService
            )
        {
            _context = context;
            _accountService = accountService;
            _localizerShared = localizerShared;
            _servicetypeService = servicetypeService;
        }



        [HttpGet("GetServiceByIdForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> GetServiceByIdForAdmin(int Id)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var Service = await _context.ServiceTBL.Where(c => c.Id == Id).Select(c => new
            {
                c.Id,
                c.Name,
                c.PersianName,
                c.IsEnabled,
                c.Color,
            }).FirstOrDefaultAsync();

            if (Service == null)
                return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = Service,
                Message = PubicMessages.SuccessMessage
            },
            PubicMessages.SuccessMessage
           ));

        }






        // GET: api/GetAllForAdmin
        [HttpGet("GetAllForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> GetAllForAdmin()
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var AllServices = await _context
                  .ServiceTBL
                  .Select(c => new
                  {
                      c.Id,
                      c.IsEnabled,
                      c.Name,
                      c.PersianName,
                      c.Color,
                  }).ToListAsync();

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = AllServices,
                Message = PubicMessages.SuccessMessage
            },
          PubicMessages.SuccessMessage
         ));

        }





        // GET: api/GetAllService
        [HttpGet("GetAllActive")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllActive()
        {
            var services = await _servicetypeService.GetAllActive();
            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = services,
                Message = PubicMessages.SuccessMessage
            },
           PubicMessages.SuccessMessage
          ));
        }





        /// <summary>
        /// ایجاد  سرویس
        /// </summary>
        /// <param name="CreateServiceDTO"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> Create([FromBody] CreateServiceDTO model)
        {

            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var result = await _servicetypeService.Create(model);
            if (result)
            {
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = new { },
                    Message = PubicMessages.SuccessMessage
                },
                   PubicMessages.SuccessMessage
                  ));
            }

            return StatusCode(StatusCodes.Status500InternalServerError,
              new ApiResponse(500, PubicMessages.InternalServerMessage)
            );

            //return badrequest(new ApiResponse(401, _localizerShared["InvalidPhoneNumber"].Value.ToString()));

        }







        [HttpPut("UpdateServiceForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<IActionResult> UpdateServiceForAdmin([FromBody] CreateServiceDTO model)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));


            var service = _servicetypeService.GetById(model.Id);
            if (service == null)
            {
                return NotFound(new ApiResponse(404, "service " + PubicMessages.NotFoundMessage));
            }


            var result = await _servicetypeService.Update(service, model);
            if (result)
            {
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = new { },
                    Message = PubicMessages.SuccessMessage
                },
                   PubicMessages.SuccessMessage
                  ));
            }


            return StatusCode(StatusCodes.Status500InternalServerError,
              new ApiResponse(500, PubicMessages.InternalServerMessage)
            );

        }


    }
}
