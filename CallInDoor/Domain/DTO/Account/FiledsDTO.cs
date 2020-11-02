using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Account
{
    public class FiledsDTO
    {
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        public string Title { get; set; }
        public DegreeType DegreeType { get; set; }
    }
}
