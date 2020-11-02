using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.DTO.Account
{
    public class UpdateProfileDTO
    {
        //public string Username { get; set; }


        [MaxLength(80, ErrorMessage = "The maximum {0} length is {1} characters")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [MinLength(2, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(30, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }


        [MinLength(2, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(30, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }


        [Display(Name = "Bio")]
        [MinLength(8, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Bio { get; set; }


        [Display(Name = "ImageAddress")]
        public string ImageAddress { get; set; }


        [NotMapped]
        public IFormFile Image { get; set; }

        [NotMapped]
        public IFormFile Video { get; set; }



        public ICollection<FiledsDTO> Fields { get; set; }
        //public List<UserFiledsDTO> UsersDegrees { get; set; }
        //public List<int> FieldsId { get; set; }

    }






}
