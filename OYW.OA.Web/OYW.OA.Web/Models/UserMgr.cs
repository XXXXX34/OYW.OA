using OYW.OA.DTO;
using OYW.OA.Infrastructure.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OYW.OA.Web.Models
{
    public class UserMgr
    {
        private readonly RedisHelper _redisHelper;
        public UserMgr(RedisHelper redisHelper)
        {
            _redisHelper = redisHelper;
        }

        public  void SetUser(string sessionID, OAUser _OAUser)
        {
            _redisHelper.Set<OAUser>(sessionID, _OAUser, TimeSpan.FromMinutes(60));
        }

        public  OAUser GetUser(string sessionID)
        {
            return _redisHelper.Get<OAUser>(sessionID);
        }
    }
}
