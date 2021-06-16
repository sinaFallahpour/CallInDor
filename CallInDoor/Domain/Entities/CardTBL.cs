using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    //این جدول کل کارت های کاربر است
    [Table("Card")]
    public class CardTBL : BaseEntity<int>
    {
        public string Username { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<TransactionTBL> TransactionTBLs { get; set; }
    }
}
