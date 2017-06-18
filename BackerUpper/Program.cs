using System;
using System.Configuration;
using System.Threading;
using Autofac;
using NLog;

namespace BackerUpper
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            var container = program.InitializeIoC();
            var service = container.Resolve<IBackupManager>();
            CancellationTokenSource tokenProvider = new CancellationTokenSource();
            var task = service.Backup(tokenProvider.Token);
            task.Wait(CancellationToken.None);
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
            Console.WriteLine("Shutting down...");
            tokenProvider.Cancel();
        }

        IContainer InitializeIoC()
        {
            Autofac.ContainerBuilder builder = new Autofac.ContainerBuilder();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => !t.Name.StartsWith("System"))
                .AsImplementedInterfaces();
            builder.Register(context => LogManager.GetLogger("BackerUpper")).As<ILogger>();
            builder.Register(context =>
            {
                var config = ConfigurationManager.GetSection("backerUpper");
                return config as BackerUpper.Configuration.Configuration;
            }).As<BackerUpper.Configuration.IConfiguration>();
            return builder.Build();
        }
    }
}
