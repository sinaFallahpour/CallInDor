using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("MyServiceService")]
    public class MyServiceServiceTBL : BaseEntity<int>
    {

        [MaxLength(200)]
        public string ServiceName { get; set; }


        [MaxLength(600)]
        public string Description { get; set; }






        /// <summary>
        /// آیا تخصص دارد؟
        ///  اگر این فیلد فلز باشد   آیدی 
        /// Speciality va Area = null  میشود
        /// if = HasSpeciality =false   theb   specialityId and AreaId == false
        /// </summary>
        public bool HasSpeciality { get; set; }




        //are


        //specialty













        /// <summary>
        /// آیا نیاز به فایل دارد
        /// </summary>
        public bool FileNeeded { get; set; }


        [MaxLength(600)]
        public string FileDescriptions { get; set; }



        /// <summary>
        /// حداقل قیمت برای سرویس    
        /// این فیلد باید مقدارش ز آن چیزی که ادمین پنل معلوم کردیم بیشتر باشد
        /// </summary>
        public double Price { get; set; }


        /// <summary>
        /// برآورد زمان تحویل کار
        /// </summary>
        public string WorkDeliveryTimeEstmation { get; set; }


        /// <summary>
        /// توضیحات درباره اینکه چطور کار میکند
        /// </summary>
        [MaxLength(600)]
        public string HowToWorkConducs { get; set; }


        [MaxLength(600)]
        public string DeliveyItems { get; set; }




        #region Relation 


        [ForeignKey("CatId")]
        public virtual CategoryTBL CategoryTBL { get; set; }

        public int? CatId { get; set; }



        [ForeignKey("SubCatId")]
        public virtual CategoryTBL SubCategoryTBL { get; set; }

        public int? SubCatId { get; set; }




        #endregion 




    }
}
