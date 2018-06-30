using Microsoft.AspNetCore.Mvc;
using OYW.OA.DTO;
using OYW.OA.EFRepositories;
using OYW.OA.Infrastructure.Redis;
using OYW.OA.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OYW.OA.Web.Controllers
{

    [CoreActionFilter(typeof(CoreAuthFilterAttribute))]
    public class BaseController : Controller
    {
        protected OAEntity db;
        public OAUser Current_User
        {
            get
            {
                string sessionid = "";
                Request.Cookies.TryGetValue("oa.sessionid", out sessionid);
                var redisHelper = IocManager.Resolve<RedisHelper>();
                return redisHelper.Get<OAUser>(sessionid);
            }
        }
        

    }
}
