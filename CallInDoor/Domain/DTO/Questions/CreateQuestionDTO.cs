using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Questions
{
    public class CreateQuestionDTO
    {
        public int? Id { get; set; }

        //public string Key { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Text { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string EnglishText { get; set; }
        public bool IsEnabled { get; set; }

        #region  Relation
        public List<Answer> Answers { get; set; }

        /// آیدی سرویس
        public int? ServiceId { get; set; }

        #endregion

    }


    public class Answer
    {

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Persian Name")]
        public string PersianName { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "English Name")]
        public string EnglishName { get; set; }


        public bool IsEnabled { get; set; }
    }



}
