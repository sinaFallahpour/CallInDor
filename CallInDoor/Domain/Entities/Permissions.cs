using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Permissions")]
    public class Permissions : BaseEntity<int>
    {


        [MaxLength(200)]
        public string Title { get; set; }


        [MaxLength(400)]
        public string ActionName { get; set; }


        #region Relation


        public ICollection<Role_Permission> Role_Permissions { get; set; }

        //public string RoleId { get; set; }

        //[ForeignKey("RoleId")]
        //public AppRole AppRole { get; set; }

        #endregion



    }
}
