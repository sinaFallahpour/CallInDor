using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    /// <summary>
    //پروفایل شرکتی است
    /// </summary>
    [Table("FirmProfile")]
    public class FirmProfileTBL
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



        /// <summary>
        /// شناسه ملی
        /// </summary>
        [MinLength(3, ErrorMessage = "please inter more than 3")]
        [MaxLength(20, ErrorMessage = "please enter  lett than 20")]
        public string FirmNationalID { get; set; }


        /// <summary>
        /// تایخ ثبت شرکت
        /// </summary>
        [MaxLength(30, ErrorMessage = "please enter  lett than 30")]
        public string FirmDateOfRegistration { get; set; }



        /// <summary>
        /// شناسه ثبت
        /// </summary>
        /// 
        [MinLength(3, ErrorMessage = "please inter more than 3")]
        [MaxLength(20, ErrorMessage = "please enter  lett than 20")]
        public string FirmRegistrationID { get; set; }






        #region relation

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }
        [Key]
        public string AppUserId { get; set; }


        ///// <summary>
        ///// دسته های سرویسی که من فعالیت میکنم توش
        ///// </summary>
        public List<FirmServiceCategoryInterInterFaceTBL> FirmServiceCategoryTBLs { get; set; }


        #endregion 



    }
}
