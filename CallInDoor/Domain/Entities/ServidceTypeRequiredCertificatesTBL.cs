using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("ServidceTypeRequiredCertificates")]
    public class ServidceTypeRequiredCertificatesTBL : BaseEntity<int>
    {
        public string FileName { get; set; }
        public string  PersianFileName { get; set; }

        public bool Isdeleted { get; set; }

        #region  Relation 

        /// آیدی سرویس
        public int? ServiceId { get; set; }
        public ServiceTBL Service { get; set; }


        #endregion
    }
}
