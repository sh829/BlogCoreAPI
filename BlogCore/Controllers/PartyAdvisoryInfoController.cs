using BlogCore.Common.Helper;
using BlogCore.IServices;
using BlogCore.Model;
using BlogCore.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers
{
    /// <summary>
    /// 派对咨询信息
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class PartyAdvisoryInfoController:Controller
    {
        private readonly IPartyAdvisoryInfoServices _partyAdvisoryInfoServices;
        public PartyAdvisoryInfoController(IPartyAdvisoryInfoServices partyAdvisoryInfoServices)
        {
            _partyAdvisoryInfoServices = partyAdvisoryInfoServices;
        }
        /// <summary>
        /// 获取全部信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>

        [HttpGet]
        [ResponseCache(Duration = 3600)]
        public async Task<MessageModel<PageModel<PartyAdvisoryInfo>>> Get(int page = 1, string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }

            int intPageSize = 50;

            var data = await _partyAdvisoryInfoServices.QueryPage(a => a.IsDelete != true, page, intPageSize, " Id desc ");

            return new MessageModel<PageModel<PartyAdvisoryInfo>>()
            {
                msg = "获取成功",
                success = data.dataCount >= 0,
                response = data
            };

        }
        [HttpGet]
        public async Task<PartyAdvisoryInfo> Get(int id )
        {

           return await _partyAdvisoryInfoServices.QueryById(id);
        }
        /// <summary>
        /// 添加预定信息
        /// </summary>
        /// <param name="advisoryInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post(PartyAdvisoryInfo advisoryInfo)
        {
            MessageModel<string> data = new MessageModel<string>();
            advisoryInfo.CreateTime = DateTime.Now;
            advisoryInfo.UpdateTime = DateTime.Now;
            var id =await _partyAdvisoryInfoServices.Add(advisoryInfo);
            data.success = id > 0;
            if (data.success)
            {
                data.msg = "添加成功";
                data.response = id.ObjToString();
            }
            return data;
        }
        [HttpPut]
        public async Task<MessageModel<string>> Put(PartyAdvisoryInfo advisoryInfo)
        {
            var data = new MessageModel<string>();
            data.success=await _partyAdvisoryInfoServices.Update(advisoryInfo);
            if (data.success)
            {
                data.msg = "修改成功";
                data.response = advisoryInfo?.Id.ObjToString();
            }
            return data;
        }
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(int id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                data.success = await _partyAdvisoryInfoServices.DeleteById(id);
                if (data.success)
                {
                    data.msg = "删除成功";
                    data.response = id.ObjToString();
                }
            }
           
            return data;
        }
 
    }
}
