using BlogCore.Repository.Base;
using BlogCore.Model.Models;
using BlogCore.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogCore.Repository
{
    /// <summary>
    /// RoleModulePermissionRepository
    /// </summary>	
    public class RoleModulePermissionRepository : BaseRespository<RoleModulePermission>, IRoleModulePermissionRepository
    {

        public async Task<List<RoleModulePermission>> WithChildrenModel()
        {
            var list = await Task.Run(() => db.Queryable<RoleModulePermission>()
                    .Mapper(it => it.Role, it => it.RoleId)
                    .Mapper(it => it.Permission, it => it.PermissionId)
                    .Mapper(it => it.Module, it => it.ModuleId).ToList());

            return null;
        }

    }
}

