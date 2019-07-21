using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogCore.Common;
using BlogCore.Common.Attributes;
using BlogCore.IRepository;
using BlogCore.IServices;
using BlogCore.Model.Models;
using BlogCore.Services.Base;

namespace Blog.Core.Services
{
    public class TopicServices: BaseServices<Topic>, ITopicServices
    {

        ITopicRepository _dal;
        public TopicServices(ITopicRepository dal)
        {
            this._dal = dal;
            base.baseDal = dal;
        }

        /// <summary>
        /// 获取开Bug专题分类（缓存）
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 60)]
        public async Task<List<Topic>> GetTopics()
        {
            return await base.Query(a => !a.tIsDelete && a.tSectendDetail == "tbug");
        }

    }
}
