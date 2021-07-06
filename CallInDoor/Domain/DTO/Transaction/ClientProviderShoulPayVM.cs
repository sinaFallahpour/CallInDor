using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Transaction
{
    public class ClientProviderShoulPayVM
    {
        /// <summary>
        /// کلاینت باید بپردازد
        /// </summary>
        public double ClientShouldPay { get; set; }

        /// <summary>
        /// پولی مه پروایدر میگیره
        /// </summary>
        public double ProviderShouldGet { get; set; }

    }


    public class ClientShoulPayVM
    {
        /// <summary>
        /// کلاینت باید بپردازد
        /// </summary>
        public double ClientShouldPay { get; set; }

    }

}
