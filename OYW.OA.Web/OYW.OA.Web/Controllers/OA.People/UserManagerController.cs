using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OYW.OA.Application.People;
using OYW.OA.ApplicationInterface.People;

namespace OYW.OA.Web.Controllers
{
    public class UserManagerController : BaseController
    {
        readonly IUserService userService;

        public UserManagerController(UserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}