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
        [MaxLength(600)]
        public string Description { get; set; }



        //public string Category { get; set; }
        //public string NewCategory { get; set; }



        public string TotalLenght { get; set; }

        public int MyProperty { get; set; }


        public int DisCountPercent { get; set; }




        /// <summary>
        /// آدرس فیلم Preview
        /// </summary>
        public string PreviewVideoAddress { get; set; }







        #region Relation

        public virtual ICollection<TopicsTBL> TopicsTBLs { get; set; }


        #endregion 
    }

}
