using AskME.Core.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AskME.Core.Entities
{
    [Table("Tag")]
    public class Tag : BaseEntity
    {
        public Tag()
        {
            SubTime = DateTime.Now;
            Disabled = Status.OK;
        }
        /// <summary>
        /// 话题名称
        /// </summary>
        [StringLength(64)]
        public string TagName { get; set; }

        public string Description { get; set; }

        public Status Disabled { get; set; }
        public DateTime SubTime { get;private set; }

        public List<QuestionTags> QuestionTags { get; set; }
        public List<TagFollower> TagFollowers { get; set; }
        public List<BlogTag> BlogTags { get; set; }


        [NotMapped]
        public int Count { get; set; }
    }
}
