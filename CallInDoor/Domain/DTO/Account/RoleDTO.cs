using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Account
{
    public class RoleDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "{0} is  Required", AllowEmptyStrings = false)]
        [Display(Name = "Access level")]
        public string Name { get; set; }
    }
}
