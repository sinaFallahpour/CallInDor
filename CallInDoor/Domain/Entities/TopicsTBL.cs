using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    /// <summary>
    /// این جدول تاپیک های سرویس هایی از جنس کورس برای کاربر هستند
    /// </summary>
    [Table("Topics")]
    public class TopicsTBL : BaseEntity<int>
    {
        [MaxLength(100)]
        public string TopicName { get; set; }

        public string TopicFileAddress { get; set; }

        /// <summary>
        /// آیا این تاپیک برای مه ایگان است یا نه
        /// </summary>
        public bool IsFreeForEveryOne { get; set; }



        [MaxLength(200)]
        public string  Compopnents { get; set; }

        [MaxLength(200)]
        public string  Routings { get; set; }


        public string Directives { get; set; }




        #region relation

        public virtual MyCourseServiceTBL MyCourseServiceTBL { get; set; }

        [ForeignKey("MyCourseServiceTBL")]
        public int? CourseId { get; set; }

        #endregion


    }
}
