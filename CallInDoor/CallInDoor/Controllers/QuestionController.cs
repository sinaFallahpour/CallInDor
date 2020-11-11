using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallInDoor.Config.Attributes;
using Domain;
using Domain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Common;

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
        private IStringLocalizer<ShareResource> _localizerShared;
        public QuestionController(
                DataContext context,
                IStringLocalizer<ShareResource> localizerShared,
                 IAccountService accountService,
                 ICommonService commonService
            )
        {
            _context = context;
            _localizerShared = localizerShared;
            _accountService = accountService;
            _commonService = commonService;
        }


        #endregion







        [HttpGet("GetQuestions")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetQuestions()
        {
            var questions = await _context.QuestionPullTBL.ToListAsync();

            //var questionsFromDB = new SettingsGetDTO()
            //{
            //    Aboutus = settings.Where(c => c.Key == PublicHelper.AboutUsKeyName).SingleOrDefault().Value,
            //    AboutusEnglish = settings.Where(c => c.Key == PublicHelper.AboutUsKeyName).SingleOrDefault().EnglishValue,

            //    Address = settings.Where(c => c.Key == PublicHelper.AddressKeyName).SingleOrDefault().Value,
            //    AddressEnglish = settings.Where(c => c.Key == PublicHelper.AddressKeyName).SingleOrDefault().EnglishValue,

            //    Email = settings.Where(c => c.Key == PublicHelper.EmailKeyName).SingleOrDefault().Value,
            //    EmailEnglish = settings.Where(c => c.Key == PublicHelper.EmailKeyName).SingleOrDefault().EnglishValue,

            //    PhoneNumber = settings.Where(c => c.Key == PublicHelper.PhoneNumberKeyName).SingleOrDefault().Value,
            //    PhoneNumberEnglish = settings.Where(c => c.Key == PublicHelper.PhoneNumberKeyName).SingleOrDefault().EnglishValue,
            //};


            var questionsFromDB = questions.Select(c => new
            {
                question1 = new
                {
                    question = questions.Where(c => c.Key == PublicHelper.Question1KeyName)
                    .Select(c => new
                    {
                        questionText = c.Text,
                        questionEnglishText = c.EnglishText,
                        answers = c.AnswersTBLs.Select(v => new { v.Text, v.EnglishText })
                    }).FirstOrDefault(),
                },

                question2 = new
                {
                    question = questions.Where(c => c.Key == PublicHelper.Question1KeyName)
                    .Select(c => new
                    {
                        questionText = c.Text,
                        questionEnglishText = c.EnglishText,
                        answers = c.AnswersTBLs.Select(v => new { v.Text, v.EnglishText })
                    }).FirstOrDefault(),
                },

                 question3 = new
                 {
                     question = questions.Where(c => c.Key == PublicHelper.Question1KeyName)
                    .Select(c => new
                    {
                        questionText = c.Text,
                        questionEnglishText = c.EnglishText,
                        answers = c.AnswersTBLs.Select(v => new { v.Text, v.EnglishText })
                    }).FirstOrDefault(),
                 },

                question4 = new
                {
                    question = questions.Where(c => c.Key == PublicHelper.Question1KeyName)
                    .Select(c => new
                    {
                        questionText = c.Text,
                        questionEnglishText = c.EnglishText,
                        answers = c.AnswersTBLs.Select(v => new { v.Text, v.EnglishText })
                    }).FirstOrDefault(),
                },

                question5 = new
                {
                    question = questions.Where(c => c.Key == PublicHelper.Question1KeyName)
                    .Select(c => new
                    {
                        questionText = c.Text,
                        questionEnglishText = c.EnglishText,
                        answers = c.AnswersTBLs.Select(v => new { v.Text, v.EnglishText })
                    }).FirstOrDefault(),
                }
            }); ;
            return Ok(_commonService.OkResponse(questionsFromDB, PubicMessages.SuccessMessage));
        }







    }
}
