using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallInDoor.Config.Attributes;
using Domain;
using Domain.DTO.Questions;
using Domain.DTO.Response;
using Domain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Service.Interfaces.Question;

namespace CallInDoor.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Authorize]
    public class QuestionController : BaseControlle
    {

        #region CTOR

        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly ICommonService _commonService;
        private readonly IQuestionService _questionService;

        private IStringLocalizer<ShareResource> _localizerShared;
        public QuestionController(
                DataContext context,
                IStringLocalizer<ShareResource> localizerShared,
                IQuestionService questionService,
                IAccountService accountService,
                 ICommonService commonService

            )
        {
            _context = context;
            _questionService = questionService;
            _localizerShared = localizerShared;
            _accountService = accountService;
            _commonService = commonService;
        }


        #endregion



        #region GetAllAreaForAdmin

        [HttpGet("GetAllQuestionsForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAllQuestionsForAdmin()
        {

            try
            {
                var questions = await _context
                    .QuestionPullTBL
                   .AsNoTracking()
                   .Select(c => new
                   {
                       c.Id,
                       c.Text,
                       c.EnglishText,
                       c.Key,
                       serviceName = c.Service != null ? c.Service.Name + $"({c.Service.PersianName})" : null,
                       //c.IsEnabled,
                   }).ToListAsync();

                return Ok(_commonService.OkResponse(questions, false));

            }

            catch
            {
                return Ok();
            }
        }


        #endregion




        //#region GetById

        //[HttpGet("/api/Area/GetQuestionByIdForAdmin")]
        ////[Authorize(Roles = PublicHelper.ADMINROLE)]
        ////[ClaimsAuthorize(IsAdmin = true)]
        //[Authorize]
        //public async Task<ActionResult> GetQuestionByIdForAdmin(int Id)
        //{
        //    var service = await _context.QuestionPullTBL
        //        .Where(c => c.Id == Id).Select(c => new
        //        {
        //            c.Id,
        //            c.Text,
        //            c.EnglishText,
        //            c.Key,
        //            serviceId = c.ServiceId,
        //            //serviceName = c.Service != null ? c.Service.Name + $"({c.Service.PersianName})" : null,
        //            c.IsEnabled,
        //           c.AnswersTBLs,
        //            //serviceId = c.Service != null ? c.Service.Id : null
        //        }).FirstOrDefaultAsync();

        //    if (service == null)
        //        return NotFound(_commonService.NotFoundErrorReponse(true));
        //    return Ok(_commonService.OkResponse(service, true));
        //}

        //#endregion





        [HttpGet("GetQuestionsByIdForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetQuestionsByIdForAdmin(int Id)
        {
            var service = await _context.QuestionPullTBL
                .Where(c => c.Id == Id)
                .Select(c => new
                {
                    c.Id,
                    c.Text,
                    c.EnglishText,
                    //c.IsEnabled,
                    c.Key,

                    Answer1 = c.AnswersTBLs.Where(c => c.Key == "1").Select(c => c.Text).FirstOrDefault(),
                    //new { c.Id, c.IsEnabled, c.Key, c.Text, c.EnglishText }).ToList(),
                    AnswerEnglish1 = c.AnswersTBLs.Where(c => c.Key == "1").Select(c => c.EnglishText).FirstOrDefault(),
                    //IsEnabled1 = c.AnswersTBLs.Where(c => c.Key == "1").Select(c => c.IsEnabled).FirstOrDefault(),

                    Answer2 = c.AnswersTBLs.Where(c => c.Key == "2").Select(c => c.Text).FirstOrDefault(),
                    AnswerEnglish2 = c.AnswersTBLs.Where(c => c.Key == "2").Select(c => c.EnglishText).FirstOrDefault(),
                    //IsEnabled2 = c.AnswersTBLs.Where(c => c.Key == "2").Select(c => c.IsEnabled).FirstOrDefault(),

                    Answer3 = c.AnswersTBLs.Where(c => c.Key == "3").Select(c => c.Text).FirstOrDefault(),
                    AnswerEnglish3 = c.AnswersTBLs.Where(c => c.Key == "3").Select(c => c.EnglishText).FirstOrDefault(),
                    //IsEnabled3 = c.AnswersTBLs.Where(c => c.Key == "3").Select(c => c.IsEnabled).FirstOrDefault(),

                    serviceId = c.Service.Id,
                    serviceName = c.Service != null ? c.Service.Name + $"({c.Service.PersianName})" : null,

                    //AnswersTBLs = c.AnswersTBLs.Select(c => new { c.Id, c.IsEnabled, c.Key, c.Text, c.EnglishText }).ToList()
                    //serviceId = c.Service != null ? c.Service.Id : null
                }).FirstOrDefaultAsync();

            if (service == null)
                return NotFound(_commonService.NotFoundErrorReponse(true));
            return Ok(_commonService.OkResponse(service, true));
        }









        //[HttpGet("GetQuestionsByIdForAdmin")]
        //[Authorize(Roles = PublicHelper.ADMINROLE)]
        //[ClaimsAuthorize(IsAdmin = true)]
        //public async Task<ActionResult> GetQuestionsByIdForAdmin(int Id)
        //{
        //    var service = await _context.QuestionPullTBL
        //        .Where(c => c.Id == Id)
        //        .Select(c => new
        //        {
        //            c.Id,
        //            c.Text,
        //            c.EnglishText,
        //            c.Key,
        //            serviceId = c.Service.Id,
        //            serviceName = c.Service != null ? c.Service.Name + $"({c.Service.PersianName})" : null,
        //            c.IsEnabled,
        //            AnswersTBLs = c.AnswersTBLs.Select(c => new { c.Id, c.IsEnabled, c.Key, c.Text, c.EnglishText }).ToList()
        //            //serviceId = c.Service != null ? c.Service.Id : null
        //        }).FirstOrDefaultAsync();

        //    if (service == null)
        //        return NotFound(_commonService.NotFoundErrorReponse(true));
        //    return Ok(_commonService.OkResponse(service, true));
        //}





        //#region Create

        ///// <summary>
        /////  ایجاد Area
        ///// </summary>
        ///// <param name="CreateCategory"></param>
        ///// <returns></returns>
        //[HttpPost("CreateQuestion")]
        //[Authorize(Roles = PublicHelper.ADMINROLE)]
        //[ClaimsAuthorize(IsAdmin = true)]
        //public async Task<ActionResult> CreateQuestion([FromBody] CreateQuestionDTO model)
        //{

        //    //validate
        //    var res = await _questionService.ValidateQuestion(model);
        //    if (!res.succsseded)
        //        return BadRequest(new ApiBadRequestResponse(res.result));

        //    var Question = await _questionService.CreateQuestion(model);
        //    if (Question == null)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //                 new ApiResponse(500, PubicMessages.InternalServerMessage)
        //                         );
        //    }

        //    var question = new
        //    {
        //        Question.Id,
        //        Question.Text,
        //        Question.EnglishText,
        //        Question.IsEnabled,
        //        serviceName = Question.Service?.Name,
        //    };

        //    if (Question != null)
        //        return Ok(_commonService.OkResponse(question, PubicMessages.SuccessMessage));

        //    return StatusCode(StatusCodes.Status500InternalServerError,
        //      new ApiResponse(500, PubicMessages.InternalServerMessage)
        //    );
        //}

        //#endregion


        #region Create

        /// <summary>
        ///  ایجاد  سوال
        /// </summary>
        /// <param name="CreateCategory"></param>
        /// <returns></returns>
        [HttpPost("CreateQuestionForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> CreateQuestionForAdmin([FromBody] CreateQuestionDTO2 model)
        {

            //validate
            var res = await _questionService.ValidateQuestion(model);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            var Question = await _questionService.CreateQuestion(model);
            if (Question == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                         new ApiResponse(500, PubicMessages.InternalServerMessage)
                                 );
            }

            var response = new
            {
                Question.Id,
                //Question.Text,
                //Question.EnglishText,
                //Question.IsEnabled,
                serviceName = Question.Service?.Name,
                persianServiceName = Question.Service?.PersianName

            };



            if (Question != null)
                return Ok(_commonService.OkResponse(response, true));

            return StatusCode(StatusCodes.Status500InternalServerError,
              new ApiResponse(500, PubicMessages.InternalServerMessage)
            );
        }

        #endregion



        /// <summary>
        /// edit Questions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("UpdateQuestionsForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> UpdateQuestionsForAdmin([FromBody] UpdateQuestionDTO model)
        {

            var question = await _context.QuestionPullTBL
                .Where(c => c.Id == model.Id)
                .Include(c => c.AnswersTBLs)
                .Include(c=>c.Service)
                .FirstOrDefaultAsync();

            if (question == null)
                return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));


            question.Text = model.Text;
            question.EnglishText = model.EnglishText;
            //question.IsEnabled = model.IsEnabled;


            var answer1 = question.AnswersTBLs.Where(c => c.QuestionId == model.Id && c.Key == "1").FirstOrDefault();
            if (answer1 != null)
            {
                //answer1.IsEnabled = model.IsEnabled1;
                answer1.Text = model.Answer1;
                answer1.EnglishText = model.AnswerEnglish1;

            }

            var answer2 = question.AnswersTBLs.Where(c => c.QuestionId == model.Id && c.Key == "2").FirstOrDefault();

            if (answer2 != null)
            {
                //answer2.IsEnabled = model.IsEnabled2;
                answer2.Text = model.Answer2;
                answer2.EnglishText = model.AnswerEnglish2;
            }
            var answer3 = question.AnswersTBLs.Where(c => c.QuestionId == model.Id && c.Key == "3").FirstOrDefault();

            if (answer3 != null)
            {
                //answer3.IsEnabled = model.IsEnabled1;
                answer3.Text = model.Answer3;
                answer3.EnglishText = model.AnswerEnglish3;
            }


            var res = new
            {
                question.Id,
                //Question.Text,
                //Question.EnglishText,
                //Question.IsEnabled,
                serviceName = question.Service?.Name,
                persianServiceName= question.Service?.PersianName
            };

            try
            {
                await _context.SaveChangesAsync();
                return Ok(_commonService.OkResponse(res, PubicMessages.SuccessMessage));
            }
            catch
            {
                List<string> erroses = new List<string> { _localizerShared["InternalServerMessage"].Value.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiBadRequestResponse(erroses, 500));
            }
        }


    }
}
