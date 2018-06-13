using AskME.Core.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AskME.Core.Entities
{
    [Table("Answer")]
    public class Answer : BaseEntity
    {
        public Answer()
        {
            this.SubTime = DateTime.Now;
        }
        /// <summary>
        /// 回答内容
        /// </summary>
        public string AnserContent { get; set; }
        /// <summary>
        /// 采用
        /// </summary>
        public bool Adoption { get; set; }
        public DateTime SubTime { get; set; }

        public int Up { get; set; }
        public int Down { get; set; }


        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public int UserId { get; set; }

        [NotMapped]
        public string UserName { get; set; }
    }
}
