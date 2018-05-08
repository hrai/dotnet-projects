using Ninject.Modules;
using NMTest.DataSource.Interfaces;

namespace NMTest.DataSource
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IStore>().To<DatabaseStore>().InSingletonScope();
            Bind<ICacheStore>().To<DistributedCacheStore>().InSingletonScope();
            Bind<IDataSource>().To<CustomDataSource>();
        }
    }
}
