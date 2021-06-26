using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    /// <summary>
    /// جدول واسط بین بین کاربر و کد تخفیف هایی که تا حالا استفاده کرده است
    /// </summary>
    [Table("DiscountUsedByUser")]
    public class DiscountUsedByUserTBL : BaseEntity<int>
    {

        [MaxLength(100, ErrorMessage = "username is too long")]
        public string UserName { get; set; }


        public DateTime CreateDate { get; set; }

        #region  relation

        public int? CheckDiscountId { get; set; }
        [ForeignKey("CheckDiscountId")]
        public CheckDiscountTBL CheckDiscountTBL { get; set; }

        #endregion



    }
}
