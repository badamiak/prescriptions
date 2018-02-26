using Autofac;
using CommandLine;
using log4net;
using Prescriptions.API.Services;
using Prescriptions.CLI.Options.Database;
using Prescriptions.Database;
using System;
using System.Configuration;
using System.Linq;

namespace Prescriptions.CLI
{
    class Program
    {
        private static ILog Logger = LogManager.GetLogger(typeof(Program));
        private static string Host = ConfigurationManager.AppSettings.Get("DbHost");
        

        static Program()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        static void Main(string[] args)
        {
            var assembly = typeof(Program).Assembly;
            Logger.Info($"{assembly.FullName}");
            Logger.Info($"Version: [{assembly.GetName().Version}] Compiled at: [{new System.IO.FileInfo(assembly.Location).LastWriteTimeUtc}]");

            Parser.Default.ParseArguments<CreateSchemaVerb, UpdateSchemaVerb, DeleteSchemaVerb, ImportToDatabaseVerb>(args)
                .WithParsed<CreateSchemaVerb>(o => new DatabaseSchemaOperations().CreateSchema(Host))
                .WithParsed<UpdateSchemaVerb>(o => new DatabaseSchemaOperations().UpdateSchema(Host))
                .WithParsed<DeleteSchemaVerb>(o => new DatabaseSchemaOperations().DeleteSchema(Host))
                .WithParsed<ImportToDatabaseVerb>(Import)
                .WithNotParsed(errors => errors.ToList().ForEach(e => Console.WriteLine(e)));
        }

        private static void Import(ImportToDatabaseVerb options)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<PrescriptorModule>();
            var container = containerBuilder.Build();

            container.Resolve<IDatabaseAccess>().InitDbConnection(Host);

            var ImportService = container.Resolve<IImportToDBService>();
            ImportService.Import(options.FromFile);   
        }

    }
}
