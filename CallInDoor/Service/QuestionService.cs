using Domain;
using Domain.DTO.Questions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class QuestionService : IQuestionService
    {
        private readonly DataContext _context;

        public QuestionService(DataContext context)
        {
            _context = context;
        }











        ///// CreateQuestion
        //public async Task<QuestionPullTBL> CreateQuestion(CreateQuestionDTO model)
        //{
        //    if (model == null) return null;

        //    var question = new QuestionPullTBL()
        //    {
        //        Text = model.Text,
        //        EnglishText = model.EnglishText,
        //        IsEnabled = model.IsEnabled,
        //        ServiceId = model.ServiceId,
        //    };

        //    question.AnswersTBLs = new List<AnswerTBL>();

        //    if (model.Answers != null)
        //    {
        //        foreach (var item in model.Answers)
        //        {
        //            var answer = new AnswerTBL()
        //            {
        //                Text = item.EnglishName,
        //                EnglishText = item.EnglishName,
        //                IsEnabled = item.IsEnabled,
        //                QuestionPullTBL = question,
        //            };
        //            question.AnswersTBLs.Add(answer);
        //        }
        //    }

        //    try
        //    {
        //        var area = await _context.QuestionPullTBL.AddAsync(question);
        //        await _context.SaveChangesAsync();
        //        return question;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}






        /// CreateQuestion
        public async Task<QuestionPullTBL> CreateQuestion(CreateQuestionDTO2 model)
        {
            if (model == null) return null;

            var question = new QuestionPullTBL()
            {
                Text = model.Text,
                EnglishText = model.EnglishText,
                //IsEnabled = model.IsEnabled,
                ServiceId = model.ServiceId,
            };

            var answers = new List<AnswerTBL>() {

              new AnswerTBL(){
                  EnglishText=model.AnswerEnglish1,
                  //IsEnabled=model.IsEnabled1,
                  Text=model.Answer1,
                  QuestionPullTBL=question,
                  Key="1"
              },
              new AnswerTBL(){
                  EnglishText=model.AnswerEnglish2,
                  //IsEnabled=model.IsEnabled2,
                  Text=model.Answer2,
                  QuestionPullTBL=question,
                  Key="2"

              },
              new AnswerTBL(){
                  EnglishText=model.AnswerEnglish1,
                  //IsEnabled=model.IsEnabled1,
                  Text=model.Answer1,
                  QuestionPullTBL=question,
                  Key="3"
              },

            };

            question.AnswersTBLs = answers;

            try
            {
                var area = await _context.QuestionPullTBL.AddAsync(question);
                await _context.SaveChangesAsync();
                return question;
            }
            catch
            {
                return null;
            }
        }





        ///// <summary>
        ///// ولیدیت کردن آبجکت  چت سرویس
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public async Task<(bool succsseded, List<string> result)> ValidateQuestion(CreateQuestionDTO model)
        //{
        //    bool IsValid = true;
        //    List<string> Errors = new List<string>();


        //    var isTextExist = await _context
        //        .QuestionPullTBL
        //         .AnyAsync(c => c.EnglishText == model.EnglishText && c.ServiceId ==model.ServiceId );
        //    if (isTextExist)
        //    {
        //        IsValid = false;
        //        Errors.Add($"{model.Text} already exist");
        //    }

        //    var isPersianTextExist = await _context.AreaTBL.AnyAsync(c => c.PersianTitle == model.Text);
        //    if (isPersianTextExist)
        //    {
        //        IsValid = false;
        //        Errors.Add($"{model.Text} already exist");
        //    }

        //    if (model.Answers == null)
        //    {
        //        IsValid = false;
        //        Errors.Add("At least one answer is required");
        //    }

        //    if (model.ServiceId == null)
        //    {
        //        IsValid = false;
        //        var errors = new List<string>();
        //        Errors.Add("service Is required");
        //    }


        //    //validate serviceTypes
        //    var serviceFromDb = await _context
        //    .ServiceTBL
        //    .Where(c => c.Id == model.ServiceId)
        //    .FirstOrDefaultAsync();

        //    if (serviceFromDb == null)
        //    {
        //        IsValid = false;
        //        Errors.Add("service not exist");
        //    }

        //    return (IsValid, Errors);
        //}




        /// <summary>
        /// ولیدیت کردن آبجکت  چت سرویس
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool succsseded, List<string> result)> ValidateQuestion(CreateQuestionDTO2 model)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();


            var isEnglishTextExist = await _context
                .QuestionPullTBL
                 .AnyAsync(c => c.ServiceId == model.ServiceId);
            if (isEnglishTextExist)
            {
                IsValid = false;
                Errors.Add($"question for this service already exist");
            }

            //validate serviceTypes
            var serviceFromDb = await _context
            .ServiceTBL
            .Where(c => c.Id == model.ServiceId)
            .FirstOrDefaultAsync();

            if (serviceFromDb == null)
            {
                IsValid = false;
                Errors.Add("service not exist");
            }
            return (IsValid, Errors);
        }



    }
}
