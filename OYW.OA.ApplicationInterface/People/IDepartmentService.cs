using System;
using System.Collections.Generic;
using System.Text;

namespace OYW.OA.ApplicationInterface.People
{
    public interface IDepartmentService
    {
        string LoadDeptTree(string node);
    }
}
