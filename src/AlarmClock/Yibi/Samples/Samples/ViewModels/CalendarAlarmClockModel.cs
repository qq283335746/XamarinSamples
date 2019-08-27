using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Plugin.Calendar.Models;

namespace Yibi.Samples.ViewModels
{
    class CalendarAlarmClockModel : BaseViewModel
    {
        private CultureInfo _culture = CultureInfo.InvariantCulture;

        public CalendarAlarmClockModel()
        {
            Culture = CultureInfo.CreateSpecificCulture("en-US");
            Events = new EventCollection();

            Task.Run(async () =>
            {
                var alarmClocks = await App.DbContext.GetAlarmClocksAsync();
                if (alarmClocks != null && alarmClocks.Any())
                {
                    var q = from r in alarmClocks
                            group r by r.AlarmTime.ToString("yyyy-MM-dd") into daysGroup
                            select new
                            {
                                date = DateTime.Parse(daysGroup.Key),
                                datas = alarmClocks.Where(m => m.AlarmTime.ToString("yyyy-MM-dd") == daysGroup.Key)?.Select(m => new EventModel { Name = m.Name, Description = m.AlarmTime.ToString("yyyy-MM-dd HH:mm:ss") })
                            };

                    foreach (var item in q)
                    {

                        Events.Add(item.date, item.datas?.ToList());
                    }
                }
            }).Wait();
        }

        public EventCollection Events { get; }

        public int Month { get; set; } = DateTime.Now.Month;
        public int Year { get; set; } = DateTime.Now.Year;

        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public CultureInfo Culture
        {
            get => _culture;
            set
            {
                _culture = value;
                OnPropertyChanged(nameof(Culture));
            }
        }
    }
}
