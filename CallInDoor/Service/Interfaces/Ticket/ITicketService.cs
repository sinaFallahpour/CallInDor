using Domain.DTO.Tiket;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Ticket
{
    public interface ITicketService
    {
        (bool succsseded, List<string> result) ValidateTicketFile(AddFileToTicketDTO model);
         Task<string> SaveFileToHost(string path, string lastPath, IFormFile file);


    }
}
