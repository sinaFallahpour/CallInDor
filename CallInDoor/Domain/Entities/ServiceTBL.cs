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
        /// آیا سرویس هایی که کاربران در این حوضه ثبت میکنند مدرک فرستادن الزامیست؟
        /// </summary>
        public bool IsProfileOptional { get; set; }




        /// <summary>
        /// /حداقل فیمت برای برای  سرویس  تایپ هایی  از نوع سرویس
        /// </summary>
        public double MinPriceForService { get; set; }




        /// <summary>
        /// حداقل زمان برای سرویس های Voice//VideCall//voiceCall
        /// </summary>
        public double MinSessionTime { get; set; }


        /// <summary>
        /// حداقل قیمت مجاز برای کاربران تیتیو برای سرویس های چت یا وویس یا ویدیو
        /// </summary>
        public double AcceptedMinPriceForNative { get; set; }

        /// <summary>
        /// حداقل قیمت مجاز برای کاربران غیر تیتیو برای سرویس های چت یا وویس یا ویدیو
        /// </summary>
        public double AcceptedMinPriceForNonNative { get; set; }

        /// <summary>
        /// آیا فعال است
        /// </summary>
        public bool IsEnabled { get; set; }



        /// <summary>
        /// درصد پولی که به ادمین میرسد
        /// برای این سرویس خواص
        /// </summary>
        public int SitePercent { get; set; }



        #region  Relation
        public List<FirmServiceCategoryInterInterFaceTBL> FirmServiceCategoryTBLs { get; set; }

        public virtual ICollection<CategoryTBL> Categories { get; set; }

        public virtual ICollection<ServiceTagsTBL> Tags { get; set; }

        public virtual ICollection<BaseMyServiceTBL> BaseMyServices { get; set; }


        public string RoleId { get; set; }


        [ForeignKey("RoleId")]
        public AppRole AppRole { get; set; }






        public List<ServidceTypeRequiredCertificatesTBL> ServidceTypeRequiredCertificatesTBL { get; set; }
        public List<User_TopTenPackageTBL> User_TopTenPackageTBL { get; set; }
        public List<TopTenPackageTBL> TopTenPackageTBL { get; set; }



        //public List<ServiceCommentsTBL> ServiceCommentsTBLs { get; set; }


        #endregion
    }
}
