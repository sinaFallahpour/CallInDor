using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Notification
{
    public class SendNotificationVM
    {

        public string UserName { get; set; }

        //public string SenderUserName { get; set; }

        public string  Text { get; set; }

        //public string TextPersian { get; set; }

        //public string EnglishText { get; set; }

        //public DateTimeOffset CreateDate { get; set; }

        //public bool IsReaded { get; set; }


        /// <summary>
        /// نوع نوتیف
        /// </summary>
        //public NotificationStatus NotificationStatus { get; set; }

    }
}
