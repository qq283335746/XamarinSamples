using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Yibi.Samples.Core.Models;
using Yibi.Samples.Core.ViewModels;

namespace Yibi.Samples.Core.Pages
{
    public partial class AlarmClocksPage : ContentPage
    {
        public AlarmClocksPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await App.DbContext.GetAlarmClocksAsync();
        }

        private DateTime CurrentTime
        {
            get
            {
                return DateTime.UtcNow;
            }
        }

        async void OnBtnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AlarmClockDetailPage
            {
                BindingContext = new AlarmClockDetailModel { MinDate = CurrentTime, MaxDate = CurrentTime.AddYears(1), SelectedDate = CurrentTime,SelectedTime=(CurrentTime.AddMinutes(30)-CurrentTime) }
            });
        }

        async void OnBtnToCalendarClicked(object sender,EventArgs e)
        {
            await Navigation.PushAsync(new CalendarAlarmClockPage
            {
                
            });
        }

        async void OnLvItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var model = e.SelectedItem as AlarmClockInfo;

                await Navigation.PushAsync(new AlarmClockDetailPage
                {
                    BindingContext = new AlarmClockDetailModel {ID=model.ID, Name = model.Name,AlarmTime = model.AlarmTime,MusicName = model.MusicName,MusicPath=model.MusicPath, MinDate = CurrentTime, MaxDate = CurrentTime.AddYears(1), SelectedDate = model.AlarmTime,SelectedTime= new TimeSpan(model.AlarmTime.Hour, model.AlarmTime.Minute, model.AlarmTime.Second) }
                });;
            }
        }
    }
}