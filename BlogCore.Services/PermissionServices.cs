using BlogCore.Services.Base;
using BlogCore.Model.Models;
using BlogCore.IRepository;
using BlogCore.IServices;

namespace Blog.Core.Services
{	
	/// <summary>
	/// PermissionServices
	/// </summary>	
	public class PermissionServices : BaseServices<Permission>, IPermissionServices
    {
	
        IPermissionRepository _dal;
        public PermissionServices(IPermissionRepository dal)
        {
            this._dal = dal;
            base.baseDal = dal;
        }
       
    }
}
