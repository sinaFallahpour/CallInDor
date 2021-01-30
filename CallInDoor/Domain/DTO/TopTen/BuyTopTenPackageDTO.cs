using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.TopTen
{
    public class BuyTopTenPackageDTO
    {


        //public int RequestId { get; set; }



        /// <summary>
        /// کد تخفیف
        /// </summary>
        public string DisCountCode { get; set; }


        //آیدی  TopTenPackageTBL
        public int PackageId { get; set; }

        ///// <summary>
        ///// آیدی ServiceTBL
        ///// </summary>
        //public int ServiceId { get; set; }



    }
}
