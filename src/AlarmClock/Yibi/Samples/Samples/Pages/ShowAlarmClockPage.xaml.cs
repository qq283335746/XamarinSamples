using MediaManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Yibi.Samples.Core.Models;

namespace Yibi.Samples.Pages
{
    public partial class ShowAlarmClockPage : ContentPage
    {
        public ShowAlarmClockPage()
        {
            InitializeComponent();
        }

        private async void OnBtnOk_Clicked(object sender, EventArgs e)
        {
            var alarmClockInfo = (AlarmClockInfo)BindingContext;
            alarmClockInfo.IsEnable = false;

            await App.DbContext.SaveAlarmClockAsync(alarmClockInfo);

            await CrossMediaManager.Current.Stop();

            await Navigation.PopToRootAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(async () =>
            {
                await BindAsycn();
            });
        }

        private async Task BindAsycn()
        {
            var alarmClockInfo = (AlarmClockInfo)BindingContext;

            var oldAlarmClockInfo = await App.DbContext.GetAlarmClockAsync(alarmClockInfo.ID);
            if(oldAlarmClockInfo != null)
            {
                BindingContext = oldAlarmClockInfo;

                if (!string.IsNullOrEmpty(oldAlarmClockInfo.MusicPath))
                {
                    await CrossMediaManager.Current.Play(oldAlarmClockInfo.MusicPath);
                }
            }
        }

       
    }
}