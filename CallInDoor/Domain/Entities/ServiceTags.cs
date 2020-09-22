using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("ServiceTags")]
    public class ServiceTags : BaseEntity<int>
    {

        public string  TagName { get; set; }
        


        
        #region relation

        /// <summary>
        /// آیدی سرویس
        /// </summary>
        public int? ServiceId { get; set; }
        public virtual ServiceTBL Service { get; set; }


        #endregion
    }
}
