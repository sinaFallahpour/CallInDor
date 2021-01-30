using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Account
{
    public class ConfirmProfileDTO
    {
        
        public string UserName { get; set; }

        //public int? FleId { get; set; }

        public int? ServiceId { get; set; }

        //[Required(ErrorMessage = "{0} is  Required")]
        //[Display(Name = "reson for reject")]
        public string ResonForReject { get; set; }

        public bool IsConfirmed { get; set; }


    }
}
