using System;
using System.Collections.Generic;
using System.Text;

namespace OYW.OA.DTO
{
    [Serializable]
    public class OAUser
    {

        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string EmplID { get; set; }

        public string EmplName { get; set; }
        public string DeptID { get; set; }

        public string DeptName { get; set; }

        public DateTime LastUpdateTime { get; set; }

    }
}
