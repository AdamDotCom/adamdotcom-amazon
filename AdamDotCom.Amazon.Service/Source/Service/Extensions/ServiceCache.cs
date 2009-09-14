using System;
using System.Configuration;
using System.Web;
using System.Web.Caching;

namespace AdamDotCom.Amazon.Service.Extensions
{
    public static class ServiceCache
    {
        private static Cache cache = HttpRuntime.Cache;
        private static readonly bool enableCache = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableCaching"]);

        public static bool IsInCache(string key)
        {
            return GetFromCache(key) != null;
        }

        public static object GetFromCache(string key)
        {
            if (cache[key] != null)
            {
                return cache[key];
            }
            return null;
        }

        public static Wishlist AddToCache(this Wishlist wishlist, string listId)
        {
            AddToCache(listId, wishlist);
            
            return wishlist;
        }

        public static Reviews AddToCache(this Reviews reviews, string customerId)
        {
            AddToCache(customerId, reviews);

            return reviews;
        }

        public static Profile AddToCache(this Profile profile, string username)
        {
            AddToCache(username, profile);

            return profile;
        }

        private static void AddToCache(string key, object cacheObject)
        {
            if (enableCache)
            {
                cache.Insert(key, cacheObject, null, DateTime.Now.AddDays(1d), Cache.NoSlidingExpiration);
            }
        }
    }
}
