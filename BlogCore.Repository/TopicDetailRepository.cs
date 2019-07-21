using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogCore.IRepository;
using BlogCore.Model.Models;
using BlogCore.Repository.Base;

namespace Blog.Core.Repository
{
    public class TopicDetailRepository: BaseRespository<TopicDetail>, ITopicDetailRepository
    {
    }
}
