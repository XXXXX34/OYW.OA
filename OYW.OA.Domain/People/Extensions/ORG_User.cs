using System;
using System.Collections.Generic;
using System.Text;

namespace OYW.OA.Domain.People
{
    public partial class ORG_User
    {
        public virtual List<ORG_UserLogon> UserLogonList { get; set; }
    }
}
