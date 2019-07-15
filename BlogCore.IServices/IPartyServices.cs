using BlogCore.IServices.Base;
using BlogCore.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.IServices
{
    public interface IPartyServices:IBaseServices<Party>
    {

         Task<List<Party>> getParties();
    }
}
