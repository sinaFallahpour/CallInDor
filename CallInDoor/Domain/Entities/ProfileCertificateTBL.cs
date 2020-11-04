using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{


    /// <summary>
    ///مدارکی که کاربر برای هر حوضه انتخاب کرده اینجاست
    /// </summary>
    [Table("ProfileCertificate")]
    public class ProfileCertificateTBL : BaseEntity<int>
    {
        //[MaxLength(100)]
        //public string  Title { get; set; }

        [MaxLength(100)]
        public string  UserName { get; set; }

        //این فایل ها فط باید عکس یا پی دی اف باشند
        [MaxLength(2000)]
        public string  FileAddress { get; set; }


        //////public string key { get; set; }


        #region  Relation 

        /// آیدی سرویس
        public int? ServiceId { get; set; }

        public  ServiceTBL Service { get; set; }

        #endregion

    }
}
