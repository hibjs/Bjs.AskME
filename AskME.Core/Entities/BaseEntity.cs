using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AskME.Core.Entities
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
