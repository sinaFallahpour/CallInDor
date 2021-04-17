using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallInDoor.Config.Attributes;
using Domain;
using Domain.DTO.Account;
using Domain.DTO.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Service.Interfaces.Resource;

namespace CallInDoor.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CardController : BaseControlle
    {

        #region ctor


        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;

        private readonly IAccountService _accountService;
        private readonly ICommonService _commonService;

        private IStringLocalizer<CardController> _localizer;
        private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IResourceServices _resourceServices;

        public CardController(

        UserManager<AppUser> userManager,
            DataContext context,
                   IAccountService accountService,
                   ICommonService commonService,
               IStringLocalizer<CardController> localizer,
                IStringLocalizer<ShareResource> localizerShared,
                IResourceServices resourceServices
            )
        {
            _context = context;
            _userManager = userManager;
            _accountService = accountService;
            _commonService = commonService;
            _localizer = localizer;
            _localizerShared = localizerShared;
            _resourceServices = resourceServices;
        }

        #endregion ctor



        [HttpGet("GetWalletInventory")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetWalletInventory()
        {
            if (!User.Identity.IsAuthenticated)
            {
                List<string> erros = new List<string> { 
                    //_localizerShared["UnauthorizedMessage"].Value.ToString() ,
                    _resourceServices.GetErrorMessageByKey("UnauthorizedMessage")
            };
                return Unauthorized(new ApiBadRequestResponse(erros, 401));
            }

            var currentusername = _accountService.GetCurrentUserName();

            AppUser userFromDB = await _userManager.FindByNameAsync(currentusername);
            if (userFromDB == null)
            {
                List<string> erros = new List<string> { 
                    //_localizerShared["UnauthorizedMessage"].Value.ToString() 
                _resourceServices.GetErrorMessageByKey("UnauthorizedMessage")
                };
                return Unauthorized(new ApiBadRequestResponse(erros, 401));
            }

            return Ok(_commonService.OkResponse(userFromDB.WalletBalance, false));
        }








        [HttpGet("GetAllCards")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetAllCards()
        {
            var currentusername = _accountService.GetCurrentUserName();

            var cards = await _context.CardTBL.AsNoTracking()
                .Where(c => c.Username == currentusername && c.IsDeleted == false)
                .ToListAsync();
            return Ok(_commonService.OkResponse(cards, false));
        }








        [HttpGet("GetCardById")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetCardById(int Id)
        {
            var cardFromDB = await _context.CardTBL.AsNoTracking()
                .Where(c => c.Id == Id)
                .FirstOrDefaultAsync();

            if (cardFromDB == null)
                return NotFound(_commonService.NotFoundErrorReponse(false));

            return Ok(_commonService.OkResponse(cardFromDB, false));
        }





        /// <summary>
        /// افزودن یک کارت 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddCard")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> AddCard([FromBody] AddCardDTO model)
        {

            var currentUsername = _accountService.GetCurrentUserName();
            var cardFromDB = await _context.CardTBL.AnyAsync(c => c.CardName.ToLower() == model.CardName.ToLower() && c.Username == currentUsername);
            if (cardFromDB)
            {
                var errors = new List<string>();
                //errors.Add(_localizerShared["card name already exist"].Value.ToString());
                errors.Add(_resourceServices.GetErrorMessageByKey("card name already exist"));
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            var card = new CardTBL()
            {
                Username = currentUsername,
                CardName = model.CardName,
                CardNumber = model.CardNumber,
                IsDeleted = false,
            };

            try
            {
                await _context.CardTBL.AddAsync(card);
                await _context.SaveChangesAsync();
                return Ok(_commonService.OkResponse(card.Id, false));
            }
            catch
            {
                List<string> erros = new List<string> { 
                    //_localizerShared["InternalServerMessage"].Value.ToString() 
                     _resourceServices.GetErrorMessageByKey("InternalServerMessage")

                };
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new ApiBadRequestResponse(erros, 500));
            }
        }





        [HttpPut("UpdateCard")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> UpdateCard([FromBody] AddCardDTO model)
        {
            var currentUsername = _accountService.GetCurrentUserName();
            var cardFromDBExist = await _context.CardTBL.AnyAsync(c => c.Id != model.Id && c.CardName.ToLower() == model.CardName.ToLower() && c.Username == currentUsername);
            if (cardFromDBExist)
            {
                var errors = new List<string>();
                //errors.Add(_localizerShared["card name already exist"].Value.ToString());
                errors.Add(_resourceServices.GetErrorMessageByKey("card name already exist"));
                return BadRequest(new ApiBadRequestResponse(errors));
            }


            var cardFromDB = await _context.CardTBL.Where(c => c.Id == model.Id && c.IsDeleted == false).FirstOrDefaultAsync();
            if (cardFromDB == null)
                return NotFound(_commonService.NotFoundErrorReponse(false));

            cardFromDB.CardName = model.CardName;
            cardFromDB.CardName = model.CardNumber;


            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, false));

        }





        [HttpDelete("DeleteCard")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> DeleteCard(int id)
        {
            var currentUsername = _accountService.GetCurrentUserName();
            var cardFromDB = await _context.CardTBL.Where(c => c.Id == id && c.IsDeleted == false).FirstOrDefaultAsync();
            if (cardFromDB == null)
                return NotFound(_commonService.NotFoundErrorReponse(false));

            cardFromDB.IsDeleted = true;
            
            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, false));
        }





    }
}
