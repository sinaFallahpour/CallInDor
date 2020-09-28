using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class User_ServiceTBL : BaseEntity<int>
    {




        #region Relation

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public string UserId { get; set; }
        
        
        

        #endregion 

    }
}
