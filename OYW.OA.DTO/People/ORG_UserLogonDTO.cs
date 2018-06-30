using System;
using System.Collections.Generic;
using System.Text;

namespace OYW.OA.DTO.People
{
    public class ORG_UserLogonDTO
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string IP { get; set; }
        public string Location { get; set; }
        public DateTime? LogonTime { get; set; }
    }
}
