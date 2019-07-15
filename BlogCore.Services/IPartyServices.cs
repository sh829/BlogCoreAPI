using BlogCore.IRepository;
using BlogCore.IServices;
using BlogCore.Model.Model;
using BlogCore.Repository;
using BlogCore.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Services
{
    public class PartyServices : BaseServices<Party>,IPartyServices
    {
        IPartyRepository dal ;

        public PartyServices(IPartyRepository _dal)
        {
            this.dal = _dal;
            base.baseDal = dal;
        }

        public async Task<List<Party>> getParties()
        {
            var PartyList = await dal.Query(null,a=>a.Id);
            return PartyList;
        }
    }
}
