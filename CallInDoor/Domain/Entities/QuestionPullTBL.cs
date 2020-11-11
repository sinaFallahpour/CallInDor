﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    /// این جذول سوالل های نظرسنجی که از از یوزر بعد از درخواست سرویس پرسیده میشه
    [Table("QuestionPull")]
    public class QuestionPullTBL : BaseEntity<int>
    {
        public string Key { get; set; }
        public string Text { get; set; }
        public string EnglishText { get; set; }

        public List<AnswerTBL> AnswersTBLs { get; set; }
    }
}