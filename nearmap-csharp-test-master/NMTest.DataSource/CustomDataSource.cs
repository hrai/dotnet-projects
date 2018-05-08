using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using NMTest.DataSource.Interfaces;

namespace NMTest.DataSource
{
    public class CustomDataSource : IDataSource
    {
        private ICacheStore _distributedCacheStore;
        private IStore _databaseStore;
        private readonly IDictionary<string, object> _values = new ConcurrentDictionary<string, object>();

        public CustomDataSource(ICacheStore distributedCacheStore, IStore databaseStore)
        {
            _distributedCacheStore = distributedCacheStore;
            _databaseStore = databaseStore;
        }

        public object GetValue(string key)
        {
            object value = GetValueFromLocalCache(key);

            if (value == null)
            {
                value = _distributedCacheStore.GetValue(key);

                if (value == null)
                {
                    value = _databaseStore.GetValue(key);
                    _distributedCacheStore.StoreValue(key, value);
                    _values[key] = value;
                }
            }

            return value;
        }

        public virtual object GetValueFromLocalCache(string key)
        {
            Thread.Sleep(30);

            object value;
            if (_values.TryGetValue(key, out value))
                return value;

            return null;
        }
    }
}