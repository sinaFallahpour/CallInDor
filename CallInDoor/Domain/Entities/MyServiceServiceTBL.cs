using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    /// <summary>
    //این  سرویس های یوزر که از نوع سرویس هستند    
    /// </summary>
    [Table("MyServiceService")]
    public class MyServiceServiceTBL : BaseEntity<int>
    {

        [MaxLength(600)]
        public string Description { get; set; }



        /// <summary>
        /// این فیلد فقط و فقط مخصوص سرویس هایی است که از نوع ترنسلیت هستند
        /// </summary>
        public bool BeTranslate { get; set; }


        //public string Speciality { get; set; }
        //public string Area { get; set; }


        public bool FileNeeded { get; set; }


        [MaxLength(600)]
        public string FileDescription { get; set; }


        [Range(0, double.MaxValue)]
        public double Price { get; set; }


        /// <summary>
        /// برآورد زمان تحویل کار
        /// </summary>
        [MaxLength(600)]
        public string WorkDeliveryTimeEstimation { get; set; }


        /// <summary>
        /// چطور کار میکند
        /// </summary>
        [MaxLength(600)]
        public string HowWorkConducts { get; set; }


        [MaxLength(600)]
        public string DeliveryItems { get; set; }



        /// <summary>
        /// مجموع تگ هایی که  سیستم معرفی میکند و تگ های که خودش وارد میکند
        /// </summary>
        [MaxLength(600)]
        public string Tags { get; set; }



        #region Relation



        //[ForeignKey("CatId")]
        //public virtual CategoryTBL CategoryTBL { get; set; }

        //public int? CatId { get; set; }


        //[ForeignKey("SubCatId")]
        //public virtual CategoryTBL SubCategoryTBL { get; set; }

        //public int? SubCatId { get; set; }




        // relation With BaseMyChatTBL
        public virtual BaseMyServiceTBL BaseMyChatTBL { get; set; }

        //[ForeignKey("BaseMyChatTBL")]
        public int? BaseId { get; set; }


        #endregion 


    }
}
