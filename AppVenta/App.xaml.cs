using AppVenta.Pages;
using Microsoft.Maui.Storage;
namespace AppVenta
{
    public partial class App : Application
    {
        [Obsolete]
        public App()
        {
            InitializeComponent();


            var logueado = Preferences.Get("logueado", string.Empty);
            if (string.IsNullOrEmpty(logueado))
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new AppShell();
            }

        }
    }
}