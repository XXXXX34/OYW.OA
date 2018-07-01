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
        readonly IUserService userService;

        public EmplLogonController(IUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public JsonResult LogonList(int page, int rows, string sort, string order)
        {
            var result = userService.GetLogonList(page, rows, sort, order);
            return Json(result);
        }
    }
}