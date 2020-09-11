using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.test
{
    public class test
    {

        [Required(ErrorMessage ="{0} is required")]
        //[Display(Name = "username")]
        [Display(Name  = "username")]

        public string  username  { get; set; }




    }
}
