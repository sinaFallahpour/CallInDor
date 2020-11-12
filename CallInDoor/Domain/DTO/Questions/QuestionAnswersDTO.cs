using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Questions
{
    public class QuestionAnswersDTO
    {

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Question1 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Question1 (English)")]
        public string Question1English { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Question2 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Question2 (English)")]
        public string Question2English { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Question3 { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Question3 (English)")]
        public string Question3English { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Question4 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Question4 (English)")]
        public string Question4English { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Question5 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Question4 (English)")]
        public string Question5English { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer10 { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer10 (English)")]
        public string Answer10English { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer11 { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer11 (English)")]
        public string Answer11English { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer12 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer12 (English)")]
        public string Answer12English { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer13 { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer13 (English)")]
        public string Answer13English { get; set; }




      




        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer20 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer20 (English)")]
        public string Answer20English { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer21 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer21 (English)")]
        public string Answer21English { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer22 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer22 (English)")]
        public string Answer22English { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer23 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer23 (English)")]
        public string Answer23English { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer30 { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer30 (English)")]
        public string Answer30English { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer31 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer31 (English)")]
        public string Answer31English { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer32 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer32 (English)")]
        public string Answer32English { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer33 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer33 (English)")]
        public string Answer33English { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer40 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer40 (English)")]
        public string Answer40English { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer41 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer41 (English)")]
        public string Answer41English { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer42 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer42 (English)")]
        public string Answer42English { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer43 { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer43 (English)")]
        public string Answer43English { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer50 { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer50 (English)")]
        public string Answer50English { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer51 { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer51 (English)")]
        public string Answer51English { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer52 { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer52 (English)")]
        public string Answer52English { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Answer53 { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Answer53 (English)")]
        public string Answer53English { get; set; }

    }
}
