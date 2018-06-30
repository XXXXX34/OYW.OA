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
        }


        public ResponseResult<ORG_UserLogonDTO> GetLogonList(int page, int rows, string sort, string order)
        {
            var response = new ResponseResult<ORG_UserLogonDTO> { };
            TypeAdapterConfig typeAdapterConfig = new TypeAdapterConfig();
            typeAdapterConfig.NewConfig<ORG_UserLogon, ORG_UserLogonDTO>().Ignore(p => p.IP);
            response.rows = db.ORG_UserLogon.OrderBy($"{sort} {order}").Adapt<List<ORG_UserLogonDTO>>(typeAdapterConfig);
            response.total = db.ORG_UserLogon.Count();
            return response;
        }
    }
}
