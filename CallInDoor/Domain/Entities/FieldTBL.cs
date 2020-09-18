using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Field")]
    public class FieldTBL : BaseEntity<int>
    {
        public string Title { get; set; }
        public string PersianTitle { get; set; }


        #region Relation
        public int? DegreeId { get; set; }

        [ForeignKey("DegreeId")]
        public virtual DegreeTBL DegreeTBL { get; set; }
        #endregion
    
    
    }
}
