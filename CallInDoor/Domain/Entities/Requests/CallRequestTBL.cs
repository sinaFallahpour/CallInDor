using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Requests
{
    /// <summary>
    ///  جدول اطلاعات اضافی یم ریکوست که مخصوص  کال هستند
    /// </summary>
    public class CallRequestTBL : BaseEntity<int>
    {

        /// <summary>
        /// زمانی که پروایدر اکسپت کرد
        /// </summary>
        public DateTime AcceptDate { get; set; }

        /// <summary>
        /// ساعت شروع سرویس
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// زمانی که انتظار میره تمام شه  سرویس گرفتن
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// زمانی که واقعا سرویس به پایان میرسد
        /// </summary>
        public DateTime? RealEndTime { get; set; }


        // relation With BaseRequestServiceTBL
        public BaseRequestServiceTBL BaseRequestServiceTBL { get; set; }



        [ForeignKey("BaseRequestServiceTBL")]
        public int? BaseRequestId { get; set; }

    }
}
