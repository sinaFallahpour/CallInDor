using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Service
{
    public class ServiceProviderResponseTypeDTO
    {
        public List<ProvideServicesDTO> ProvidesdService { get; set; }
        public double TotalPages { get; set; }
    }
}
