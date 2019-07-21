using BlogCore.Services.Base;
using BlogCore.Model.Models;
using BlogCore.IRepository;
using BlogCore.IServices;

namespace BlogCore.Services
{	
	/// <summary>
	/// ModulePermissionServices
	/// </summary>	
	public class ModulePermissionServices : BaseServices<ModulePermission>, IModulePermissionServices
    {
	
        IModulePermissionRepository _dal;
        public ModulePermissionServices(IModulePermissionRepository dal)
        {
            this._dal = dal;
            base.baseDal = dal;
        }
       
    }
}
