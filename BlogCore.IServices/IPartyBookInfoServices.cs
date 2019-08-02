using BlogCore.Model.Models;
using BlogCore.IServices.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlogCore.Model.ViewModels;
using BlogCore.Model;

namespace BlogCore.IServices
{
    public interface IPartyBookInfoServices : IBaseServices<PartyBookInfo>
    {
          Task<List<PartyBookInfoViewModel>> GetBookInfos(int page, int pageSize, string key);
    }
}
