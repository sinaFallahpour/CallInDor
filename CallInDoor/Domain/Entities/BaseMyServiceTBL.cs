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


        /// <summary>
        /// vide or chat or service or course ,...
        /// </summary>
        public ServiceType ServiceType { get; set; }




        //public bool IsActive { get; set; }


        /// <summary>
        /// آیا این  حذف شده
        /// </summary>
        public bool IsDeleted { get; set; }

        #region  Relation

        [ForeignKey("CatId")]
        public virtual CategoryTBL CategoryTBL { get; set; }

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





        #endregion


    }
}
