using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Service
{
    public class AddServiceServiceForUsersDTO
    {

        public int? Id { get; set; }

        //[Required(ErrorMessage = "{0} is  Required")]
        //[MaxLength(30, ErrorMessage = "The maximum {0} length is {1} characters")]
        //[Display(Name = "UserName")]
        //public string UserName { get; set; }


        //[Required(ErrorMessage = "{0} is  Required")]
        [Range(-90, +90, ErrorMessage = "The  {0} should be between {1} and {2}")]
        [Display(Name = "Latitude")]
        public double? Latitude { get; set; }



        //[Required(ErrorMessage = "{0} is  Required")]
        [Range(-180, +180, ErrorMessage = "The  {0} should be between {1} and {2}")]
        [Display(Name = "Longitude")]
        public double? Longitude { get; set; }






        [Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(200, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "ServiceName")]
        public string ServiceName { get; set; }




        #region  دیفالت بین همشونه

        /// <summary>
        /// vide or chat or seice or course ,...
        /// </summary>
        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "ServiceType")]
        public ServiceType? ServiceType { get; set; }



        //public bool IsActive { get; set; }
      
        #endregion




        [Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [MinLength(10, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }


        /// <summary>
        /// این فیلد فقط و فقط مخصوص سرویس هایی است که از نوع ترنسلیت هستند
        /// </summary>
        public bool BeTranslate { get; set; }


        //public string Speciality { get; set; }
        //public string Area { get; set; }


        public bool FileNeeded { get; set; }


        //[Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        //[MinLength(10, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "FileDescription")]
        public string FileDescription { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [Range(0, int.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2}")]
        [Display(Name = "Price")]
        public double? Price { get; set; }


        /// <summary>
        /// برآورد زمان تحویل کار
        /// </summary>
        [Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [MinLength(6, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "WorkDeliveryTimeEstimation")]
        public string WorkDeliveryTimeEstimation { get; set; }


        /// <summary>
        /// چطور کار میکند
        /// </summary>
        [Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [MinLength(10, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "HowWorkConducts")]
        public string HowWorkConducts { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        [MinLength(10, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "DeliveryItems")]
        public string DeliveryItems { get; set; }



        /// <summary>
        /// مجموع تگ هایی که  سیستم معرفی میکند و تگ های که خودش وارد میکند
        /// </summary>
      
        [MaxLength(1000, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Tags")]
        public string Tags { get; set; }


        [MaxLength(1000, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "CustomTags")]
        public string  CustomTags { get; set; }


        #region Relation

        public int? CatId { get; set; }

        public int? SubCatId { get; set; }


  
        public int? AreaId { get; set; }
 
        public int? SpecialityId { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "Service")]
        public int? ServiceId { get; set; }

        #endregion 

    }
}
