using BlogCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.IServices
{
   public  interface IRoleModulePermissionServices
    {
        Task<List<RoleModulePermission>> GetRoleModule();
    }
}
