using BlogCore.Common.Helper;
using BlogCore.Common.Redis;
using BlogCore.IServices;
using BlogCore.Model;
using BlogCore.Model.Models;
using BlogCore.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers
{
   
    [Produces("application/json")]
    [Route("api/CustomInfo")]
    //[Authorize(Policy = "Admin")]
    public class CustomInfoController : Controller
    {
        private readonly ICustomInfoServices customInfoServices;
        private readonly IRedisCacheManager _redisCacheManager;
        public CustomInfoController(ICustomInfoServices _customInfoServices,IRedisCacheManager redisCacheManager)
        {
            customInfoServices = _customInfoServices;
            _redisCacheManager = redisCacheManager;
        }
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration =600)]
        public async Task<List<CustomInfoViewModel>> Get( int page=1,string key="")
        {
            List<CustomInfoViewModel> customInfos = new List<CustomInfoViewModel>();
            try
            {
                if (_redisCacheManager.Get<object>("Redis.CustomInfo") != null)
                {
                    customInfos = _redisCacheManager.Get<List<CustomInfoViewModel>>("Redis.CustomInfo");
                }
                else
                {
                    customInfos =await customInfoServices.GetCustomInfos();
                    _redisCacheManager.Set("Redis.CustomInfo", customInfos, TimeSpan.FromHours(2));
                }
            }catch(Exception ex)
            {
                customInfos = await customInfoServices.GetCustomInfos();

            }
            return customInfos;
        }
    }
}
