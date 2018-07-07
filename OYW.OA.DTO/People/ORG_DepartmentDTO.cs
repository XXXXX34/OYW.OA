using System;
using System.Collections.Generic;
using System.Text;

namespace OYW.OA.DTO.People
{
    public class ORG_DepartmentDTO
    {
        public string DeptID { get; set; }

        public string DeptName { get; set; }

        public string DeptDescr { get; set; }

        public string ParentID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public string DeptHierarchyCode { get; set; }
    }
}
