using OYW.OA.DTO;
using OYW.OA.DTO.Settings;
using OYW.OA.EFRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using log4net;
using OYW.OA.Infrastructure.User;

namespace OYW.OA.Application.Settings
{
    public class MenuService
    {
        private readonly OAEntity db;
        private readonly OAUser user;
        public MenuService(OAEntity db, OAUser user, ILog log)
        {
            this.db = db;
            this.user = user;
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        /// 
        public List<SYS_MenuDTO> GetMenus()
        {

            var cur_user_id = user.EmplID;
            var menus = db.SYS_Menu.ToList();
            //非管理员
            if (user.EmplID != AdminUser.EmplID)
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

            return menus.Where(p => p.ParentID == null).Select(p => new SYS_MenuDTO
            {
                MenuName = p.MenuName,
                Code = p.Code,
                Icon = p.Icon,
                URL = p.URL,
                Sort = p.Sort,
                SubMenus = menus.Where(x => x.ParentID == p.ID).Select(x => new SYS_MenuDTO
                {
                    MenuName = x.MenuName,
                    Code = x.Code,
                    Icon = x.Icon,
                    URL = x.URL,
                    Sort = x.Sort,
                }).OrderBy(x => x.Sort).ToList()
            }).OrderBy(p => p.Sort).ToList();
        }

        private bool VerifyPermission(string code, string emplid)
        {
            return false;
        }
    }
}
