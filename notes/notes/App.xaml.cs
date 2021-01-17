using System;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using System.IO;

namespace notes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

     
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            Instance.pool.Clear();
        }

        protected override void OnResume()
        {
        }

    }
}

