using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Service
{
    public class AddCommentServiceDTO
    {

        [MaxLength(300, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "Service")]
        public int? ServiceId { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [Range(0, 5, ErrorMessage = "The  {0} should be between {1} and {2}")]
        [Display(Name = "StarCount")]
        public int StarCount { get; set; }


        [MaxLength(300, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "ResonForUnder3Star")]
        public string ResonForUnder3Star { get; set; }

    }
}
