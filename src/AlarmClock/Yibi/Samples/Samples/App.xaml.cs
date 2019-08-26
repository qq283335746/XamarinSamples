using MediaManager;
using System;
using System.IO;
using Xamarin.Forms;
using Yibi.Samples.Core;
using Yibi.Samples.Pages;

namespace Yibi.Samples
{
    public partial class App : Application
    {
        //public static string FolderPath { get; private set; }
        static SqliteContext _context;
        public static SqliteContext DbContext
        {
            get
            {
                if (_context == null)
                {
                    _context = new SqliteContext(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "YibiDb.db3"));
                }
                return _context;
            }
        }

        public App()
        {
            InitializeComponent();

            CrossMediaManager.Current.Init();

            //FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            MainPage = new NavigationPage(new AlarmClocksPage());

            //MainPage = new MainPage();
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
