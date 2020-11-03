using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Account
{
   public class RegisterAdminDTO
    {


        public string  Id { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(10, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(10, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [EmailAddress(ErrorMessage = "InValid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(6, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(20, ErrorMessage = "The maximum {0} length is {1} characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(20, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "CountryCode")]
        public string CountryCode { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(200, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(200, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string LastName { get; set; }

        public bool IsCompany { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        public string  RoleId { get; set; }




    }
}
