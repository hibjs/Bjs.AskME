using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AskME.Core.Entities
{
    [Table("QuestionTags")]
    public class QuestionTags
    {
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }


        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
