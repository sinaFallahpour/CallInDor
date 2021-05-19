using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Category
{
    public class Top6UserOfCategoryDTO
    {
        public string ImageAddress { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsOnline { get; set; }
        public int StarCount { get; set; }
    }
}
