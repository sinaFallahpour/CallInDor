using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Role_Permission")]
    public class Role_Permission : BaseEntity<int>
    {


        #region relation
        public int? PermissionId { get; set; }

        [ForeignKey("PermissionId")]
        public Permissions Permissions { get; set; }



        public string RoleId { get; set; }

        [ForeignKey("RoleId")]
        public AppRole AppRole { get; set; }
        #endregion
    }
}
