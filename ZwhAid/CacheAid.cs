using System;
using System.Web;
using System.Web.Caching;

namespace ZwhAid
{
    /// <summary>
    /// Web Cache操作
    /// </summary>
    public class CacheAid
    {
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey">Cache键</param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {            
            Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey">Cache键</param>
        /// <param name="objObject">Cache值</param>
        public static void SetCache(string CacheKey, object objObject)
        {
            Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey">Cache键</param>
        /// <param name="objObject">Cache值</param>
        /// <param name="absoluteExpiration">有效时间</param>
        /// <param name="slidingExpiration">无操作过期时间</param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }
    }
}
