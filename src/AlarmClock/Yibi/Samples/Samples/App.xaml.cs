using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Yibi.Samples.Core.Datas;
using Yibi.Samples.Core.Pages;

namespace Yibi.Samples.Core
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

        static NoteDatabase noteDatabase;
        public static NoteDatabase NoteDatabase
        {
            get
            {
                if (noteDatabase == null)
                {
                    noteDatabase = new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "YibiDb.db3"));
                }
                return noteDatabase;
            }
        }

        public App()
        {
            InitializeComponent();

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
