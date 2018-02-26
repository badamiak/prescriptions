using Prescriptions.API.Model.Drugs;
using System.Linq;
using System;
using System.Collections.Generic;
using Prescriptions.API.Services;
using log4net;

namespace Prescriptions.Services
{

    public class ImportToDBService : IImportToDBService
    {
        private IDrugsImportService xmlImportService;
        private IDatabaseAccess database;
        private readonly ILog Logger = LogManager.GetLogger(typeof(ImportToDBService));

        public ImportToDBService(IDrugsImportService xmlImportService, IDatabaseAccess databaseAccess)
        {
            this.xmlImportService = xmlImportService;
            this.database = databaseAccess;

        }

        public void Import(string fromFile)
        {
            var importedDrugs = this.xmlImportService.Import(fromFile);
            var existingDrugs = this.database.GetAllEntitiesOfType<Drug>().Where(x => x.IsActive).ToList();

            var existingDrugsBL7 = existingDrugs.Select(x => x.BL7).ToList();
            var importedDrugsBL7 = importedDrugs.Drugs.Select(x => x.BL7).ToList();

            this.Logger.Info("Searching for new drugs");
            var newDrugs = importedDrugs.Drugs.AsParallel().Where(x => !existingDrugsBL7.Contains(x.BL7)).ToList();
            this.Logger.Info($"New drugs: {newDrugs.Count}");
            this.Logger.Debug($"New drugs: [{string.Join(",",newDrugs.Select(x=>x.BL7))}]");

            this.Logger.Info("Searching for removed drugs");
            var removedDrugs = existingDrugs.AsParallel().Where(x => !importedDrugsBL7.Contains(x.BL7)).ToList();
            this.Logger.Info($"Removed drugs: {removedDrugs.Count}");
            this.Logger.Debug($"Removed drugs: [{string.Join(",",removedDrugs.Select(x=>x.BL7))}]");

            var newAndRemovedDrugs = newDrugs.Concat(removedDrugs).ToList();

            var possiblyChangedDrugs = importedDrugs.Drugs.Where(x => !newAndRemovedDrugs.Contains(x));

            this.Logger.Info("Searching for changes drugs");

            var changedDrugs = GetChangedDrugs(possiblyChangedDrugs, existingDrugs);
            this.Logger.Info($"Changed drugs: {changedDrugs.Count}");
            this.Logger.Debug($"Changed drugs: [{string.Join(",", changedDrugs.Select(x => x.BL7))}]");

            Logger.Info("Deactivating removed drugs");
            MarkRemovedDrugsAsInactive(removedDrugs);
            Logger.Info("DONE: Deactivating removed drugs");

            Logger.Info("Saving new drugs");
            PersistNewDrugs(newDrugs);
            Logger.Info("DONE: Saving new drugs");


            Logger.Info("Saving changed drugs");
            PersistChangedDrugs(changedDrugs, importedDrugs.Drugs);
            Logger.Info("DONE: Saving changed drugs");
        }

        private void PersistNewDrugs(List<Drug> newDrugs)
        {
            this.database.SaveBatch(newDrugs);
        }

        private void MarkRemovedDrugsAsInactive(List<Drug> removedDrugs)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            removedDrugs.ForEach(x => { x.IsActive = false; x.InactiveSince = timestamp; });

            this.database.SaveBatch(removedDrugs);
        }

        private List<Drug> GetChangedDrugs(IEnumerable<Drug> possiblyChangedDrugsFromImport, List<Drug> existingDrugs)
        {

            var changedBL7 = possiblyChangedDrugsFromImport.AsParallel().Where(x => existingDrugs.Where(e => e.BL7 == x.BL7).FirstOrDefault()?.HasChangedAccordingTo(x) ?? false).Select(x=>x.BL7).ToList();
            return existingDrugs.Where(x=>changedBL7.Contains(x.BL7)).ToList();
        }

        private void PersistChangedDrugs(IEnumerable<Drug> changedExistingDrugs, List<Drug> importedDrugs)
        {
            var changedBL7 = changedExistingDrugs.Select(x => x.BL7).ToList();

            MarkRemovedDrugsAsInactive(changedExistingDrugs.ToList());
            this.database.SaveBatch(importedDrugs.Where(x => changedBL7.Contains(x.BL7)));
        }
    }

}
