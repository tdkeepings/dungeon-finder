using Models;
using System;
using Services;
using Lookups;
using Autofac;

namespace GroupFinder
{
    class Program
    {
        // private is fine, because Containers should only matter for the scope of this class.
        // any other container should be defined inside the concrete type, for its "children"
        private static IContainer Container { get;set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            // only need to define what's going to get used in the scope of this class
            builder.RegisterType<QueueService>().As<IQueueService>().SingleInstance();
            builder.RegisterType<DungeonFinderService>().As<IDungeonFinderService>();
            builder.RegisterType<AppService>();

            // build it
            Container = builder.Build();
            
            //use it
            using (var scope = Container.BeginLifetimeScope())
            {
                var service  = scope.Resolve<AppService>();
                service.Start();
            }
        }
    }
}
