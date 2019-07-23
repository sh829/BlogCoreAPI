using AutoMapper;
using BlogCore.Model;
using BlogCore.Model.Models;
using BlogCore.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.AutoMapper
{
    public class CustomProfile: Profile
    {
        /// <summary>
        /// 构造函数创建关系映射
        /// </summary>
        public CustomProfile()
        {
            //CreateMap <Tuple<CustomInfo,PartyAdvisoryInfo>,PartyAdvisoryViewModel>()
            //    .ForMember(a=>a.CustomId,opt=>opt.MapFrom(s=>s.Item1.Id))
            //    .ForMember(a=>a.AdvisoryId,opt=>opt.MapFrom(s=>s.Item2.Id))
            //    .ForMember(a => a.IsDelete, opt => opt.MapFrom(s => s.Item2.IsDelete));
            CreateMap<PartyAdvisoryViewModel,CustomInfo >()
                .ForMember(a => a.Id, opt => opt.MapFrom(s => s.CustomId))
                .ForMember(a=>a.Birthday,opt=>opt.MapFrom(s=>s.Age));
            CreateMap<PartyAdvisoryViewModel,PartyAdvisoryInfo>()
                .ForMember(a => a.Id, opt => opt.MapFrom(s => s.AdvisoryId))
                .ForMember(a=>a.CustomInfo,opt=>opt.MapFrom(s=>s.CustomId));
        }
    }
}
