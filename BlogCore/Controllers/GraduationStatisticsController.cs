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
    [Route("api/GraduationStatistics")]
    //[Authorize(Policy = "Admin")]
    public class GraduationStatisticsController : Controller
    {
        private readonly IGraduationStatisticsServices graduationStatisticsServices;
        private readonly IRedisCacheManager _redisCacheManager;
        public GraduationStatisticsController(IGraduationStatisticsServices _graduationStatisticsServices, IRedisCacheManager redisCacheManager)
        {
            graduationStatisticsServices = _graduationStatisticsServices;
            _redisCacheManager = redisCacheManager;
        }
        /// <summary>
        /// 获取学生信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<PageModel<GraduationStatistics>> Get( int page=1,string key="")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            PageModel<GraduationStatistics> customInfos = new PageModel<GraduationStatistics>();
            try
            {
                    customInfos = await graduationStatisticsServices.QueryPage(a => a.IsDelete != true && (a.Name != "" && a.Name.Contains(key)), page);
                    _redisCacheManager.Set("Redis.CustomInfo", customInfos, TimeSpan.FromHours(2));
            }catch(Exception ex)
            {
                
            }
            return customInfos;
        }
        /// <summary>
        /// 添加顾客信息 无权限
        /// </summary>
        /// <param name="custom"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<MessageModel<string>> Post([FromBody]GraduationStatistics custom)
        {
            var data = new MessageModel<string>();
            custom.CreateTime = DateTime.Now;
            custom.IsDelete = false;
            var id = await graduationStatisticsServices.Add(custom);
            data.success = id > 0;
            if (data.success)
            {
                data.response = id.ObjToString();
                data.msg = "添加成功";
            }
            return data;
        }
        /// <summary>
        /// 修改顾客信息
        /// </summary>
        /// <param name="custom"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] GraduationStatistics custom)
        {
            var data = new MessageModel<string>();
            if (custom != null && custom.Id > 0)
            {
                data.success = await graduationStatisticsServices.Update(custom);
                if (data.success)
                {
                    data.response = custom?.Id.ObjToString();
                    data.msg = "修改成功";
                }
            }
            return data;
        }
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(int id )
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var custom = await graduationStatisticsServices.QueryById(id);
                custom.IsDelete = true;
                data.success =await graduationStatisticsServices.Update(custom);
                if (data.success)
                {
                    data.response = custom?.Id.ObjToString();
                    data.msg = "删除成功";
                }
            }
            return data;
        }
    }
}
