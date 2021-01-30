using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Helper.Models.Entities
{
    /// <summary>
    // جدول تنضیمات سایت مثل abut us , contactUs ,...  
    /// </summary>
    [Table("Settings")]
    public class SettingTBL : BaseEntity<int>
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string EnglishValue { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
