using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Account
{
    public class AddDepositDTO
    {

        //مبلغ تراکنش شده
        [Required(ErrorMessage = "{0} is  Required")]
        [Range(0, double.MaxValue, ErrorMessage = "The {0} range is valid")]
        [Display(Name = "Amount")]
        public double? Amount { get; set; }

    }
}
