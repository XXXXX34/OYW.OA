using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OYW.OA.ApplicationInterface.People;

namespace OYW.OA.Web.Controllers
{
    public class EmplLogonController : BaseController
    {
        public IUserService UserService { get; set; }

       
        public IActionResult Index()
        {
            return View();
        }


        public JsonResult LogonList(int page, int rows, string sort, string order)
        {
            var result = UserService.GetLogonList(page, rows, sort, order);
            return Json(result);
        }
    }
}