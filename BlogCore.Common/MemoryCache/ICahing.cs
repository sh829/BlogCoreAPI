using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Common.MemoryCache
{
    /// <summary>
    /// 简单的缓存接口 只有查询和添加 以后扩展
    /// </summary>
    public interface ICahing
    {
        object Get(string cacheKey);
        void Set(string cacheKey, object cacheValue);
    }
}
