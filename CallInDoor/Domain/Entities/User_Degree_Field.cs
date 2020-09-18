using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("User_Degree_Field")]
    public class User_Degree_Field : BaseEntity<int>
    {


        #region Relation



        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }




        public string DegreeName { get; set; }
        public string DegreePersianName { get; set; }

        public int? DegreeId { get; set; }

        [ForeignKey("DegreeId")]
        public virtual DegreeTBL DegreeTBL { get; set; }





        public string FieldName { get; set; }
        public string FieldPersianName { get; set; }

        public int? FieldId { get; set; }

        [ForeignKey("FieldId")]
        public virtual FieldTBL FieldTBL { get; set; }

        #endregion

    }
}
