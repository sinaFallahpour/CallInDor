using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Tiket
{
    public class AddTicketDTO
    {
        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(800, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Title")]
        public string Title { get; set; }


        public PriorityStatus PriorityStatus { get; set; }




        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(3000, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Text")]
        public string StartText { get; set; }



    }
}
