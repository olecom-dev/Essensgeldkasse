using Essensgeldkasse.Views;
namespace Essensgeldkasse
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Routing.RegisterRoute("home", typeof(MainPage));
            Routing.RegisterRoute("addClient", typeof(AddClient));
            Routing.RegisterRoute("editClient", typeof(StartPage));
        }
    }
}