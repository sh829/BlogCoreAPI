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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExcelMapToModelController: Controller
    {
        readonly IExcelMapToModelServices _dictionaryServices; 
        public ExcelMapToModelController(IExcelMapToModelServices dictionaryServices)
        {
            _dictionaryServices = dictionaryServices;
        }
        /// <summary>
        /// 获取参数字典
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<ExcelMapToModel>>> Get(int page=1,string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            int intPageSize = 50;

            var data = await _dictionaryServices.QueryPage(a => a.ExcelColumn != null && a.ExcelColumn.Contains(key), page, intPageSize, " Id desc ");

            return new MessageModel<PageModel<ExcelMapToModel>>()
            {
                msg = "获取成功",
                success = data.dataCount >= 0,
                response = data
            };
        }
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody]ExcelMapToModel dictionary)
        {
            var data = new MessageModel<string>();

            var id = (await _dictionaryServices.Add(dictionary));
            data.success = id > 0;
            if (data.success)
            {
                data.response = id.ObjToString();
                data.msg = "添加成功";
            }
            return data;
        }
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody]ExcelMapToModel dictionary)
        {
            var data = new MessageModel<string>();
            if (dictionary != null && dictionary.Id > 0)
            {
                data.success = await _dictionaryServices.Update(dictionary);
                if (data.success)
                {
                    data.msg = "更新成功";
                    data.response = dictionary?.Id.ObjToString();
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
                var dictionary = await _dictionaryServices.QueryById(id);
                data.success = await _dictionaryServices.Delete(dictionary);
                if (data.success)
                {
                    data.msg = "删除成功";
                    data.response = dictionary?.Id.ObjToString();
                }
            }

            return data;
        }
    }
}
