﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OYW.OA.Web.Controllers
{
    public class PeopleManagerController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}