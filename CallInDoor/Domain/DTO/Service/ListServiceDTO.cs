using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Service
{
    public class ListServiceDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }



        public string PersianName { get; set; }


        /// <summary>
        /// رنگ سرویس
        /// </summary>
        public string Color { get; set; }


        /// <summary>
        /// آیا فعال است
        /// </summary>
        public bool IsEnabled { get; set; }


    }
}
