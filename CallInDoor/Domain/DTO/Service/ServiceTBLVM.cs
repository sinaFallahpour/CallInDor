using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Service
{
    public class ServiceTBLVM
    {
        public int Id { get; set; }

        public bool IsProfileOptional { get; set; }
        
        public double AcceptedMinPriceForNative { get; set; }

        public double AcceptedMinPriceForNonNative { get; set; }

        public string Name { get; set; }


        public double  MinPriceForService { get; set; }
    }
}
