using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Questions
{
    public class CreateQuestionDTO2
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
        //public bool IsEnabled { get; set; }






        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer 1" )]
        public string Answer1 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name ="English Answer 1")]
        public string AnswerEnglish1  { get; set; }

        //public bool IsEnabled1  { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer 2")]
        public string Answer2 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "English Answer 2")]
        public string AnswerEnglish2 { get; set; }


        //public bool IsEnabled2 { get; set; }




        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer 3")]
        public string Answer3 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "English Answer 3")]
        public string AnswerEnglish3 { get; set; }

        //public bool IsEnabled3 { get; set; }




        //[Required(ErrorMessage = "{0} is  Required")]
        //[MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        //[MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        //[Display(Name = "Answer 4")]
        //public string Answer4 { get; set; }

        //[Required(ErrorMessage = "{0} is  Required")]
        //[MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        //[MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        //[Display(Name = "English Answer 4")]
        //public string AnswerEnglish4 { get; set; }





        #region  Relation

        /// آیدی سرویس
        public int? ServiceId { get; set; }

        #endregion

    }
}
