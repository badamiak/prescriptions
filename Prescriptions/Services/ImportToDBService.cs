﻿using Prescriptions.API.Model.Drugs;
using System.Linq;
using System;
using System.Collections.Generic;
using Prescriptions.API.Services;

namespace Prescriptions.Services
{

    public class ImportToDBService : IImportToDBService
    {
        private IDrugsImportService xmlImportService;
        private IDatabaseAccess database;

        public ImportToDBService(IDrugsImportService xmlImportService, IDatabaseAccess databaseAccess)
        {
            this.xmlImportService = xmlImportService;
            this.database = databaseAccess;

        }

        public void Import(string fromFile)
        {
            var importedDrugs = xmlImportService.Import(fromFile);
            var existingDrugs = database.GetAllEntitiesOfType<Drug>().Where(x => x.IsActive).ToList();

            var existingDrugsEans = existingDrugs.Select(x => x.EAN).ToList();
            var importedDrugsEans = importedDrugs.Drugs.Select(x => x.EAN).ToList();

            var newDrugs = importedDrugs.Drugs.Where(x => !existingDrugsEans.Contains(x.EAN)).ToList();
            var removedDrugs = existingDrugs.Where(x => !importedDrugsEans.Contains(x.EAN)).ToList();

            var newAndRemovedDrugs = newDrugs.Concat(removedDrugs).ToList();

            var possiblyChangedDrugs = importedDrugs.Drugs.Where(x => !newAndRemovedDrugs.Contains(x));

            MarkRemovedDrugsAsInactive(removedDrugs);

            PersistNewDrugs(newDrugs);

            var changedDrugs = GetChangedDrugs(possiblyChangedDrugs, existingDrugs);
            PersistChangedDrugs(changedDrugs, importedDrugs.Drugs);


        }

        private void PersistNewDrugs(List<Drug> newDrugs)
        {
            database.SaveBatch(newDrugs);
        }

        private void MarkRemovedDrugsAsInactive(List<Drug> removedDrugs)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            removedDrugs.ForEach(x => { x.IsActive = false; x.InactiveSince = timestamp; });
            database.SaveBatch(removedDrugs);
        }

        private IEnumerable<Drug> GetChangedDrugs(IEnumerable<Drug> possiblyChangedDrugsFromImport, List<Drug> existingDrugs)
        {
            var changedEans = possiblyChangedDrugsFromImport.Where(x => existingDrugs.Where(e => e.EAN == x.EAN).FirstOrDefault()?.HasChangedAccordingTo(x) ?? false).Select(x=>x.EAN).ToList();
            return existingDrugs.Where(x=>changedEans.Contains(x.EAN));
        }

        private void PersistChangedDrugs(IEnumerable<Drug> changedExistingDrugs, List<Drug> importedDrugs)
        {
            var changedEans = changedExistingDrugs.Select(x => x.EAN).ToList();

            MarkRemovedDrugsAsInactive(changedExistingDrugs.ToList());
            PersistNewDrugs(importedDrugs.Where(x => changedEans.Contains(x.EAN)).ToList());
        }
    }

}
