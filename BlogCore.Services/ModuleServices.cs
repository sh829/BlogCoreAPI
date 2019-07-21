using BlogCore.Services.Base;
using BlogCore.Model.Models;
using BlogCore.IRepository;
using BlogCore.IServices;

namespace BlogCore.Services
{	
	/// <summary>
	/// ModuleServices
	/// </summary>	
	public class ModuleServices : BaseServices<Module>, IModuleServices
    {
	
        IModuleRepository _dal;
        public ModuleServices(IModuleRepository dal)
        {
            this._dal = dal;
            base.baseDal = dal;
        }
       
    }
}
