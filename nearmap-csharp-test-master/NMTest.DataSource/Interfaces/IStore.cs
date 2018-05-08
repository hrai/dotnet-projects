namespace NMTest.DataSource.Interfaces
{
    public interface IStore
    {
        object GetValue(string key);
        void StoreValue(string key, object value);
    }
}