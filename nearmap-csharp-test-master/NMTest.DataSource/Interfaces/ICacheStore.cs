namespace NMTest.DataSource.Interfaces
{
    public interface ICacheStore
    {
        object GetValue(string key);
        void StoreValue(string key, object value);
    }
}