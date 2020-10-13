using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    /// <summary>
    //این  سرویس های یوزر که از نوع کورس هستند    
    /// </summary>
    [Table("MyCourseService")]
    public class MyCourseServiceTBL : BaseEntity<int>
    {
        [MaxLength(1000)]
        public string Description { get; set; }



        //public string Category { get; set; }

        [MaxLength(200)]
        public string NewCategory { get; set; }



        public string TotalLenght { get; set; }


        [Range(0, double.MaxValue)]
        public double Price { get; set; }


        public int DisCountPercent { get; set; }




        /// <summary>
        /// آدرس فیلم Preview
        /// </summary>
        public string PreviewVideoAddress { get; set; }



        #region Relation

        /// <summary>
        /// لیست تاپیک های یک کورس
        /// </summary>
        public  ICollection<MyCourseTopics> TopicsTBLs { get; set; }



        // relation With BaseMyChatTBL
        public BaseMyServiceTBL BaseMyChatTBL { get; set; }

        [ForeignKey("BaseMyChatTBL")]
        public int? BaseId { get; set; }

        #endregion 
    }

}
