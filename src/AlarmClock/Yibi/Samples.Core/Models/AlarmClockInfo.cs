using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yibi.Samples.Core.Models
{
    public class AlarmClockInfo
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime AlarmTime { get; set; }

        public string MusicName { get; set; }

        public string MusicPath { get; set; }

        public bool IsEnable { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
