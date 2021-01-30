using Domain.Enums;
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
        [EmailAddress(ErrorMessage = "Invalid email address")]
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



        [NotMapped]
        public IFormFile Image { get; set; }

        [NotMapped]
        public IFormFile Video { get; set; }


        public Gender Gender { get; set; }



        [MinLength(10, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(10, ErrorMessage = "The maximum {0} length is {1} characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} must be numeric")]
        [Display(Name = "NationalCode")]
        public string NationalCode { get; set; }

        [MaxLength(40)]
        public string BirthDate { get; set; }

        public List<FiledsDTO> Fields { get; set; }

        public List<RequiredFile> RequiredFile { get; set; }

    }


    public class RequiredFile
    {

        public int? ServiceId { get; set; }
        public int FileId { get; set; }
        public bool AddNew { get; set; }
        //public string FileAddress { get; set; }
        public IFormFile File { get; set; }
        //public List<RequireCertitications> RequiredCertificate { get; set; }
    }



}
