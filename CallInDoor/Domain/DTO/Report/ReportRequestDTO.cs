using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Report
{
    public class ReportRequestDTO
    {

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(3, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(10000, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Text")]
        public string Text { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(10000, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "RequestId")]
        public int? RequestId { get; set; }
   
    
    
    
    }
}
