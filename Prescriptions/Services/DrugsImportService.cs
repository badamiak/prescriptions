using Prescriptions.Model.Drugs;
using System.IO;
using System.Xml.Serialization;

namespace Prescriptions.Services
{
    public class DrugsImportService
    {
        public DrugsCollection Import (string xmlFilePath)
        {
            var file = new FileInfo(xmlFilePath);
            if(!file.Exists)
            {
                throw new FileNotFoundException($"Provided file: {Path.GetFullPath(xmlFilePath)} does not exist");
            }

            var serializer = new XmlSerializer(typeof(DrugsCollection));
            using (var fs = file.OpenRead())
            {
                return serializer.Deserialize(fs) as DrugsCollection;
            }
        }
    }
}
