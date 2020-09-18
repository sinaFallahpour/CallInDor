using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Account
{
    public class RefreshDTO
    {
        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(10, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(10, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(20, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "CountryCode")]
        public string CountryCode { get; set; }




    }
}
