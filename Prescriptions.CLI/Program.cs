using CommandLine;
using Prescriptions.CLI.Options.Database;
using Prescriptions.Database;
using System;
using System.Configuration;
using System.Linq;

namespace Prescriptions.CLI
{
    class Program
    {
        static Program()
        {
            log4net.Config.XmlConfigurator.Configure();

        }
        static void Main(string[] args)
        {
            string host = ConfigurationManager.AppSettings.Get("DbHost");

            Parser.Default.ParseArguments<CreateSchemaVerb, UpdateSchemaVerb,DeleteSchemaVerb>(args)
                .WithParsed<CreateSchemaVerb>(o=> new DatabaseSchemaOperations().CreateSchema(host))
                .WithParsed<UpdateSchemaVerb>(o => new DatabaseSchemaOperations().UpdateSchema(host))
                .WithParsed<DeleteSchemaVerb>(o => new DatabaseSchemaOperations().DeleteSchema(host))
                .WithNotParsed(errors => errors.ToList().ForEach(e => Console.WriteLine(e)));
        }

    }
}
