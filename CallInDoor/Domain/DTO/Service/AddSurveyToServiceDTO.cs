using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Service
{
    public class AddSurveyToServiceDTO
    {

        #region realation
        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "Service")]
        public int? ServiceId { get; set; }

        public List<Questoin> Questoins { get; set; }

        #endregion


    }



    public class Questoin
    {

        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "Question")]
        public int? QuestionId { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "Answer")]
        public int? AnswerId { get; set; }

    }

}
