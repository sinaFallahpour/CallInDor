using Domain.DTO.Tiket;
using Domain.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Service.Interfaces.Ticket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TicketService : ITicketService
    {

        private readonly IHostingEnvironment _hostingEnvironment;


        public TicketService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        public (bool succsseded, List<string> result) ValidateTicketFile(AddFileToTicketDTO model)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();

            if (model.File == null)
            {
                IsValid = false;
                Errors.Add("File Is required");
                return (IsValid, Errors);
            }
            else
            {
                if (!model.File.IsValidExtention())
                {
                    IsValid = false;
                    Errors.Add("Invalid file");
                    return (IsValid, Errors);

                }
                if (model.File.Length > 3000000)
                {
                    IsValid = false;
                    Errors.Add("file is too large");
                    return (IsValid, Errors);
                }
            }
            return (IsValid, Errors);

        }









        public async Task<string> SaveFileToHost(string path, string lastPath, IFormFile file)
        {

            //try
            //{
            string uniqueVideoFileName = null;
            if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
                _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, path);
            uniqueVideoFileName = (Guid.NewGuid().ToString().GetImgUrlFriendly() + "_" + file.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueVideoFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //Delete LastImage Image
            if (!string.IsNullOrEmpty(lastPath))
            {
                //var LastVideoPath = lastPath?.Substring(1);
                var LastPath = Path.Combine(_hostingEnvironment.WebRootPath, lastPath);
                if (System.IO.File.Exists(LastPath))
                {
                    System.IO.File.Delete(LastPath);
                }
            }
            //update Newe video Address To database
            //user.VideoAddress = "/Upload/User/" + uniqueVideoFileName;

            return path + uniqueVideoFileName;

            //}
            //catch
            //{
            //    return null;
            //}
        }







    }
}
