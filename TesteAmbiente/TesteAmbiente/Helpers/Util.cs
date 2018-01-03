using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Net.Mobile.Forms;

namespace TesteAmbiente.Helpers
{
    public class Util
    {
        public async static Task<ZXingScannerPage> Capturar(ZXingScannerPage scan,string titulo)
        {
            if(scan == null)
            {
                var options = new ZXing.Mobile.MobileBarcodeScanningOptions();

#if __ANDROID__
                MobileBarcodeScanner.Initialize(Application);
#endif
                scan = new ZXingScannerPage(options)
                {
                    IsScanning = true
                };
            }
            scan.Title = titulo;
            return scan;
        }
    }
}
