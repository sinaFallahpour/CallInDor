using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Service
{
    public class ResponseDTO
    {


        public List<SearchResponseDTO> Users  { get; set; }
        public  double TotalPages { get; set; }

        
    }
}
