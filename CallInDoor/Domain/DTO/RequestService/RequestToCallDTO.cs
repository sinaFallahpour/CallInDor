using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.RequestService
{
    public class RequestToCallDTO
    {
        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(1000, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "First Message")]
        public string FirstMessage { get; set; }


        #region

        public int? BaseServiceId { get; set; }

        #endregion

    }
}
