using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    [Table("FirmServiceCategoryInterInterFace")]
    public class FirmServiceCategoryInterInterFaceTBL : BaseEntity<int>
    {




        #region relation

        [ForeignKey("ServiceTBLId")]
        public ServiceTBL ServiceTBL { get; set; }
        public int? ServiceTBLId { get; set; }





        [ForeignKey("FirmProfileTBLId")]
        public FirmProfileTBL FirmProfileTBL { get; set; }
        public string FirmProfileTBLId { get; set; }


        #endregion


    }
}
