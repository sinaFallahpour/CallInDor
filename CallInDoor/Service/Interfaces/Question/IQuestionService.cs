using Domain.DTO.Questions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Question
{
    public interface IQuestionService
    {
        Task<QuestionPullTBL> CreateQuestion(CreateQuestionDTO2 model);

        Task<(bool succsseded, List<string> result)> ValidateQuestion(CreateQuestionDTO2 model);

    }
}
