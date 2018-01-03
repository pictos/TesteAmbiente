using System.Threading.Tasks;
using TesteAmbiente.Helpers;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace TesteAmbiente
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private string _codigo;

        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        private string _formato;

        public string Formato
        {
            get { return _formato; }
            set { _formato = value; }
        }


        bool init, msg = false;
        ZXingScannerPage _scan = null;

        async Task Capturar()
        {
            _scan = await Util.Capturar(_scan, "Escanear Código");
            if(!init)
            {
                _scan.OnScanResult += (result) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            if (msg)
                                return;
                            _scan.IsScanning = false;

                            if (!string.IsNullOrEmpty(Codigo))
                                return;
                            Codigo = result.Text;
                            Formato = result.BarcodeFormat.ToString();
                            await Navigation.PopAsync();
                        }
                        catch (System.Exception ex)
                        {

                            msg = true;
                            await DisplayAlert("Erro",ex.Message,"Ok");
                            msg = false;
                        }
                    });
                };
                init = true;
            }
            Codigo = string.Empty;
            await Navigation.PushAsync(_scan);
        }

        async void Button_Clicked(object sender, System.EventArgs e)
        {
            await Capturar();
        }
    }
}
