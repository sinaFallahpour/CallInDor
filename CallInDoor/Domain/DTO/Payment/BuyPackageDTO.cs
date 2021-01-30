using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Payment
{
    public class BuyPackageDTO
    {


        //public int BaseServiceId { get; set; }



        public int RequestId  { get; set; }


        /// <summary>
        /// کد تخفیف
        /// </summary>
        public string DisCountCode { get; set; }


        /// <summary>
        /// آیا با کیف پول است 
        /// </summary>
        //public bool IsByWallet  { get; set; }




    }
}
