using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Service")]
    public class ServiceTBL : BaseEntity<int>
    {


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
        /// /حداقل فیمت برای برای  سرویس  تایپ هایی  از نوع سرویس
        /// </summary>
        public double MinPriceForService { get; set; }




        /// <summary>
        /// حداقل زمان برای سرویس های Voice//VideCall//voiceCall
        /// </summary>
        public double MinSessionTime { get; set; }





        /// <summary>
        /// آیا فعال است
        /// </summary>
        public bool IsEnabled { get; set; }





        #region  Relation

        public virtual ICollection<CategoryTBL> Categories { get; set; }

        public virtual ICollection<ServiceTags> Tags { get; set; }
        #endregion 
    }
}
