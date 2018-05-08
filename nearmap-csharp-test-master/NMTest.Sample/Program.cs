using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using NMTest.DataSource;
using NMTest.DataSource.Interfaces;

namespace NMTest.Sample
{
    public static class Program
    {
        public static void Main(string[] parameters)
        {
            var kernel = GetNinjectKernel();
            PopulateDatabaseStore(kernel);

            var dataSource = kernel.Get<IDataSource>();
            var random = new Random(0);

            var parallelLoop = Parallel.For(0, 10, item =>
                {
                    var stopwatch = Stopwatch.StartNew();

                    for (var j = 1; j <= 50; j++)
                    {
                        var key = GetRandomKey(random);
                        var value = dataSource.GetValue(key);

                        Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] '{key}', response '{value}', time: '{stopwatch.ElapsedMilliseconds}' ms");

                        stopwatch.Restart();
                    }
                });

            if (parallelLoop.IsCompleted)
            {
                Console.WriteLine("--- Finished executing. Press any key to exit ----");
                Console.ReadKey();
            }
        }

        private static string GetRandomKey(Random random)
        {
            var randomInt = random.Next(9);
            return $"key{randomInt}";
        }

        private static void PopulateDatabaseStore(StandardKernel kernel)
        {
            var databaseStore = kernel.Get<IStore>();

            Enumerable.Range(0, 9).ToList().ForEach(num => databaseStore.StoreValue($"key{num}", $"value{num}"));
        }

        private static StandardKernel GetNinjectKernel()
        {
            var kernel = new StandardKernel(new Bindings());
            return kernel;
        }
    }
}