using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Yibi.Samples.Core.ViewModels;

namespace Yibi.Samples.Core.Pages
{
    public partial class CalendarAlarmClockPage : ContentPage
    {
        public CalendarAlarmClockPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new CalendarAlarmClockModel { Month = CurrentTime.Month, Year = CurrentTime.Year };
        }

        private DateTime CurrentTime
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}