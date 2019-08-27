using MediaManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Yibi.Samples.Core.Models;
using Yibi.Samples.ViewModels;

namespace Yibi.Samples.Pages
{
    public partial class ShowAlarmClockPage : ContentPage
    {
        AlarmClockInfo _alarmClockInfo;

        public ShowAlarmClockPage()
        {
            InitializeComponent();
        }

        private async void OnBtnOk_Clicked(object sender, EventArgs e)
        {
            _alarmClockInfo.IsEnable = false;

            await App.DbContext.SaveAlarmClockAsync(_alarmClockInfo);

            await CrossMediaManager.Current.Stop();

            await Navigation.PopToRootAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(async () =>
            {
                await BindAsycn();
            }).Wait();
        }

        private async Task BindAsycn()
        {
            _alarmClockInfo = (AlarmClockInfo)BindingContext;
            BindingContext = new ShowAlarmClockModel { Name = _alarmClockInfo.Name, SelectedTime = _alarmClockInfo.AlarmTime.ToString("HH:mm") };

            if (!string.IsNullOrEmpty(_alarmClockInfo.MusicPath))
            {
                await CrossMediaManager.Current.Play(_alarmClockInfo.MusicPath);
            }
        }

       
    }
}