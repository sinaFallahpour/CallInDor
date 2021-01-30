using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("ServiceComments")]
    public class ServiceCommentsTBL : BaseEntity<int>
    {

        [MaxLength(120)]
        public string UserName { get; set; }


        [MaxLength(400)]
        public string Comment { get; set; }

        public DateTime CreateDate { get; set; }


        [MaxLength(400)]
        public string ResonForUnder3Star { get; set; }


        public int StarCount { get; set; }


        public bool IsConfirmed { get; set; }


        #region  Relation 

        /// آیدی سرویس
        public int? BaseMyServiceId { get; set; }

        [ForeignKey("BaseMyServiceId")]
        public BaseMyServiceTBL BaseMyService { get; set; }

        #endregion



    }
}
