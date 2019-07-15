using BlogCore.IServices;
using BlogCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Services
{
    public class RoleModulePermissionServices : IRoleModulePermissionServices
    {
        public Task<List<RoleModulePermission>> GetRoleModule()
        {
            throw new NotImplementedException();
        }
    }
}
