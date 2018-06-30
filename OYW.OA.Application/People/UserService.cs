using log4net;
using OYW.OA.DTO;
using OYW.OA.DTO.People;
using OYW.OA.EFRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using Mapster;
using OYW.OA.Domain.People;
using System.Linq;
using System.Linq.Dynamic.Core;
using OYW.OA.Infrastructure.User;

namespace OYW.OA.Application.People
{

    public class UserService
    {
        private readonly OAEntity db;
        public UserService(OAEntity db)
        {
            this.db = db;
        }

        public void Save(ORG_UserLogonDTO logon, OAUser oAUser)
        {
            var userLogon = logon.Adapt<ORG_UserLogon>();
            userLogon.UserID = oAUser.ID;
            db.ORG_UserLogon.Add(userLogon);
            db.SaveChanges();
            //var userTemp = db.ORG_User.Where(p => p.EmplID == AdminUser.EmplID).SingleOrDefault();
            //var list = userTemp.UserLogonList.ToList();
        }
    }
}
