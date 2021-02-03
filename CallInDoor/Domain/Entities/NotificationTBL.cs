using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class NotificationTBL : BaseEntity<int>
    {

        /// <summary>
        ///نام کاربری کسی که باید بهش نوتیف برود
        /// </summary>
        public string UserName { get; set; }





        /// <summary>
        /// نام کاربری کسی که نوتیف  میگیرد
        /// </summary>
        public string SenderUserName { get; set; }

        public string TextPersian { get; set; }

        public string EnglishText { get; set; }

        public DateTimeOffset CreateDate { get; set; }
        
        public bool IsReaded { get; set; }


        /// <summary>
        /// نوع نوتیف
        /// </summary>
        public NotificationStatus NotificationStatus { get; set; }





    }
}
