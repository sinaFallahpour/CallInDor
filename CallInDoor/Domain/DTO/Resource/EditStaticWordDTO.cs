using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Resource
{
    public class EditStaticWordDTO
    {


        [Required(ErrorMessage = "{0} is  Required")]
        public string StaticWord1 { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        public string StaticWord2 { get; set; }
    }



    public class EditStaticWordDTO2 : EditStaticWordDTO
    {
        /// <summary>
        /// این نوع زبان را معلوم میکند
        /// </summary>
        [Required(ErrorMessage = "{0} is  Required")]
        public LanguageHeader LanguageHeader { get; set; }
    }
}
