using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Speciality")]
    public class SpecialityTBL : BaseEntity<int>
    {
        public string PersianName { get; set; }

        public string EnglishName { get; set; }


        #region  Relation 

        /// <summary>
        /// آیدی Area
        /// </summary>
        public int? AreatId { get; set; }

        [ForeignKey("AreatId")]
        public AreaTBL Area { get; set; }

        #endregion

    }
}
