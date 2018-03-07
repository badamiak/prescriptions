using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Prescriptions.API.Services
{
    public class BarcodeService
    {

        public static BitmapImage GetBarcode(string forData)
        {
            return GetImageFromBase64(riverOx_barcode128.Barcode128.generateBarcode(forData, false));
        }

        public static BitmapImage GetImageFromBase64(string base64EncodedImage)
        {
            var bytes = Convert.FromBase64String(base64EncodedImage.Remove(0, "data:image/png;base64,".Length));
            var ms = new MemoryStream(bytes);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;

        }
    }
}
