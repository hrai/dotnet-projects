using System.Collections.Generic;
using System.Threading;
using NMTest.DataSource.Interfaces;

namespace NMTest.DataSource
{
    public class DatabaseStore : IStore
    {
        private readonly IDictionary<string, object> _values = new Dictionary<string, object>();

        public virtual object GetValue(string key)
        {
            //simulates 500 ms roundtrip to the database
            Thread.Sleep(500);
            object value;
            if (_values.TryGetValue(key, out value))
            {
                return value;
            }
            return null;
        }

        public virtual void StoreValue(string key, object value)
        {
            //simulates 500 ms roundtrip to the database
            Thread.Sleep(500);
            _values[key] = value;
        }
    }
}