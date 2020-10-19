using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Account
{
    public class CreateServiceDTO
    {

        public int Id { get; set; }



        [Required(ErrorMessage = "{0} is resquired")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Name { get; set; }





        [Required(ErrorMessage = "{0} is resquired")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string PersianName { get; set; }





        /// <summary>
        /// رنگ سرویس
        /// </summary>
        [Required(ErrorMessage = "{0} is resquired")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Color { get; set; }




        /// <summary>
        /// حداقل فیمت برای برای  سرویس  تایپ هایی  از نوع سرویس
        /// </summary>
        [Required(ErrorMessage = "{0} is resquired")]
        [Range(1, double.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2}")]
        [Display(Name = "Minimm Price For Service")]
        public double MinPriceForService { get; set; }




        /// <summary>
        /// حداقل زمان برای سرویس های Voice//VideCall//voiceCall
        /// </summary>
        [Required(ErrorMessage = "{0} is resquired")]
        [Range(1, double.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2} minutes")]
        [Display(Name = "Minimm Session Time")]
        public double MinSessionTime { get; set; }






        /// <summary>
        /// حداقل قیمت مجاز برای کاربران تیتیو برای سرویس های چت یا وویس یا ویدیو
        /// </summary>
        [Required(ErrorMessage = "{0} is resquired")]
        [Range(1, double.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2}  ")]
        [Display(Name = "Accepted Minimm Price For Native user")]
        public double AcceptedMinPriceForNative { get; set; }






        /// <summary>
        /// حداقل قیمت مجاز برای کاربران غیر تیتیو برای سرویس های چت یا وویس یا ویدیو
        /// </summary>
        [Required(ErrorMessage = "{0} is resquired")]
        [Range(1, double.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2}  ")]
        [Display(Name = "Accepted Minimm Price For Non Native user")]
        public double AcceptedMinPriceForNonNative { get; set; }






        /// <summary>
        /// آیا فعال است
        /// </summary>
        public bool IsEnabled { get; set; }



        //[MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(2000, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Tags { get; set; }


        //[MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(2000, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string PersinaTags { get; set; }




        [Required(ErrorMessage = "{0} is resquired")]
        public string RoleId { get; set; }


    }
}
