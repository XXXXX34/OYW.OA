using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OYW.OA.Domain.People
{
    public partial class ORG_User
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(500)]
        public string UserPwd { get; set; }

        [StringLength(50)]
        public string EmplID { get; set; }

        public int UserLogonTimes { get; set; }

        public int UserNeedChgPwd { get; set; }

        public int UserDisabled { get; set; }

        [StringLength(50)]
        public string UserLastLogonIP { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
