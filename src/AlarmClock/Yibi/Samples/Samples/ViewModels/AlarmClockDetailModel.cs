using System;
using System.ComponentModel;

namespace Yibi.Samples.ViewModels
{
    class AlarmClockDetailModel:BaseViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime AlarmTime { get; set; }

        public DateTime MinDate { get; set; }

        public DateTime MaxDate { get; set; }

        public DateTime SelectedDate { get; set; }

        public TimeSpan SelectedTime { get; set; }

        public string MusicPath { get; set; }

        private string _musicName;

        public string MusicName
        {
            get { return _musicName; }
            set
            {
                _musicName = value;

                OnPropertyChanged(nameof(MusicName));
            }
        }
    }
}
