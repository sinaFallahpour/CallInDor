using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{


    /// <summary>
    ///جدول واسط کاربر و تاپ تن
    /// </summary>
    [Table("User-TopTenPackage")]
    public class User_TopTenPackageTBL : BaseEntity<int>
    {

        /// <summary>
        /// کاربری که خریده
        /// </summary>
        public string UserName { get; set; }



        /// <summary>
        /// زمانی که کاربرخریده پکیج را 
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// زمان اکسپایر شدن بسته
        /// </summary>
        public DateTime ExpireTime { get; set; }



        #region Reallation


        /// <summary>
        /// رابطه 1-1
        /// </summary>
        //[ForeignKey("TransactionTBLId")]
        public TransactionTBL TransactionTBL { get; set; }
        //public int? TransactionTBLId { get; set; }





        /// <summary>
        /// رابطه 1-1
        /// </summary>
        [ForeignKey("TopTenPackageId")]
        public TopTenPackageTBL TopTenPackageTBL { get; set; }
        public int? TopTenPackageId { get; set; }




        public ServiceTBL ServiceTbl { get; set; }
        [ForeignKey("ServiceTbl")]
        public int? ServiceId { get; set; }





        #endregion



    }
}
