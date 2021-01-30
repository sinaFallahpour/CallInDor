using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Account
{
    public class AddCardDTO
    {

        public int Id  { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Card Name")]
        public string CardName { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(16, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(16, ErrorMessage = "The maximum {0} length is {1} characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} must be number")]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

    }
}
