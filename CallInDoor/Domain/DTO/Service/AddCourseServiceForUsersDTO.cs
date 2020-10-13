using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Service
{
    public class AddCourseServiceForUsersDTO
    {
        public int? Id { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(30, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(200, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "ServiceName")]
        public string ServiceName { get; set; }




        [Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(1000, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }



        //public string Category { get; set; }


        [MaxLength(200, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "NewCategory")]
        public string NewCategory { get; set; }


        [MaxLength(200, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "TotalLenght")]
        public string TotalLenght { get; set; }



        [Range(0, double.MaxValue, ErrorMessage = "The {0} range is valid")]
        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "Price")]
        public double? Price { get; set; }


        [Range(0, 100, ErrorMessage = "The {0} range is valid")]
        [Display(Name = "DisCountPercent")]
        public int DisCountPercent { get; set; }



        ///// <summary>
        ///// آدرس فیلم Preview
        ///// </summary>
        //public string PreviewVideoAddress { get; set; }

        /// <summary>
        /// فایل preview
        /// </summary>
        public IFormFile PreviewFile { get; set; }


        #region Relation


        /// <summary>
        /// لیست تاپیک های یک کورس
        /// </summary>
        public ICollection<AddCourseTopic> Topics { get; set; }

        [Display(Name = "Category")]
        public int? CatId { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "Service")]
        public int? ServiceId { get; set; }

        #endregion




    }
}
