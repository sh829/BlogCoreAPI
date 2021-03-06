﻿using BlogCore.Common.Helper;
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
    /// 派对客户需求物品
    /// </summary>
    [Route("api/PartyDemands")]
    public class PartyDemandsController : Controller
    {
        private readonly IPartyDemandsServices _partyAdvisoryInfoServices;
        public PartyDemandsController(IPartyDemandsServices partyAdvisoryInfoServices)
        {
            _partyAdvisoryInfoServices = partyAdvisoryInfoServices;
        }
        [HttpGet]
        public async Task<PartyDemands> Get(int id )
        {

           return await _partyAdvisoryInfoServices.QueryById(id);
        }
        /// <summary>
        /// 添加预定信息
        /// </summary>
        /// <param name="advisoryInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post(PartyDemands advisoryInfo)
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
        public async Task<MessageModel<string>> Put(PartyDemands advisoryInfo)
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
