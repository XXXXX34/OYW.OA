using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OYW.OA.DTO;
using OYW.OA.EFRepositories;
using OYW.OA.Infrastructure.Encrypt;
using OYW.OA.Web.Models;

namespace OYW.OA.Web.Controllers
{
    public class HomeController : Controller
    {
        OAEntity db;
        UserMgr userMgr;

        public HomeController(OAEntity db, UserMgr userMgr)
        {
            this.db = db;
            this.userMgr = userMgr;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string username, string password = "")
        {
            string pwdEnc = MD5Util.GetMD5(password);

            var user = db.ORG_User.Where(p => p.UserName == username && p.UserPwd == pwdEnc && p.UserDisabled == 0).SingleOrDefault();
            if (user != null)
            {
                OAUser _OAUser = new OAUser
                {
                    UserName = user.UserName,
                    EmplID = user.EmplID
                };

                string sessionid = MD5Util.GetMD5(Guid.NewGuid().ToString().Replace("-", ""));

                userMgr.SetUser(sessionid, _OAUser);
                Response.Cookies.Append("oa.sessionid", sessionid);
                return RedirectToAction("Desktop", "Welcome", new { app = "Web", menu = "Welcome" });
            }
            return RedirectToAction("Index");
        }

        public ActionResult LoginOut()
        {
            try
            {
                string sessionid_cookie_value = null;
                Request.Cookies.TryGetValue("oa.sessionid", out sessionid_cookie_value);
                if (sessionid_cookie_value != null)
                {
                    userMgr.ClearUser(sessionid_cookie_value);
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index", "Home");
        }
    }
}
