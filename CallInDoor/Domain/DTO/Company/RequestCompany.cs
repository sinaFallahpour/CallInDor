using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Company
{
    public class RequestCompany
    {
        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "ServiceCategoryId")]
        public int? ServiceCategoryId { get; set; }
    }
}