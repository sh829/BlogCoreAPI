using AutoMapper;
using BlogCore.Common.Attributes;
using BlogCore.IRepository;
using BlogCore.IServices;
using BlogCore.Model;
using BlogCore.Model.Models;
using BlogCore.Model.ViewModels;
using BlogCore.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Services
{
    public class BookInfoServicesvices : BaseServices<PartyBookInfo>, IPartyBookInfoServices
    {
        readonly IPartyBookInfoRespository _partyBookInfo;
        readonly IExtraOtherProjectRespository _extraOtherProject;
        readonly IExtraProjectInfoRespository _extraProjectInfo;
        readonly ICustomInfoRespository _customInfo;
        readonly IMapper _mapper;
        public BookInfoServicesvices(IPartyBookInfoRespository partyBookInfo, 
            IExtraProjectInfoRespository extraProjectInfo, 
            IExtraOtherProjectRespository extraOtherProject, 
            ICustomInfoRespository customInfo, 
            IMapper mapper)
        {
            this._partyBookInfo = partyBookInfo;
            this._extraOtherProject = extraOtherProject;
            this._extraProjectInfo = extraProjectInfo;
            this._customInfo = customInfo;
            base.baseDal = _partyBookInfo;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取派对预定信息列表
        /// </summary>
        /// <returns></returns>
        //[Caching(AbsoluteExpiration = 10)]
        public async Task<List<PartyBookInfoViewModel>> GetBookInfos(int page,int  pageSize, string key)

        {
            List<PartyBookInfoViewModel> partyBooksList = new List<PartyBookInfoViewModel>();
            try
            {
                //var bookInfos = await _partyBookInfo.Query(a=>a.TopicName.Contains(key),page,pageSize, "UpdateTime");
                //var customInfo = await _customInfo.Query(a => a.IsDelete != true && a.Name.Contains(key));
                var bookInfos = await _partyBookInfo.GetPartyBookInofs(page, pageSize, key, "UpdateTime");
                foreach (var info in bookInfos)
                {
                    var p1 = await _extraProjectInfo.Query(a => a.BookInfoId == info.BookInfoId);
                    var p2 = await _extraOtherProject.Query(a => a.BookInfoId == info.BookInfoId);
                    info.ExtraProjectInfo = p1;
                    info.ExtraOtherProject = p2;
                    partyBooksList.Add(info);
                }
                return partyBooksList;
            }
            catch (Exception ex)
            {
                return partyBooksList;
            }

        }

    }
}
