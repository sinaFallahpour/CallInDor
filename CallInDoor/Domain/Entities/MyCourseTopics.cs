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
    [Table("MyCourseTopics")]
    public class MyCourseTopics : BaseEntity<int>
    {

        [MaxLength(200)]
        public string TopicName { get; set; }

        public  bool IsFreeForEveryOne { get; set; }

        public string  FileAddress { get; set; }




        public bool IsConfirmByAdmin { get; set; }


        #region Relation




        [ForeignKey("MyCourseId")]
        public MyCourseServiceTBL MyCourseServiceTBL { get; set; }

        public int? MyCourseId { get; set; }

        #endregion


    }
}
