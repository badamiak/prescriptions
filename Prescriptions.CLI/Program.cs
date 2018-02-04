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

        static Program()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        static void Main(string[] args)
        {
            string host = ConfigurationManager.AppSettings.Get("DbHost");

            Parser.Default.ParseArguments<CreateSchemaVerb, UpdateSchemaVerb, DeleteSchemaVerb, ImportToDatabaseVerb>(args)
                .WithParsed<CreateSchemaVerb>(o => new DatabaseSchemaOperations().CreateSchema(host))
                .WithParsed<UpdateSchemaVerb>(o => new DatabaseSchemaOperations().UpdateSchema(host))
                .WithParsed<DeleteSchemaVerb>(o => new DatabaseSchemaOperations().DeleteSchema(host))
                .WithParsed<ImportToDatabaseVerb>(Import)
                .WithNotParsed(errors => errors.ToList().ForEach(e => Console.WriteLine(e)));
        }

        private static void Import(ImportToDatabaseVerb options)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<PrescriptorModule>();
            var ImportService = containerBuilder.Build().Resolve<IImportToDBService>();

            ImportService.Import(options.FromFile);   
        }

    }
}
