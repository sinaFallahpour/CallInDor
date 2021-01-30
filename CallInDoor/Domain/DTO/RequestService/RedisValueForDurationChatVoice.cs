using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.RequestService
{

    //ذخیره اطلاعاتی که در redi mire
    public class RedisValueForDurationChatVoice
    {

        public RedisValueForDurationChatVoice()
        {
            Chats = new List<ChatsVM>();
        }

        /// <summary>
        /// زمان پایان
        /// </summary>
        public DateTime EndTime { get; set; }

        public DateTime StartTime { get; set; }


        public RequestStatusForRedis RequestStatusForRedis { get; set; }



        public List<ChatsVM> Chats { get; set; }
    }


    public class ChatsVM
    {
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
        public string ProviderUserName { get; set; }

        /// <summary>
        /// نام کاربری مشتری یا سرویس گیرنده
        /// </summary>
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


        #region  Relation 

        public int? ServiceRequestId { get; set; }

        #endregion




    }
}
