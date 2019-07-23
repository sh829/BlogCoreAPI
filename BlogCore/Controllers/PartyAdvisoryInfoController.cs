using AutoMapper;
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
    /// <summary>
    /// 派对咨询信息
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class PartyAdvisoryInfoController : Controller
    {
        private readonly IPartyAdvisoryInfoServices _partyAdvisoryInfoServices;
        private readonly IRedisCacheManager _redisCacheManager;
        readonly ICustomInfoServices _customInfoServices;
        IMapper _mapper;
        public PartyAdvisoryInfoController(IPartyAdvisoryInfoServices partyAdvisoryInfoServices, ICustomInfoServices customInfoServices, IMapper mapper,IRedisCacheManager redisCacheManager)
        {
            _partyAdvisoryInfoServices = partyAdvisoryInfoServices;
            _customInfoServices = customInfoServices;
            _redisCacheManager = redisCacheManager;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取咨询信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<PageModel<PartyAdvisoryViewModel>>> Get(int page = 1, int pageSize = 20, string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            PageModel<PartyAdvisoryViewModel> returnInfos = new PageModel<PartyAdvisoryViewModel>();
            var customInfo = await _customInfoServices.Query(a => a.IsDelete != true && a.Name.Contains(key));
            var advosiry = await _partyAdvisoryInfoServices.Query(a => a.IsDelete != true);
            try
            {
                var infos = (from c in customInfo
                             from ad in advosiry.Where(a => a.CustomInfo == c.Id).DefaultIfEmpty()
                             select new PartyAdvisoryViewModel
                             {
                                 CustomId = c.Id,
                                 AdvisoryId = ad?.Id,
                                 Name = c.Name,
                                 Age = c.Birthday,
                                 PhoneNumber = c.PhoneNumber,
                                 AdvisoryTime = c.AdvisoryTime,
                                 PredictNumber = ad?.PredictNumber,
                                 Deposit = ad?.Deposit,
                                 DepositTime = ad?.DepositTime,
                                 Source = ad?.Source,
                                 FirstInfo = ad?.FirstInfo,
                                 SecondInfo = ad?.SecondInfo,
                                 ThirdInfo = ad?.ThirdInfo,
                                 Remark = ad?.Remark,
                                 IsDelete = ad?.IsDelete
                             });
                returnInfos.data = infos.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                returnInfos.dataCount = infos.Count();
                return new MessageModel<PageModel<PartyAdvisoryViewModel>> {
                    msg = "获取成功",
                    response = returnInfos,
                    success = returnInfos.dataCount >= 0
                };
            }
            catch 
            {
                return new MessageModel<PageModel<PartyAdvisoryViewModel>>();
            }
        }

        //[HttpGet]
        //[ResponseCache(Duration = 3600)]
        //public async Task<MessageModel<PageModel<PartyAdvisoryInfo>>> Get(int page = 1, string key = "")
        //{
        //    if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
        //    {
        //        key = "";
        //    }

        //    int intPageSize = 50;

        //    var data = await _partyAdvisoryInfoServices.QueryPage(a => a.IsDelete != true, page, intPageSize, " Id desc ");

        //    return new MessageModel<PageModel<PartyAdvisoryInfo>>()
        //    {
        //        msg = "获取成功",
        //        success = data.dataCount >= 0,
        //        response = data
        //    };

        //}

        /// <summary>
        /// 添加咨询信息
        /// </summary>
        /// <param name="infos"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] PartyAdvisoryViewModel infos)
        {
            var data = new MessageModel<string>();
            try
            {
                if (infos != null)
                {
                    var customInfo = _mapper.Map<CustomInfo>(infos);
                    var advisoryInfo = _mapper.Map<PartyAdvisoryInfo>(infos);
                    var cResult =await _customInfoServices.Add(customInfo);
                    advisoryInfo.CustomInfo = cResult;
                    var aResult =await _partyAdvisoryInfoServices.Add(advisoryInfo);
                    if (cResult>0 && aResult>0)
                    {
                        data.msg = "添加成功";
                        data.success = true;
                        data.response = aResult.ObjToString();
                    }
                }
            }catch(Exception ex)
            {

            }
            return data;
        }
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] PartyAdvisoryViewModel Infos)
        {
            var data = new MessageModel<string>();
            try
            {
                var customInfo = _mapper.Map<CustomInfo>(Infos);
                var advisoryInfo = _mapper.Map<PartyAdvisoryInfo>(Infos);
                var cResult = await _customInfoServices.Update(customInfo);
                var aResult = true;
                if (advisoryInfo.Id > 0)
                {
                    aResult = await _partyAdvisoryInfoServices.Update(advisoryInfo);
                }
                if (cResult && aResult)
                {
                    data.success = cResult;
                }
                if (data.success)
                {
                    data.msg = "修改成功";
                    data.response = advisoryInfo?.Id.ObjToString();
                }
                return data;
            }
            catch(Exception ex)
            {
                return data;
            }
            
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
