using BlogCore.IRepository.Base;
using BlogCore.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogCore.IRepository
{	
	/// <summary>
	/// IRoleModulePermissionRepository
	/// </summary>	
	public interface IRoleModulePermissionRepository : IBaseRespository<RoleModulePermission>//类名
    {
        Task<List<RoleModulePermission>> WithChildrenModel();
    }
}
