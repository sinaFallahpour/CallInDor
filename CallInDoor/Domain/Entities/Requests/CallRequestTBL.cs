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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RealEndTime { get; set; }





        // relation With BaseRequestServiceTBL
        public BaseRequestServiceTBL BaseRequestServiceTBL { get; set; }
       
        
        [ForeignKey("BaseRequestServiceTBL")]
        public int? BaseRequestId { get; set; }




    }
}
