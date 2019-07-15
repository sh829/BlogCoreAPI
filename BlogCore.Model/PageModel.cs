using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model
{
    public class PageModel<T>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int page { get; set; } = 1;
        /// <summary>
        /// 总页数
        /// </summary>
        public int pageCount { get; set; } = 6;
        /// <summary>
        /// 数据总数
        /// </summary>
        public int dateCount { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> data { get; set; }

    }
}
