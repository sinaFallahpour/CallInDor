using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("BaseMyService")]
    public class BaseMyServiceTBL : BaseEntity<int>
    {

        [MaxLength(200)]
        public string ServiceName { get; set; }




        /// <summary>
        /// لت جغرافیایی برای یافتن موفعیت جغرافیایی طرف
        /// </summary>
        public double? Latitude { get; set; }

        /// لانگ جغرافیایی برای یافتن موفعیت جغرافیایی طرف
        public double? Longitude { get; set; }



        /// <summary>
        /// آیا سرویس هایی که کاربران در این حوضه ثبت میکنند مدرک فرستادن الزامیست؟
        /// این پراپرتی با جدول serviceTbl ریدانتدنتی دارد
        /// </summary>
        public bool IsProfileOptional { get; set; }




        /// <summary>
        ///  تاریخ درج
        /// </summary>
        public DateTime CreateDate { get; set; }

        [MaxLength(100)]
        public string UserName { get; set; }




        //آیا سرویس کاربرر قابلیت ادیت کردن را دارد یا نه
        //بعد تایید ادمیناین مقدارش فالز میشه
        public bool IsEditableService { get; set; }

        ////////////////////////////public bool IsEditableProfile { get; set; }


        /// <summary>
        /// وضعیت تایید سرویس بوسیله ادمین
        /// </summary>
        public ConfirmedServiceType ConfirmedServiceType { get; set; }

        public string RejectReason { get; set; }



        public ProfileConfirmType ProfileConfirmType { get; set; }
        public string ProfileRejectReson { get; set; }







        /// <summary>
        /// video or voice  or chat or course or service  shayad ham  karpardaz
        //value should be 0,1,2,3,4,5
        /// </summary>
        public string ServiceTypes { get; set; }











        /// <summary>
        /// vide or chat or service or course ,...
        //#TODO --> should be delete  
        /// </summary>
        public ServiceType ServiceType { get; set; }






        //public bool IsActive { get; set; }


        /// <summary>
        /// آیا این  حذف شده
        /// </summary>
        public bool IsDeleted { get; set; }





        /// <summary>
        /// آیا این سرویسی توسط کمپانی ایی که در ان عضو است 
        /// disabled شده؟؟
        /// </summary>
        public bool IsDisabledByCompany { get; set; }





        /// <summary>
        /// تعداد کل ستاره هایی که این سرویس گرفت
        /// </summary>
        public int StarCount { get; set; }





        /// <summary>
        /// تعداد کل ستار های زیر 3 که این سرویس دارد  
        /// </summary>
        public int Under3StarCount { get; set; }





        #region  Relation





        /// <summary>
        /// شرکتی که این سرویس را disable کرد
        /// </summary>
        [ForeignKey("CompanyId")]
        public AppUser Company { get; set; }
        public string CompanyId { get; set; }




        [ForeignKey("CatId")]
        public CategoryTBL CategoryTBL { get; set; }

        public int? CatId { get; set; }


        [ForeignKey("SubCatId")]
        public virtual CategoryTBL SubCategoryTBL { get; set; }

        public int? SubCatId { get; set; }


        /// <summary>
        /// 1 به 1 لا جدول MyChatService
        /// </summary>
        public MyChatServiceTBL MyChatsService { get; set; }

        /// <summary>
        /// رابطه 1 به 1 با حدول سرویس هایی از نوع سرویس
        /// </summary>
        public MyServiceServiceTBL MyServicesService { get; set; }

        /// <summary>
        /// رابطه 1 به 1 با حدول سرویس هایی از نوع کورس
        /// </summary>
        public MyCourseServiceTBL MyCourseService { get; set; }


        public ServiceTBL ServiceTbl { get; set; }

        [ForeignKey("ServiceTbl")]
        public int? ServiceId { get; set; }



        public List<ServiceCommentsTBL> ServiceCommentsTBLs { get; set; }

        public List<ServiceRequestTBL> ServiceRequestTBLs { get; set; }


        #endregion


    }
}
