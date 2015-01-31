using System;
using System.Web.Caching;
using System.Web;

namespace Naif.Core.Caching
{
    public class MemoryCache : ICacheProvider
    {
        #region Private Members

        private static readonly Cache Cache = GetCache();

        #endregion

        #region Private Methods

        private static Cache GetCache()
        {
            HttpContext ctx = HttpContext.Current;
            return ctx != null ? ctx.Cache : HttpRuntime.Cache;
        }

        #endregion

        #region ICacheProvider Members

        public object this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Insert(key, value);
            }
        }

        public object Get(string key)
        {
            return Cache.Get(key);
        }

        public void Insert(string key, object value, DateTime absoluteExpiration)
        {
            Cache.Insert(key, value, null, absoluteExpiration, TimeSpan.Zero);
        }

        public void Insert(string key, object value)
        {
            Cache.Insert(key, value);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        #endregion
    }
}
