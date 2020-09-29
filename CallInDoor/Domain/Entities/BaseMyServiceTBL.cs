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



        /// <summary>
        /// وضعیت تایید سرویس بوسیله ادمین
        /// </summary>
        public ConfirmedServiceType ConfirmedServiceType { get; set; }


        /// <summary>
        /// vide or chat or service or course ,...
        /// </summary>
        public ServiceType ServiceType { get; set; }



        #region  Relation

        /// <summary>
        /// 1 به 1 لا جدول MyChatService
        /// </summary>

        public virtual MyChatServiceTBL MyChatsService { get; set; }


        /// <summary>
        /// رابطه 1 به 1 با حدول سرویس هایی از نوع سرویس
        /// </summary>
        public virtual MyServiceServiceTBL MyServicesService { get; set; }



        public virtual ServiceTBL ServiceTbl { get; set; }

        [ForeignKey("ServiceTbl")]
        public int? ServiceId { get; set; }





        #endregion


    }
}
