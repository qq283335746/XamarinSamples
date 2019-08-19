using System;
using System.Collections.Generic;
using System.Text;

namespace Yibi.Samples.Core.ViewModels
{
    public class AlarmClockDetailModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime AlarmTime { get; set; }

        public DateTime MinDate { get; set; }

        public DateTime MaxDate { get; set; }

        public DateTime SelectedDate { get; set; }

        public TimeSpan SelectedTime { get; set; }

        public string MusicPath { get; set; }

        public string MusicName { get; set; }
    }
}
