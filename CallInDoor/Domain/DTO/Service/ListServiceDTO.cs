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

        public string ImageAddress { get; set; }

        public bool IsProfileOptional { get; set; }


        /// <summary>
        /// رنگ سرویس
        /// </summary>
        public string Color { get; set; }


        /// <summary>
        /// آیا فعال است
        /// </summary>
        public bool IsEnabled { get; set; }


        public string RoleName { get; set; }


        /// <summary>
        /// /حداقل فیمت برای برای  سرویس  تایپ هایی  از نوع سرویس
        /// </summary>
        public double MinPriceForService { get; set; }

        public int SitePercent { get; set; }


        /// <summary>
        /// حداقل زمان برای سرویس های Voice//VideCall//voiceCall
        /// </summary>
        public double MinSessionTime { get; set; }




        /// <summary>
        /// حداقل قیمت مجاز برای کاربران سرویس های چت یا وویس یا ویدیو
        /// </summary>
        public double AcceptedMinPrice { get; set; }




        ///// <summary>
        ///// حداقل قیمت مجاز برای کاربران تیتیو برای سرویس های چت یا وویس یا ویدیو
        ///// </summary>
        //public double AcceptedMinPriceForNative { get; set; }

        ///// <summary>
        ///// حداقل قیمت مجاز برای کاربران غیر تیتیو برای سرویس های چت یا وویس یا ویدیو
        ///// </summary>
        //public double AcceptedMinPriceForNonNative { get; set; }






    }
}
