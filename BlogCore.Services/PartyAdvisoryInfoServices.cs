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
    public class PartyAdvisoryInfoServices : BaseServices<PartyAdvisoryInfo>, IPartyAdvisoryInfoServices
    {
        IPartyAdvisoryInfoRepository dal;
        IMapper _mapper;
        public PartyAdvisoryInfoServices(IPartyAdvisoryInfoRepository _dal, IMapper mapper)
        {
            this.dal = _dal;
            base.baseDal = dal;
            _mapper = mapper;
        }
       
    }
}
