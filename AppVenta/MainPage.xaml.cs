using AppVenta.ViewModels;

namespace AppVenta
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        
    }
}