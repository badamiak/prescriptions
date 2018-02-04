using Prescriptions.API.Model.Drugs;

namespace Prescriptions.API.Services
{
    public interface IDrugsImportService
    {
        DrugsCollection Import(string xmlFilePath);
    }
}