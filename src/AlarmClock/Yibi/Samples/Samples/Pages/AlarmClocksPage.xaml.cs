using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Yibi.Samples.Core.Models;
using Yibi.Samples.ViewModels;
using Yibi.Samples.Pages;
using System.Threading;
using Yibi.Samples.Messages;

namespace Yibi.Samples.Pages
{
    public partial class AlarmClocksPage : ContentPage
    {
        public AlarmClocksPage()
        {
            InitializeComponent();

            HandleSendedMessages();

            HandleReceivedMessages();

            //DoTimerRunning();
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
                BindingContext = new AlarmClockDetailModel { MinDate = CurrentTime, MaxDate = CurrentTime.AddYears(1), SelectedDate = CurrentTime, SelectedTime = (CurrentTime.AddMinutes(30) - CurrentTime) }
            });
        }

        async void OnBtnToCalendarClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarAlarmClockPage
            {
                BindingContext = new CalendarAlarmClockModel { Month = CurrentTime.Month, Year = CurrentTime.Year }
            });
        }

        async void OnLvItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var model = e.SelectedItem as AlarmClockInfo;

                await Navigation.PushAsync(new AlarmClockDetailPage
                {
                    BindingContext = new AlarmClockDetailModel { ID = model.ID, Name = model.Name, AlarmTime = model.AlarmTime, MusicName = model.MusicName, MusicPath = model.MusicPath, MinDate = CurrentTime, MaxDate = CurrentTime.AddYears(1), SelectedDate = model.AlarmTime, SelectedTime = new TimeSpan(model.AlarmTime.Hour, model.AlarmTime.Minute, model.AlarmTime.Second) }
                }); ;
            }
        }

        private void DoTimerRunning()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(1000), () =>
            {
                // UI interaction goes here

                var alarmClockInfo = App.DbContext.GetEnableAlarmClockAsync().Result;
                if (alarmClockInfo != null)
                {
                    if (alarmClockInfo.AlarmTime <= DateTime.UtcNow)
                    {
                        Navigation.PushAsync(new ShowAlarmClockPage
                        {
                            BindingContext = alarmClockInfo
                        }).Wait();
                    }
                }

                return true;
            });
            //while (true)
            //{
            //    var alarmClockInfo = await App.DbContext.GetEnableAlarmClockAsync();
            //    if (alarmClockInfo != null)
            //    {
            //        if (alarmClockInfo.AlarmTime <= DateTime.UtcNow)
            //        {
            //            await Navigation.PushAsync(new ShowAlarmClock
            //            {
            //                BindingContext = alarmClockInfo
            //            });
            //        }
            //    }

            //    Thread.Sleep(5000);
            //}
            //Device.StartTimer(TimeSpan.FromMilliseconds(1000), () =>
            //{
            //    var alarmClockInfo = App.DbContext.GetEnableAlarmClockAsync().Result;
            //    if (alarmClockInfo != null)
            //    {
            //        if (alarmClockInfo.AlarmTime <= DateTime.UtcNow)
            //        {
            //            Navigation.PushAsync(new ShowAlarmClock
            //            {
            //                BindingContext = alarmClockInfo
            //            }).Wait();
            //        }
            //    }

            //    return true; // True = Repeat again, False = Stop the timer
            //});

            //Device.StartTimer(TimeSpan.FromMilliseconds(1000), () =>
            //{
            //    var alarmClockInfo = App.DbContext.GetEnableAlarmClockAsync().GetAwaiter().GetResult();

            //    return false;
            //});
        }

        void HandleSendedMessages()
        {
            Device.BeginInvokeOnMainThread(() => {
                var message = new StartLongRunningTaskMessage();
                MessagingCenter.Send(message, "StartLongRunningTaskMessage");
            });
        }

        void HandleReceivedMessages()
        {
            MessagingCenter.Subscribe<TickedMessage>(this, "TickedMessage", async message => {
                await Navigation.PushAsync(new ShowAlarmClockPage
                {
                    BindingContext = new AlarmClockInfo { ID = int.Parse(message.Id) }
                });
            });
        }
    }
}