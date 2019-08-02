using BlogCore.IRepository.Base;
using BlogCore.Model.Models;
using BlogCore.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.IRepository
{
    public interface IPartyBookInfoRespository : IBaseRespository<PartyBookInfo>
    {
        Task<List<PartyBookInfoViewModel>> GetPartyBookInofs(int page, int pageSize, string key, string strOrderByFileds);
    }
}
