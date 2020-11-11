using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    /// این جذول سوالل های نظرسنجی که از از یوزر بعد از درخواست سرویس پرسیده میشه
    [Table("Answer")]
    public class AnswerTBL : BaseEntity<int>
    {
        public string Text { get; set; }
        public string EnglishText { get; set; }

        #region  relation

        [ForeignKey("QuestionPullTBL")]
        public int? QuestionId { get; set; }

        public QuestionPullTBL QuestionPullTBL { get; set; }
        #endregion 
    }
}
