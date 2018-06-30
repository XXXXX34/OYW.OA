using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OYW.OA.Domain.People
{
    public partial class ORG_UserLogon
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string IP { get; set; }
        public string Location { get; set; }
        public DateTime? LogonTime { get; set; }

    }
}
