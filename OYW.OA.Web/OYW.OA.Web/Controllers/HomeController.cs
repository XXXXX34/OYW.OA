using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OYW.OA.Application.People;
using OYW.OA.ApplicationInterface.People;
using OYW.OA.DTO;
using OYW.OA.DTO.People;
using OYW.OA.EFRepositories;
using OYW.OA.Infrastructure;
using OYW.OA.Infrastructure.Encrypt;
using OYW.OA.Web.Models;

namespace OYW.OA.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly OAEntity db;
        readonly UserMgr userMgr;
        readonly IHttpContextAccessor accessor;
        public IUserService UserService { get; set; }
        public HomeController(OAEntity db, UserMgr userMgr, IHttpContextAccessor accessor)
        {
            this.db = db;
            this.userMgr = userMgr;
            this.accessor = accessor;
        }

        public IActionResult Index()
        {
            throw new OYWValidationException("验证失败！");
            return View();
        }

        public IActionResult Login(string username, string password = "")
        {
            string pwdEnc = MD5Util.GetMD5(password);

            var user = db.ORG_User.Where(p => p.UserName == username && p.UserPwd == pwdEnc && p.UserDisabled == 0).SingleOrDefault();
            if (user != null)
            {
                OAUser oAUser = new OAUser
                {
                    ID = user.ID,
                    UserName = user.UserName,
                    EmplID = user.EmplID
                };

                string sessionid = MD5Util.GetMD5(Guid.NewGuid().ToString().Replace("-", ""));

                userMgr.SetUser(sessionid, oAUser);
                Response.Cookies.Append("oa.sessionid", sessionid);

                UserService.Save(new ORG_UserLogonDTO
                {
                    ID = Guid.NewGuid(),
                    IP = accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Location = "",
                    LogonTime = DateTime.Now
                }, oAUser);
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
