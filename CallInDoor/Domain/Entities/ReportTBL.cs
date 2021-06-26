using Domain.Entities.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    /// <summary>
    /// جدول ریپورت های کاربر که دام درخواست هایی که داده برا سرویس رو ریپورت کرده است
    /// </summary>
    [Table("ReportTBL")]
    public class ReportTBL : BaseEntity<int>
    {

        /// <summary>
        ///نام کاربری کسی که میخواهد ریپورت کند
        /// </summary>
        public string UserName { get; set; }

        public DateTime CreateDate { get; set; }

        /// <summary>
        /// متنی که کاربر باید وارد کند برای علت ریپورت
        /// </summary>
        public string Text { get; set; }
       
        #region  Relation

        [ForeignKey("BaseRequestServiceId")]
        public BaseRequestServiceTBL BaseRequestServiceTBL { get; set; }

        public int? BaseRequestServiceId { get; set; }


        #endregion



    }
}
