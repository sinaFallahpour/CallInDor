using Domain.Entities.Requests;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    /// <summary>
    ///جدول کل چت هایه سرویس هایی که ازنوه چت وویس از نوع سشن یا پ=ریودیک هستند را ذخیره میکند
    /// </summary>
    [Table("ChatForLimitedServiceMessagesTBL")]
    public class ChatForLimitedServiceMessagesTBL : BaseEntity<int>
    {

        public string SenderUserName { get; set; }


        /// <summary>
        /// متن پیام 
        /// </summary>
        public string Text { get; set; }



        public ChatMessageType ChatMessageType { get; set; }
        /// اگرا ازنوع چت نباشداین برای وویس و فایل مقدار میگیرد
        public string FileOrVoiceAddress { get; set; }




        /// <summary>
        /// نام کاربری سرویس دهنده
        /// </summary>
        [MaxLength(100)]
        public string ProviderUserName { get; set; }



        /// <summary>
        /// نام کاربری مشتری یا سرویس گیرنده
        /// </summary>
        [MaxLength(100)]
        public string ClientUserName { get; set; }


        /// <summary>
        ///آیا پروایدر فرستاد
        /// </summary>
        public bool IsProviderSend { get; set; }


        /// <summary>
        /// چه کسی پیام را فرستاد کاینت یا پرووایدر
        /// </summary>
        public SendetMesageType SendetMesageType { get; set; }



        public DateTime CreateDate { get; set; }





        /// <summary>
        /// آیا پیام خوانده شده ؟
        /// </summary>
        public bool IsSeen { get; set; }



        #region  Relation 


        //public int? ServiceRequestId { get; set; }

        //[ForeignKey("ServiceRequestId")]
        //public ServiceRequestTBL ServiceRequestTBL { get; set; }
















        #region   Rabete 3 Gani
        public int? BaseServiceRequestId { get; set; }

        [ForeignKey("BaseServiceRequestId")]
        public BaseRequestServiceTBL BaseRequestServiceTBL { get; set; }


        public int? ChatRequestId { get; set; }

        [ForeignKey("ChatRequestId")]
        public ChatRequestTBL ChatRequestTBL { get; set; }
        #endregion


        #endregion


    }
}
