using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Tiket
{
    public class GetAllTiketsDTO
    {
        
        public int? Page { get; set; }

        public int? PerPage { get; set; }

        public string    Title{ get; set; }

        public DateTime? CreateDate  { get; set; }

        public TiketStatus? TiketStatus { get; set; }


    }
}
