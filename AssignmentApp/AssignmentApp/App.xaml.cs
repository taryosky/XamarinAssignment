using System;
using AssignmentApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AssignmentApp
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string dbPath) : this()
        {
            Db.SetDBPath(dbPath);
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

