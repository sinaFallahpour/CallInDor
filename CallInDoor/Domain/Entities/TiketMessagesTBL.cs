using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    ///جدول پیامهای یک تیکت 
    [Table("TiketMessages")]
    public class TiketMessagesTBL : BaseEntity<int>
    {

        /// <summary>
        /// ایا ادمین فرستاد ؟
        /// </summary>
        public bool IsAdmin { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsFile { get; set; }
        public string FileAddress { get; set; }

        #region relation
        /// آیدی تیکت
        public int? TiketId { get; set; }

        [ForeignKey("TiketId")]
        public TiketTBL TiketTBL { get; set; }

        #endregion


    }
}
