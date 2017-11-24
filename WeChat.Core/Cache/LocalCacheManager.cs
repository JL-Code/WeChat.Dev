using Senparc.Weixin.Cache;
using System;

namespace Zap.WeChat.SDK.Cache
{
    public class LocalCacheManager
    {
        static LocalObjectCacheStrategy _cache = LocalObjectCacheStrategy.Instance;

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Add(string key, object value)
        {
            _cache.InsertToCache(key, value);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            if (_cache.CheckExisted(key))
            {
                return _cache.Get(key);
            }
            return null;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            var result = default(T);
            try
            {
                var item = Get(key);
                if (item != null)
                    result = (T)item;
            }
            catch (Exception)
            { }
            return result;
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Update(string key, object value)
        {
            _cache.Update(key, value);
        }
    }
}
