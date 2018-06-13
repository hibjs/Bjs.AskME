using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AskME.Core.Entities
{
    [Table("UserInfo")]
    public class UserInfo : BaseEntity
    {
        public UserInfo()
        {
            CreateTime = DateTime.Now;
            EmailValidated = false;
        }
        [StringLength(32)]
        public string UId { get; set; }
        [StringLength(64)]
        public string Email { get; set; }
        [StringLength(32)]
        public string Password { get; set; }
        [StringLength(32)]
        public string UserName { get; set; }
        [StringLength(11)]
        public string TelNumber { get; set; }
        [StringLength(32)]
        public string QQ { get; set; }
        [StringLength(32)]
        public string WeChat { get; set; }
        [StringLength(128)]
        public string Icon { get; set; }
        public string Bio { get; set; }
        public string Address { get; set; }
        public string SelfInfo { get; set; }
        public bool EmailValidated { get; set; }
        public DateTime CreateTime { get; private set; }


        public List<Answer> Answers { get; set; }
        public List<TagFollower> TagFollowers { get; set; }


        [NotMapped]
        public List<Tag> Tags { get; set; }

    }
}
