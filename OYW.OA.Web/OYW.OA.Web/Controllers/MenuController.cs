using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OYW.OA.EFRepositories;

namespace OYW.OA.Web.Controllers
{
    public class MenuController : BaseController
    {

        public MenuController(OAEntity db)
        {
            this.db = db;
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        /// 
        public IActionResult GetMenus()
        {
            var cur_user_id = Current_User.EmplID;
            var menus = db.SYS_Menu.ToList();
            //非管理员
            if (Current_User.EmplID != "99999999-9999-9999-9999-999999999999")
            {
                var needRemoveLst = new List<Guid>();
                menus.ForEach(x =>
                {
                    string[] permissionCodes = new string[] { x.MenuName + "-Use", x.MenuName + "-Admin" };
                    bool need = true;
                    foreach (var code in permissionCodes)
                    {
                        if (VerifyPermission(code, cur_user_id))
                        {
                            need = false;
                            break;
                        }
                    }
                    if (need)
                    {
                        needRemoveLst.Add(x.ID);
                    }
                });
                menus.RemoveAll(p => needRemoveLst.Contains(p.ID));
            }
            var result = menus.Where(p => p.ParentID == null).Select(p => new
            {
                MenuName = p.MenuName,
                Code = p.Code,
                Icon = p.Icon,
                URL = p.URL,
                Sort = p.Sort,
                SubMenus = menus.Where(x => x.ParentID == p.ID).Select(x => new
                {
                    MenuName = x.MenuName,
                    Code = x.Code,
                    Icon = x.Icon,
                    URL = x.URL,
                    Sort = x.Sort,
                }).OrderBy(x => x.Sort)
            }).OrderBy(p => p.Sort);

            return Json(new
            {
                Succeed = true,
                Data = result
            });
        }

        private bool VerifyPermission(string code, string emplid)
        {
            return false;
        }
    }
}