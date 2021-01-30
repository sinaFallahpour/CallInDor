using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Tiket
{
   public class AddChatMessageToTicketInUserDTO
    {
        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(800, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Text")]
        public string Text { get; set; }


        //public IFormFile File { get; set; }

        public int? TicketId { get; set; }

    }
}
