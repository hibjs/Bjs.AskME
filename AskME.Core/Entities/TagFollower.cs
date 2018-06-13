using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AskME.Core.Entities
{
    [Table("TagFollower")]
    public class TagFollower
    {
        public int UserInfoId { get; set; }
        public virtual UserInfo UserInfo { get; set; }

        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }

    }
}
