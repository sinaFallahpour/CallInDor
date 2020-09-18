using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Category
{
    public class CategoryListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PersianTitle { get; set; }
        public bool IsEnabled { get; set; }
        public int? ParentId { get; set; }
        public int? ServiceId { get; set; }

        public List<CategoryListDTO> Children { get; set; }
    }
}
