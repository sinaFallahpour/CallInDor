using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Domain.Entities
{
    /// <summary>
    /// جدول ذخیره کاربرانی که در یک  سرویس خاص عضو یک کمپانی هستند
    /// </summary>
    [Table("CompanyServiceUser")]
    public class CompanyServiceUserTBL : BaseEntity<int>
    {


        public DateTimeOffset CreateDate { get; set; }



        #region 



        /// <summary>
        ///نام کاربری کاربری که در این سرویس عضو شده است
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// نام کاربری کمپانی 
        /// </summary>
        public string CompanyUserName { get; set; }



        [ForeignKey("ServiceId")]
        public ServiceTBL ServiceTBL { get; set; }
        public int? ServiceId { get; set; }




        #endregion

    }
}
