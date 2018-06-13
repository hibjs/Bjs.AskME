using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AskME.Core.Entities
{
    [Table("Bolg")]
    public class Blog : BaseEntity
    {
        public Blog()
        {
            SubTime = DateTime.Now;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }


        public int Up { get; set; }
        public int Down { get; set; }

        public int ViewCount { get; set; }

        /// <summary>
        /// 长传时间
        /// </summary>
        public DateTime SubTime { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserInfoId { get; set; }
        public virtual UserInfo UserInfo { get; set; }


        public List<BlogTag> BlogTags { get; set; }


        [NotMapped]
        public List<Tag> Tags { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string Icon { get; set; }
    }
}
