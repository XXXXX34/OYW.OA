using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OYW.OA.Application.Settings;
using OYW.OA.EFRepositories;

namespace OYW.OA.Web.Controllers
{
    public class MenuController : BaseController
    {
        MenuService menuService;
        public MenuController(MenuService menuService)
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