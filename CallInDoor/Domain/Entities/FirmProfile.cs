using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class FirmProfile
    {

        [MaxLength(240, ErrorMessage = "firm name is required")]
        public string FirmName { get; set; }


        [MaxLength(240, ErrorMessage = "firm manager name  is required")]
        public string FirmManagerName { get; set; }



        public string FirmLogo { get; set; }



        //[MaxLength(240, ErrorMessage = "firm manager name  is required")]
        //public string  { get; set; }

        [MaxLength(200, ErrorMessage = "code posti  should be 200 character")]
        public string NationalCode { get; set; }



        [MaxLength(200, ErrorMessage = "code posti  should be 200 character")]
        public string CodePosti { get; set; }


        public string FirmAddress { get; set; }

        [MaxLength(200, ErrorMessage = "firm country should be 200 character")]
        public string FirmCountry { get; set; }

        [MaxLength(200, ErrorMessage = "Firm state should be 200 character")]
        public string FirmState { get; set; }


        [Key, ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }



    }
}
