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
    /// </summary>
    [Table("ChatServiceMessages")]
    public class ChatServiceMessagesTBL : BaseEntity<int>
    {

        /// <summary>
        /// یوزر نیم فرسنده پیام
        /// </summary>
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
        [MaxLength(40)]
        public string ProviderUserName { get; set; }



        /// <summary>
        /// نام کاربری مشتری یا سرویس گیرنده
        /// </summary>
        [MaxLength(40)]
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
        /// قیمت هر پیامی که میفرستد کاربر(چه نیتو باشد و چه غیر نیتو)
        /// </summary>
        public double? Price { get; set; }


        /// <summary>
        /// آیا پیام خوانده شده ؟
        /// </summary>
        public bool IsSeen { get; set; }


        #region  Relation 


        ////////public int? ServiceRequestId { get; set; }

        ////////[ForeignKey("ServiceRequestId")]
        ////////public ServiceRequestTBL ServiceRequestTBL { get; set; }



        #region  rabete 3 gani

        //public int? BaseServiceRequestId { get; set; }

        //[ForeignKey("BaseServiceRequestId")]
        //public BaseRequestServiceTBL BaseRequestServiceTBL { get; set; }


        //public int? ChatRequestId { get; set; }

        //[ForeignKey("ChatRequestId")]
        //public ChatRequestTBL ChatRequestTBL { get; set; }



        #endregion

        #endregion


    }

}
