using Senparc.Weixin.Cache;

namespace WeChat.Core.Cache
{
    public class LocalCacheManager
    {
        static LocalObjectCacheStrategy _cache = LocalObjectCacheStrategy.Instance;

        public static void Add(string key, object value)
        {
            _cache.InsertToCache(key, value);
        }

        public static object GetItem(string key)
        {
            if (_cache.CheckExisted(key))
            {
                return _cache.Get(key);
            }
            return null;
        }


        public static T Get<T>(string key)
        {
            var result = default(T);
            var item = GetItem(key);
            //TODO: 泛型转换为值类型

            return result;
        }
    }
}
