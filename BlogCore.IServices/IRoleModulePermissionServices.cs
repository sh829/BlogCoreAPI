using BlogCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlogCore.IServices.Base;

namespace BlogCore.IServices
{
   public  interface IRoleModulePermissionServices: IBaseServices<RoleModulePermission>
    {
        Task<List<RoleModulePermission>> GetRoleModule();
    }
}
