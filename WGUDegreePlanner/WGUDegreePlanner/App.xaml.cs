using System;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WGUDegreePlanner
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public static Database DB { get; set; }
        public App(string databaseLocation)
        {
            InitializeComponent();

            DB = new Database(databaseLocation);

            MainPage = new NavigationPage(new MainPage());
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
