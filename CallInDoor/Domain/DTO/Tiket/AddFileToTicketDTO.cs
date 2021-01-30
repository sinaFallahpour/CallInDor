using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Tiket
{
    public class AddFileToTicketDTO
    {
        public IFormFile File { get; set; }
        public int? TicketId { get; set; }
    }
}
