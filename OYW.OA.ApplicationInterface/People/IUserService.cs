using OYW.OA.DTO;
using OYW.OA.DTO.People;

namespace OYW.OA.ApplicationInterface.People
{
    public interface IUserService
    {
        ResponseResult<ORG_UserLogonDTO> GetLogonList(int page, int rows, string sort, string order);
        void Save(ORG_UserLogonDTO logon, OAUser oAUser);
    }
}