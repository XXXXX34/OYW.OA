using System;
using System.Collections.Generic;
using System.Text;

namespace OYW.OA.DTO.Settings
{
    public class SYS_MenuDTO
    {
        public Guid ID { get; set; }

        public string MenuName { get; set; }

        public string Code { get; set; }

        public string Icon { get; set; }

        public string App { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Params { get; set; }

        public string URL { get; set; }

        public Guid? ParentID { get; set; }

        public int? Sort { get; set; }

        public string EmplID { get; set; }

        public string EmplName { get; set; }

        public string DeptID { get; set; }

        public string DeptName { get; set; }

        public DateTime? CreateTime { get; set; }

        public List<SYS_MenuDTO> SubMenus { get; set; }
    }
}
