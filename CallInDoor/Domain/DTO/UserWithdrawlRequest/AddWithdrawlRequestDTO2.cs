using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.UserWithdrawlRequest
{
    public class AddWithdrawlRequestDTO2
    {


        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "Amount")]
        public double?  Amount { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "card")]
        public int? CardId { get; set; }
    


    }
}
