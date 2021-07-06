using Domain.Entities.Requests;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    /// <summary>
    ///  پول هایی که برای در خواست هایی که به پروایدر ها میدهیم 
    ///  از کیف پولمان کم میشود وتا زمانی که سرویس نگرفتیم به پروایدر اضافه نمیشود
    ///  وممکن است پروایدر آن را برگرداند
    /// </summary>
    [Table("BlockMony")]
    public class BlockMonyTBL : BaseEntity<int>
    {
        /// <summary>
        /// قیمت اولیه سرویس بدون در نظر گرفتن کد تخفیف
        /// </summary>
        public double Price { get; set; }
        public string ClientUsername { get; set; }



        /// <summary>
        /// قیمت نهایی که با کد تخفیف از کیف پول کلاینت کم شد
        /// </summary>
        public double FinalPrice { get; set; }




        public string ProviderUsername { get; set; }

        public DateTime CreayteDate { get; set; }

        public BlockMonyStatus BlockMonyStatus { get; set; }






        #region 


        public BaseRequestServiceTBL BaseRequestServiceTBL { get; set; }
        [ForeignKey("BaseRequestServiceTBL")]
        public int? BaseRequestId { get; set; }






        /// <summary>
        ///  کد تخفیف  که کلاینت برای این درخواست استفاده میکند
        /// </summary>
        [ForeignKey("CheckDiscountId")]
        public CheckDiscountTBL CheckDiscountTBL { get; set; }
        public int? CheckDiscountId { get; set; }


        #endregion
    }
}
