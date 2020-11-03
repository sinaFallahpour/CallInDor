using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class ProfileCertificateTBL : BaseEntity<int>
    {
        [MaxLength(100)]
        public string  Title { get; set; }

        [MaxLength(100)]
        public string  UserName { get; set; }

        [MaxLength(2000)]
        public string  FileAddress { get; set; }
      


        #region  Relation 


        /// آیدی سرویس
        public int? ServiceId { get; set; }

        public virtual ServiceTBL Service { get; set; }

        #endregion

    }
}
