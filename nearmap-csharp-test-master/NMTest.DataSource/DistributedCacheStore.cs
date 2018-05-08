using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using NMTest.DataSource.Interfaces;

namespace NMTest.DataSource
{
    public class DistributedCacheStore : ICacheStore
    {
        private readonly IDictionary<string, object> _values = new ConcurrentDictionary<string, object>();

        public virtual object GetValue(string key)
        {
            //simulates 100 ms roundtrip to the distributed cache
            Thread.Sleep(100);
            object value;
            if (_values.TryGetValue(key, out value))
            {
                return value;
            }
            return null;
        }

        public virtual void StoreValue(string key, object value)
        {
            //simulates 100 ms roundtrip to the distributed cache
            Thread.Sleep(100);
            _values[key] = value;
        }
    }
}
