using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallInDoor.Config.Attributes;
using Domain;
using Domain.DTO.Questions;
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
            var answers = await _context.AnswerTBL.ToListAsync();

            var response = new QuestionAnswersDTO
            {
                Question1 = questions.Where(c => c.Key == PublicHelper.Question1KeyName).FirstOrDefault().Text,
                Question1English = questions.Where(c => c.Key == PublicHelper.Question1KeyName).FirstOrDefault().EnglishText,

                Question2 = questions.Where(c => c.Key == PublicHelper.Question2KeyName).FirstOrDefault().Text,
                Question2English = questions.Where(c => c.Key == PublicHelper.Question2KeyName).FirstOrDefault().EnglishText,

                Question3 = questions.Where(c => c.Key == PublicHelper.Question3KeyName).FirstOrDefault().Text,
                Question3English = questions.Where(c => c.Key == PublicHelper.Question3KeyName).FirstOrDefault().EnglishText,

                Question4 = questions.Where(c => c.Key == PublicHelper.Question4KeyName).FirstOrDefault().Text,
                Question4English = questions.Where(c => c.Key == PublicHelper.Question4KeyName).FirstOrDefault().EnglishText,

                Question5 = questions.Where(c => c.Key == PublicHelper.Question5KeyName).FirstOrDefault().Text,
                Question5English = questions.Where(c => c.Key == PublicHelper.Question5KeyName).FirstOrDefault().EnglishText,

                Answer10 = answers.Where(c => c.Key == PublicHelper.Answer10KeyName).FirstOrDefault().Text,
                Answer11 = answers.Where(c => c.Key == PublicHelper.Answer11KeyName).FirstOrDefault().Text,
                Answer12 = answers.Where(c => c.Key == PublicHelper.Answer12KeyName).FirstOrDefault().Text,
                Answer13 = answers.Where(c => c.Key == PublicHelper.Answer13KeyName).FirstOrDefault().Text,

                Answer20 = answers.Where(c => c.Key == PublicHelper.Answer20KeyName).FirstOrDefault().Text,
                Answer21 = answers.Where(c => c.Key == PublicHelper.Answer21KeyName).FirstOrDefault().Text,
                Answer22 = answers.Where(c => c.Key == PublicHelper.Answer22KeyName).FirstOrDefault().Text,
                Answer23 = answers.Where(c => c.Key == PublicHelper.Answer23KeyName).FirstOrDefault().Text,

                Answer30 = answers.Where(c => c.Key == PublicHelper.Answer30KeyName).FirstOrDefault().Text,
                Answer31 = answers.Where(c => c.Key == PublicHelper.Answer31KeyName).FirstOrDefault().Text,
                Answer32 = answers.Where(c => c.Key == PublicHelper.Answer32KeyName).FirstOrDefault().Text,
                Answer33 = answers.Where(c => c.Key == PublicHelper.Answer33KeyName).FirstOrDefault().Text,

                Answer40 = answers.Where(c => c.Key == PublicHelper.Answer40KeyName).FirstOrDefault().Text,
                Answer41 = answers.Where(c => c.Key == PublicHelper.Answer41KeyName).FirstOrDefault().Text,
                Answer42 = answers.Where(c => c.Key == PublicHelper.Answer42KeyName).FirstOrDefault().Text,
                Answer43 = answers.Where(c => c.Key == PublicHelper.Answer43KeyName).FirstOrDefault().Text,

                Answer50 = answers.Where(c => c.Key == PublicHelper.Answer50KeyName).FirstOrDefault().Text,
                Answer51 = answers.Where(c => c.Key == PublicHelper.Answer51KeyName).FirstOrDefault().Text,
                Answer52 = answers.Where(c => c.Key == PublicHelper.Answer52KeyName).FirstOrDefault().Text,
                Answer53 = answers.Where(c => c.Key == PublicHelper.Answer53KeyName).FirstOrDefault().Text,
            };
            return Ok(_commonService.OkResponse(response, PubicMessages.SuccessMessage));
        }





        /// <summary>
        /// edit Questions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("UpdateQuestions")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> UpdateQuestions(QuestionAnswersDTO model)
        {
            var questions = await _context.QuestionPullTBL.ToListAsync();
            var answers = await _context.AnswerTBL.ToListAsync();


            var Question1 = questions.Where(c => c.Key == PublicHelper.Question1KeyName).FirstOrDefault();
            if (Question1 != null)
            {
                Question1.Text = model.Question1;
                Question1.EnglishText = model.Question1English;
            }

            var Question2 = questions.Where(c => c.Key == PublicHelper.Question2KeyName).FirstOrDefault();
            if (Question2 != null)
            {
                Question2.Text = model.Question2;
                Question2.EnglishText = model.Question2English;
            }
            var Question3 = questions.Where(c => c.Key == PublicHelper.Question3KeyName).FirstOrDefault();
            if (Question3 != null)
            {
                Question3.Text = model.Question3;
                Question3.EnglishText = model.Question3English;
            }

            var Question4 = questions.Where(c => c.Key == PublicHelper.Question4KeyName).FirstOrDefault();
            if (Question4 != null)
            {
                Question4.Text = model.Question4;
                Question4.EnglishText = model.Question4English;
            }
            var Question5 = questions.Where(c => c.Key == PublicHelper.Question5KeyName).FirstOrDefault();
            if (Question5 != null)
            {
                Question5.Text = model.Question5;
                Question5.EnglishText = model.Question5English;
            }


            var Answer10 = answers.Where(c => c.Key == PublicHelper.Answer10KeyName).FirstOrDefault();
            if (Answer10 != null)
            {
                Answer10.Text = model.Answer10;
                Answer10.EnglishText = model.Answer10;
            }

            var Answer11 = answers.Where(c => c.Key == PublicHelper.Answer11KeyName).FirstOrDefault();
            if (Answer11 != null)
            {
                Answer11.Text = model.Answer11;
                Answer11.EnglishText = model.Answer11;
            }


            var Answer12 = answers.Where(c => c.Key == PublicHelper.Answer12KeyName).FirstOrDefault();
            if (Answer12 != null)
            {
                Answer12.Text = model.Answer12;
                Answer12.EnglishText = model.Answer12;
            }
            var Answer13 = answers.Where(c => c.Key == PublicHelper.Answer13KeyName).FirstOrDefault();
            if (Answer13 != null)
            {
                Answer13.Text = model.Answer10;
                Answer13.EnglishText = model.Answer10;
            }
            var Answer20 = answers.Where(c => c.Key == PublicHelper.Answer20KeyName).FirstOrDefault();
            if (Answer20 != null)
            {
                Answer20.Text = model.Answer20;
                Answer20.EnglishText = model.Answer20;
            }


            var Answer21 = answers.Where(c => c.Key == PublicHelper.Answer21KeyName).FirstOrDefault();
            if (Answer21 != null)
            {
                Answer21.Text = model.Answer21;
                Answer21.EnglishText = model.Answer10;
            }
            var Answer22 = answers.Where(c => c.Key == PublicHelper.Answer22KeyName).FirstOrDefault();
            if (Answer22 != null)
            {
                Answer22.Text = model.Answer22;
                Answer22.EnglishText = model.Answer22;
            }
            var Answer23 = answers.Where(c => c.Key == PublicHelper.Answer23KeyName).FirstOrDefault();
            if (Answer23 != null)
            {
                Answer23.Text = model.Answer23;
                Answer23.EnglishText = model.Answer23;
            }
            var Answer30 = answers.Where(c => c.Key == PublicHelper.Answer30KeyName).FirstOrDefault();
            if (Answer30 != null)
            {
                Answer30.Text = model.Answer30;
                Answer30.EnglishText = model.Answer30;
            }
            var Answer31 = answers.Where(c => c.Key == PublicHelper.Answer31KeyName).FirstOrDefault();
            if (Answer31 != null)
            {
                Answer31.Text = model.Answer31;
                Answer31.EnglishText = model.Answer31;
            }
            var Answer32 = answers.Where(c => c.Key == PublicHelper.Answer32KeyName).FirstOrDefault();
            if (Answer32 != null)
            {
                Answer32.Text = model.Answer32;
                Answer32.EnglishText = model.Answer32;
            }
            var Answer33 = answers.Where(c => c.Key == PublicHelper.Answer33KeyName).FirstOrDefault();
            if (Answer33 != null)
            {
                Answer33.Text = model.Answer33;
                Answer33.EnglishText = model.Answer33;
            }
            var Answer40 = answers.Where(c => c.Key == PublicHelper.Answer40KeyName).FirstOrDefault();
            if (Answer40 != null)
            {
                Answer40.Text = model.Answer40;
                Answer40.EnglishText = model.Answer40;
            }
            var Answer41 = answers.Where(c => c.Key == PublicHelper.Answer41KeyName).FirstOrDefault();
            if (Answer41 != null)
            {
                Answer41.Text = model.Answer41;
                Answer41.EnglishText = model.Answer41;
            }

            var Answer42 = answers.Where(c => c.Key == PublicHelper.Answer42KeyName).FirstOrDefault();
            if (Answer42 != null)
            {
                Answer42.Text = model.Answer42;
                Answer42.EnglishText = model.Answer42;
            }

            var Answer43 = answers.Where(c => c.Key == PublicHelper.Answer43KeyName).FirstOrDefault();
            if (Answer43 != null)
            {
                Answer43.Text = model.Answer43;
                Answer43.EnglishText = model.Answer43;
            }

            var Answer50 = answers.Where(c => c.Key == PublicHelper.Answer50KeyName).FirstOrDefault();
            if (Answer50 != null)
            {
                Answer50.Text = model.Answer50;
                Answer50.EnglishText = model.Answer50;
            }

            var Answer51 = answers.Where(c => c.Key == PublicHelper.Answer51KeyName).FirstOrDefault();
            if (Answer51 != null)
            {
                Answer51.Text = model.Answer51;
                Answer51.EnglishText = model.Answer51;
            }


            var Answer52 = answers.Where(c => c.Key == PublicHelper.Answer52KeyName).FirstOrDefault();
            if (Answer52 != null)
            {
                Answer52.Text = model.Answer52;
                Answer52.EnglishText = model.Answer52;
            }

            var Answer53 = answers.Where(c => c.Key == PublicHelper.Answer53KeyName).FirstOrDefault();
            if (Answer53 != null)
            {
                Answer53.Text = model.Answer53;
                Answer53.EnglishText = model.Answer53;
            }

            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, PubicMessages.SuccessMessage));

            //return Ok(_commonService.OkResponse(settingsFromDB, PubicMessages.SuccessMessage));
        }









    }
}
