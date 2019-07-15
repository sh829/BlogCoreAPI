using BlogCore.Common.Helper;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Common.Redis
{
     public class RedisCacheManger : IRedisCacheManager
    {
        private readonly string redisConnectionString;
        public volatile ConnectionMultiplexer redisConnection;
        private readonly object redisConnectionLock = new object();
        public RedisCacheManger()
        {
            //获取redis连接
            string redisConfiguration = Appsettings.app("AppSettings", "RedisCaching", "ConnectionString");
            if (string.IsNullOrWhiteSpace(redisConfiguration))
            {
                throw new ArgumentException("redis config is empty", nameof(redisConfiguration));
            }
            redisConnectionString = redisConfiguration;
            redisConnection = GetRedisConnection();
        }
        //因为在开发中，通过connectionmultiplexer频繁的连接关闭服务，很占内存资源，所以使用单例模式来实现
        /// <summary>
        /// 核心代码 获取连接实例
        /// 通过双if  加lock的方式，实现单例模式
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetRedisConnection()
        {
            //如果已经连接实例，直接返回
            if (redisConnection!=null && redisConnection.IsConnected)
            {
                return redisConnection;
            }
            //加锁防止异步编程中，出现单例无效的问题
            lock (redisConnectionLock)
            {
                if (redisConnection != null)
                {
                    //释放redis连接
                    redisConnection.Dispose();
                }
                try
                {
                    redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);
                }catch(Exception ex)
                {
                    throw new Exception("Redis服务未启用，请开启该服务，并且请注意端口号，本项目使用的的6379，而且我的是没有设置密码。");
                }

            }
            return redisConnection;
        }
        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            foreach(var endPoint in GetRedisConnection().GetEndPoints())
            {
                var server = GetRedisConnection().GetServer(endPoint);
                foreach(var key in server.Keys())
                {
                    redisConnection.GetDatabase().KeyDelete(key);
                }
            }
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TEntity Get<TEntity>(string key)
        {
            var value = redisConnection.GetDatabase().StringGet(key);
            if (value.HasValue)
            {
                //需要反序列化，把redis存的byte[]，进行反序列化
                return SerializeHelper.Deserialize<TEntity>(value);
            }
            return default(TEntity);
        }
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Get(string key)
        {
            return redisConnection.GetDatabase().KeyExists(key);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            return redisConnection.GetDatabase().StringGet(key);
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            redisConnection.GetDatabase().KeyDelete(key);
        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        public void Set(string key, object value, TimeSpan cacheTime)
        {
             redisConnection.GetDatabase().StringSet(key,SerializeHelper.Serialize(value), cacheTime);
        }
        public  void SetValue(string key ,byte[] value) 
        {
            redisConnection.GetDatabase().StringSet(key, value, TimeSpan.FromSeconds(120));
        }

    }
}
