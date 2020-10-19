using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Account
{
   public class UpdateAdminDTO
    {

        public string Id { get; set; }
    

        [Required(ErrorMessage = "{0} is  Required")]
        [EmailAddress(ErrorMessage = "InValid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(200, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(200, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        public string RoleId { get; set; }





    }
}
