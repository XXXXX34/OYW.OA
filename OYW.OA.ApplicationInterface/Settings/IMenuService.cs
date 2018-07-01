using System.Collections.Generic;
using OYW.OA.DTO.Settings;

namespace OYW.OA.ApplicationInterface.Settings
{
    public interface IMenuService
    {
        List<SYS_MenuDTO> GetMenus();
    }
}