using BlogCore.Common.Helper;
using BlogCore.IServices;
using BlogCore.Model;
using BlogCore.Model.Models;
using BlogCore.Model.ViewModels;
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
    [ApiController]
    public class PartyBookInfoController:Controller
    {
        private readonly IPartyBookInfoServices _partyBookInfoServices;
        public PartyBookInfoController(IPartyBookInfoServices partyBookInfoServices)
        {
            _partyBookInfoServices = partyBookInfoServices;
        }
        [HttpGet]
        public async Task<MessageModel<PageModel<PartyBookInfoViewModel>>> Get(int page=1,int pageSize=20,string key="" )
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            var bookInfos = await _partyBookInfoServices.GetBookInfos(page,pageSize,key);
            PageModel<PartyBookInfoViewModel> bookInfoModel = new PageModel<PartyBookInfoViewModel> {
                data = bookInfos,
                dataCount = bookInfos.Count
            };

            return new MessageModel<PageModel<PartyBookInfoViewModel>>
            {
                msg = "获取成功",
                response = bookInfoModel,
                success = true
            };
        }
        /// <summary>
        /// 添加预定信息
        /// </summary>
        /// <param name="advisoryInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post(PartyBookInfo advisoryInfo)
        {
            MessageModel<string> data = new MessageModel<string>();
            advisoryInfo.CreateTime = DateTime.Now;
            advisoryInfo.UpdateTime = DateTime.Now;
            var id =await _partyBookInfoServices.Add(advisoryInfo);
            data.success = id > 0;
            if (data.success)
            {
                data.msg = "添加成功";
                data.response = id.ObjToString();
            }
            return data;
        }
        [HttpPut]
        public async Task<MessageModel<string>> Put(PartyBookInfo advisoryInfo)
        {
            var data = new MessageModel<string>();
            data.success=await _partyBookInfoServices.Update(advisoryInfo);
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
                data.success = await _partyBookInfoServices.DeleteById(id);
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
