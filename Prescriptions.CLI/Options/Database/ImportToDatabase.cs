using CommandLine;

namespace Prescriptions.CLI.Options.Database
{
    [Verb("import")]
    public sealed class ImportToDatabase
    {
        public string FromFile { get; set; }
    }
}
