using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.CheckDiscount
{
    public class DiscountVM
    {

        public int Id { get; set; }

        public int Percent { get; set; }

        public DateTime ExpireTime { get; set; }

        public int? ServiceId { get; set; }
        
    }
}
