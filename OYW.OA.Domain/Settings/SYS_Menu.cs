using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OYW.OA.Domain.Settings
{
    public partial class SYS_Menu
    {
        public Guid ID { get; set; }

        [StringLength(50)]
        public string MenuName { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        [StringLength(50)]
        public string App { get; set; }

        [StringLength(50)]
        public string Controller { get; set; }

        [StringLength(50)]
        public string Action { get; set; }

        [StringLength(50)]
        public string Params { get; set; }

        [StringLength(500)]
        public string URL { get; set; }

        public Guid? ParentID { get; set; }

        public int? Sort { get; set; }

        [StringLength(50)]
        public string EmplID { get; set; }

        [StringLength(50)]
        public string EmplName { get; set; }

        [StringLength(50)]
        public string DeptID { get; set; }

        [StringLength(50)]
        public string DeptName { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
