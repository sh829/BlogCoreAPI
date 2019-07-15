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
            CreateMap<CustomInfo, CustomInfoViewModel>();
        }
    }
}
