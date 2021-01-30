using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    ///این جدول تیکت های کاربر به ادمین وادمین به کاربر است
    [Table("Tiket")]
    public class TiketTBL : BaseEntity<int>
    {

        public string  Title { get; set; }

        /// <summary>
        /// اینیوزر نیم کاربری است که تیکت را به ادمین ارسال کرد
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// نوع تیکت
        /// فوری یاعادی یا جهت اطلاع
        /// </summary>
        public PriorityStatus PriorityStatus { get; set; }


        public DateTime CreateDate { get; set; }

        /// <summary>
        /// بازه یا بسته
        /// </summary>
        public TiketStatus TiketStatus { get; set; }

        public DateTime UserLastUpdateDate { get; set; }

        public DateTime AdminLastUpdateDate { get; set; }

        /// <summary>
        /// ایا کاربر پیام جدید فرستاد که ادمین نخواند
        /// </summary>
        public bool IsUserSendNewMessgae { get; set; }

        /// <summary>
        ///ایا ادمین پیام جدید فرستاد که یوزر نخوانده
        /// </summary>
        public bool IsAdminSendNewMessgae { get; set; }


        #region Relation
        public List<TiketMessagesTBL> TiketMessagesTBL { get; set; }
        #endregion

    }

}
