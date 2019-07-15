using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Common.Redis
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface IRedisCacheManager
    {
        //获取redis的值
        string GetValue(string key );
        //获取值并序列化
        TEntity Get<TEntity>(string key);
        //保存
        void Set(string key, object value, TimeSpan cacheTime);
        //判断是否存在
        bool Get(string key);
        //移除某个缓存值
        void Remove(string key);
        //全部清除
        void Clear();
    }
}
