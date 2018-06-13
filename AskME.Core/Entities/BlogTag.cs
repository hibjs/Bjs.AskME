using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AskME.Core.Entities
{
    [Table("BolgTag")]
    public class BlogTag
    {
        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }

        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
