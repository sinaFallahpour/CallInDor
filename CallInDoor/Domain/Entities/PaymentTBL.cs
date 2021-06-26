using Domain.DTO.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    /// <summary>
    /// جدول پیمنت های کاربران برای شارژ کیف پولشان است
    /// </summary>
    [Table("Payment")]
    public class PaymentTBL : BaseEntity<int>
    {

        public double Amount { get; set; }


        ///////// <summary>
        ///////// درصد تخفیف
        ///////// </summary>
        //////public int DiscountPercent { get; set; }
        //public decimal FinallyAmountWithTax { get; set; }



        public bool IsSucceed { get; set; }
        public string InvoiceKey { get; set; }
        public string TransactionCode { get; set; }
        public DateTime Date { get; set; }
        public string TrackingNumber { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorCode { get; set; }

        //public bool IsImmeditely { get; set; }
        //public  TransactionTBL  Transactions { get; set; }


        [ForeignKey("TransactionId")]
        public TransactionTBL TransactionTBL { get; set; }
        public int? TransactionId { get; set; }




        [MaxLength(120, ErrorMessage = "username is too long")]
        public string UserName { get; set; }





        #region relation


        //////////////[ForeignKey("CheckDiscountID")]
        //////////////public CheckDiscountTBL CheckDiscountTBL { get; set; }

        //////////////public int? CheckDiscountID { get; set; }

        #endregion


        //public User User { get; set; }

        //public int? PlanId { get; set; }
        //public Plan Plan { get; set; }


        //[ForeignKey("RefrenceTransationId")]
        //public RefrenceTransation RefrenceTransations { get; set; }
        //public int? RefrenceTransationId { get; set; }


    }
}
