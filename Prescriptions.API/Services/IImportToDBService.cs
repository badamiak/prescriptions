namespace Prescriptions.API.Services
{
    public interface IImportToDBService
    {
        void Import(string fromFile);
    }
}