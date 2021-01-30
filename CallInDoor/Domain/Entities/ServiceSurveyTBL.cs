using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("ServiceSurvey")]
    public class ServiceSurveyTBL : BaseEntity<int>
    {

        [MaxLength(120)]
        public string UserName { get; set; }

        public DateTime CreateDate { get; set; }

        #region  Relation 

        /// آیدی سرویس
        public int? ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public ServiceTBL Service { get; set; }


        /// آیدی سوال
        public int? QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public QuestionPullTBL QuestionTBL { get; set; }


        /// آیدی جواب
        public int? AnswerId { get; set; }

        [ForeignKey("AnswerId")]
        public AnswerTBL AnswerTBL { get; set; }

        #endregion



    }
}
