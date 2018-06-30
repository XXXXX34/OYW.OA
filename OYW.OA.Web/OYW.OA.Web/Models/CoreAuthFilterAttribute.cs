using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OYW.OA.DTO;
using OYW.OA.Infrastructure.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OYW.OA.Web.Models
{
    public class CoreAuthFilterAttribute : Attribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            bool authored = false;
            string cookie_sessionid = null;
            context.HttpContext.Request.Cookies.TryGetValue("oa.sessionid", out cookie_sessionid);


            if (cookie_sessionid != null)
            {

                if (!String.IsNullOrEmpty(cookie_sessionid))
                {
                    var redisHelper = IocManager.Resolve<RedisHelper>();
                    OAUser _OAUser = redisHelper.Get<OAUser>(cookie_sessionid);
                    if (_OAUser != null && _OAUser.LastUpdateTime < DateTime.Now.AddMinutes(-30))
                    {
                        _OAUser.LastUpdateTime = DateTime.Now;
                        redisHelper.Set<OAUser>(cookie_sessionid, _OAUser, TimeSpan.FromMinutes(60));//60分钟有效期
                        authored = true;
                    }
                    else if (_OAUser != null)
                    {
                        authored = true;
                    }


                }

            }
            if (!authored)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }



        }
    }
}
