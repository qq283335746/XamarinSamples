using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Yibi.Samples.ViewModels;

namespace Yibi.Samples.Pages
{
    public partial class CalendarAlarmClockPage : ContentPage
    {
        public CalendarAlarmClockPage()
        {
            InitializeComponent();
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //}

        private DateTime CurrentTime
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}