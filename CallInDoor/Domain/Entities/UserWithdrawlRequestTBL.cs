using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    /// <summary>
    /// جدول در خواست هایه کاربر برای برداشت پول
    /// </summary>
    [Table("UserWithdrawlRequest")]
    public class UserWithdrawlRequestTBL : BaseEntity<int>
    {

        public double Amount { get; set; }

        public DateTime CreateDate { get; set; }


        /// <summary>
        /// وضعیت درخواست
        /// </summary>
        public WithdrawlRequestStatus WithdrawlRequestStatus { get; set; }


        /// <summary>
        /// علت رد درخواست
        /// </summary>
        public string ResonOfReject { get; set; }



        /// <summary>
        /// زمان رد یا تایید سرویس
        /// </summary>
        public DateTime RejectOrConfirmTime { get; set; }


        #region Relation


        public string UserName { get; set; }



        public int? CardItId { get; set; }

        [ForeignKey("CardItId")]
        public CardTBL CardTBL { get; set; }


        #endregion 
    }
}
