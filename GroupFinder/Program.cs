using Models;
using System;
using Services;
using Lookups;
using Autofac;

namespace GroupFinder
{
    class Program
    {
        private static IContainer Container { get;set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<QueueService>().As<IQueueService>();
            builder.RegisterType<DungeonFinderService>();
            Container = builder.Build();
            
            using (var scope = Container.BeginLifetimeScope())
            {
                var service  = scope.Resolve<DungeonFinderService>();
                service.Start();
            }
            
        }
    }
}
