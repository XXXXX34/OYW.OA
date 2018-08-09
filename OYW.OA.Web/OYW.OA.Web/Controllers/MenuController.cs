using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OYW.OA.Application.Settings;
using OYW.OA.ApplicationInterface.Settings;
using OYW.OA.EFRepositories;

namespace OYW.OA.Web.Controllers
{
    public class MenuController : BaseController
    {
        public IMenuService MenuService { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        /// 
        public IActionResult GetMenus()
        {
            var i = 100;

            var result = MenuService.GetMenus();
            return Json(new
            {
                Succeed = true,
                Data = result
            });
        }


    }
}