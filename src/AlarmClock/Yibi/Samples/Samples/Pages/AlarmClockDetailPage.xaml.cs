using MediaManager;
using Plugin.FilePicker;
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
            //var mediaTypes = new string[] {"mp3", "mfm","rtttl", "midi", "mmf", "amr", "mpeg", "waw","wav","wma", "aac" };
            await PickAndShowFile(null);
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
                    viewModel.MusicName = Path.GetFileName(pickedFile.FilePath);

                    BindingContext = viewModel;
                }
            }
            catch (Exception ex)
            {

            }
        }

        async void OnBtnAudioClicked(object sender,EventArgs e)
        {
            var viewModel = (AlarmClockDetailModel)BindingContext;
            if(!string.IsNullOrEmpty(viewModel.MusicPath)) await CrossMediaManager.Current.Play(viewModel.MusicPath);


        }

        async void OnBtnSaveClicked(object sender,EventArgs e)
        {
            var viewModel = (AlarmClockDetailModel)BindingContext;
            if (string.IsNullOrEmpty(viewModel.Name)) return;

            var alarmTime = new DateTime(viewModel.SelectedDate.Year, viewModel.SelectedDate.Month, viewModel.SelectedDate.Day, viewModel.SelectedTime.Hours, viewModel.SelectedTime.Minutes, 0);

            var alarmClockInfo = new AlarmClockInfo { ID = viewModel.ID, Name = viewModel.Name,MusicName = viewModel.MusicName, MusicPath = viewModel.MusicPath, AlarmTime = alarmTime };

            if (alarmClockInfo.ID < 1) alarmClockInfo.CreatedDate = DateTime.UtcNow;
            alarmClockInfo.LastUpdatedDate = DateTime.UtcNow;
            alarmClockInfo.IsEnable = true;

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