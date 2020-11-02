using Domain.Enums;
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
        //public string PersianTitle { get; set; }

        ///   مدرک
        public DegreeType DegreeType { get; set; }

        #region Relation
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        #endregion


    }
}
