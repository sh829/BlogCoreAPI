﻿using AutoMapper;
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
    public class ExtraProjectInfoServices : BaseServices<ExtraProjectInfo>, IExtraProjectInfoServices
    {
        IExtraProjectInfoRespository dal;
        IMapper _mapper;
        public ExtraProjectInfoServices(IExtraProjectInfoRespository _dal, IMapper mapper)
        {
            this.dal = _dal;
            base.baseDal = dal;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10)]
        public async Task<List<PartyAdvisoryViewModel>> GetCustomInfos()
        {
            try
            {
                var customInfos = await dal.Query();
                return _mapper.Map<List<PartyAdvisoryViewModel>>(customInfos);
            }catch (Exception ex)
            {
                return  new List<PartyAdvisoryViewModel>();
            }

        }
        
    }
}
