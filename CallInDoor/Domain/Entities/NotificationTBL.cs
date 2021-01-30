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

        public string  SenderUserName{ get; set; }

        public string TextPersian { get; set; }

        public string EnglishText { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsReaded { get; set; }

        public NotificationStatus NotificationStatus { get; set; }

    }
}
