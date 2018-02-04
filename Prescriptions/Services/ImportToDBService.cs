using Prescriptions.Database;

namespace Prescriptions.Services
{

    public class ImportToDBService
    {
        private DrugsImportService xmlImportService;
        private DatabaseAccess database;

        public ImportToDBService(DrugsImportService xmlImportService, DatabaseAccess database)
        {

        }
        //public Import(string fromFile)
        //{
        //    var importedDrugs = xmlImportService.Import(fromFile);
        //    var existingDrugs = 
        //}

    }
}
