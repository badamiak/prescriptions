using CommandLine;

namespace Prescriptions.CLI.Options.Database
{
    [Verb("import")]
    public sealed class ImportToDatabaseVerb
    {
        [Option('f',nameof(FromFile),HelpText = "Source file for import")]
        public string FromFile { get; set; }
    }
}
