using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Degree")]
    public class DegreeTBL : BaseEntity<int>
    {
        public string Title { get; set; }
        public string PersianTitle { get; set; }




        #region Relation

        //public ICollection<FieldTBL> Fields { get; set; }
       
        #endregion
    }
}
