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
        IMenuService menuService;
        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        /// 
        public IActionResult GetMenus()
        {
            var result = menuService.GetMenus();
            return Json(new
            {
                Succeed = true,
                Data = result
            });
        }


    }
}