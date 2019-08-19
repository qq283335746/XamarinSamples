using Plugin.FilePicker;
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
    public partial class AlarmClockDetailPage : ContentPage
    {
        public AlarmClockDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void OnBtnPickFileClicked(object sender,EventArgs e)
        {
            var mediaTypes = new string[] {"mp3", "mfm","rtttl", "midi", "mmf", "amr", "mpeg", "waw","wav","wma", "aac" };
            await PickAndShowFile(mediaTypes);
        }

        async Task PickAndShowFile(string[] fileTypes)
        {
            try
            {
                var pickedFile = await CrossFilePicker.Current.PickFile(fileTypes);

                if (pickedFile != null)
                {
                    var viewModel = (AlarmClockDetailModel)BindingContext;
                    viewModel.MusicPath = pickedFile.FilePath;
                    viewModel.MusicName = pickedFile.FileName;
                }
            }
            catch (Exception ex)
            {

            }
        }

        async void OnBtnAudioClicked(object sender,EventArgs e)
        {
            var viewModel = (AlarmClockDetailModel)BindingContext;

        }

        async void OnBtnSaveClicked(object sender,EventArgs e)
        {
            var viewModel = (AlarmClockDetailModel)BindingContext;
            if (string.IsNullOrEmpty(viewModel.Name)) return;

            var alarmClockInfo = new AlarmClockInfo { ID = viewModel.ID, Name = viewModel.Name, AlarmTime = DateTime.Parse($"{viewModel.SelectedDate.ToString("yyyy-MM-dd")} {viewModel.SelectedTime.Hours}:{viewModel.SelectedTime.Minutes}:{viewModel.SelectedTime.Seconds}"), MusicPath = viewModel.MusicPath };
            if (alarmClockInfo.ID < 1) alarmClockInfo.CreatedDate = DateTime.UtcNow;
            alarmClockInfo.LastUpdatedDate = DateTime.UtcNow;

            await App.DbContext.SaveAlarmClockAsync(alarmClockInfo);

            await Navigation.PopAsync();
        }

        async void OnBtnDeleteClicked(object sender,EventArgs e)
        {
            var viewModel = (AlarmClockDetailModel)BindingContext;
            var alarmClockInfo = await App.DbContext.GetAlarmClockAsync(viewModel.ID);
            if (alarmClockInfo == null) return;

            await App.DbContext.DeleteAlarmClockAsync(alarmClockInfo);

            await Navigation.PopAsync();
        }
    }
}