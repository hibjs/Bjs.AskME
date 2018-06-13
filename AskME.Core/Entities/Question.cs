using AskME.Core.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AskME.Core.Entities
{
    [Table("Question")]
    public class Question : BaseEntity
    {
        public Question()
        {

            this.SubTime = DateTime.Now;
        }
        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(64)]
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SubTime { get; set; }


        public int UserInfoId { get; set; }
        public virtual UserInfo UserInfo { get; set; }

        public List<QuestionTags> QuestionTags { get; set; }

        public int Up { get; set; }
        public int Down { get; set; }

        public int ViewCount { get; set; }

        [NotMapped]
        public List<Tag> Tags { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public List<Answer> Answers { get; set; }

        /// <summary>
        /// 回答的个数
        /// </summary>
        [NotMapped]
        public int Count { get; set; }

        /// <summary>
        /// 通过Answer反向查找时，记录Answer值
        /// </summary>
        [NotMapped]
        public string AnswerContent { get; set; }

        [NotMapped]
        public int AnswerId { get; set; }
    }
}
