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
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    //[Authorize(Policy = "Admin")]
    public class GraduationStatisticsController : Controller
    {
        private readonly IGraduationStatisticsServices graduationStatisticsServices;
        private readonly IDictionaryServices _dictionaryServices;
        private readonly IRedisCacheManager _redisCacheManager;
        public GraduationStatisticsController(IGraduationStatisticsServices _graduationStatisticsServices, IDictionaryServices dictionaryServices, IRedisCacheManager redisCacheManager)
        {
            graduationStatisticsServices = _graduationStatisticsServices;
            _dictionaryServices = dictionaryServices;
            _redisCacheManager = redisCacheManager;
        }
        /// <summary>
        /// 获取学生信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<GraduationStatistics>>> Get(int page = 1, int pageSize = 20, string Name = "",string Class="",string CurrentCity="")
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
            {
                Name = "";
            }
            if (string.IsNullOrEmpty(Class) || string.IsNullOrWhiteSpace(Class))
            {
                Class = "";
            }
            if (string.IsNullOrEmpty(CurrentCity) || string.IsNullOrWhiteSpace(CurrentCity))
            {
                CurrentCity = "";
            }
            PageModel<GraduationStatistics> customInfos = new PageModel<GraduationStatistics>();
            try
            {
                customInfos = await graduationStatisticsServices.QueryPage(a => a.IsDelete != true && (a.Name != "" && a.Name.Contains(Name)&&(a.CurrentCity!=""&&a.CurrentCity.Contains(CurrentCity))&&((Class!=""&&a.Class==Class)||Class=="")), page);
                return new MessageModel<PageModel<GraduationStatistics>>
                {
                    msg = "获取成功",
                    response = customInfos,
                    success = customInfos.dataCount >= 0
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<PageModel<GraduationStatistics>>();
            }

        }
        /// <summary>
        /// 添加顾客信息 无权限
        /// </summary>
        /// <param name="custom"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody]GraduationStatistics custom)
        {
            var data = new MessageModel<string>();
            custom.CreateTime = DateTime.Now;
            custom.IsDelete = false;
            var classInfo=await _dictionaryServices.Query(a => a.Category == "Class" && a.Code == custom.Class);
            custom.ClassName = classInfo.FirstOrDefault()?.Value;
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
        public async Task<MessageModel<string>> Delete(int id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var custom = await graduationStatisticsServices.QueryById(id);
                custom.IsDelete = true;
                data.success = await graduationStatisticsServices.Update(custom);
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
