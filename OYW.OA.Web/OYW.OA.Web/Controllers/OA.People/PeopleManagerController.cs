using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OYW.OA.ApplicationInterface.People;

namespace OYW.OA.Web.Controllers
{
    public class PeopleManagerController : BaseController
    {
        private readonly IDepartmentService departmentService;

        public PeopleManagerController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ContentResult LoadDept(string node)
        {
            string result = departmentService.LoadDeptTree(node);
            return Content(result);
        }




    }
}