using OYW.OA.ApplicationInterface.People;
using OYW.OA.EFRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace OYW.OA.Application.People
{
    public class DepartmentService : IDepartmentService
    {
        private readonly OAEntity db;
        public DepartmentService(OAEntity db)
        {
            this.db = db;
        }
        public string LoadDeptTree(string node)
        {
            var departmentAll =db.ORG_Department.OrderBy(p => p.CreateTime);
            var departments = departmentAll.Where(p => p.ParentID == node);

            System.Text.StringBuilder dataString = new System.Text.StringBuilder();
            dataString.Append("[");
            foreach (var item in departments)
            {
                string lazy = "false";
                if (departmentAll.Any(p => p.ParentID == item.DeptID))
                {
                    lazy = "true";
                }
                if (dataString.ToString() != "[")
                {
                    dataString.Append(",");
                }
                dataString.Append("{\"title\": \"" + item.DeptName + "\", \"key\": \"" + item.DeptID + "\", \"isFolder\": true, \"isLazy\":" + lazy + " }");//, \"expand\":true 
            }
            dataString.Append("]");
            return dataString.ToString();
        }
    }
}
