using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OYW.OA.Domain.People
{
    public partial class ORG_Department
    {
        [Key]
        [StringLength(50)]
        public string DeptID { get; set; }

        [StringLength(50)]
        public string DeptName { get; set; }

        [StringLength(250)]
        public string DeptDescr { get; set; }

        [StringLength(50)]
        public string ParentID { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        [StringLength(50)]
        public string DeptHierarchyCode { get; set; }
    }
}
