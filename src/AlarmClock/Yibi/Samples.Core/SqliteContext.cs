using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yibi.Samples.Core.Models;

namespace Yibi.Samples.Core
{
    public class SqliteContext
    {
        readonly SQLiteAsyncConnection _database;

        public SqliteContext(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<AlarmClockInfo>().Wait();
        }

        public async Task<AlarmClockInfo> GetEnableAlarmClockAsync()
        {
            var currTime = DateTime.Now.ToLocalTime();
            var currentTime = new DateTime(currTime.Year, currTime.Month, currTime.Day, currTime.Hour, currTime.Minute, 0);

            var datas = await _database.Table<AlarmClockInfo>().ToListAsync();

            return datas.FirstOrDefault(m => m.IsEnable && new DateTime(m.AlarmTime.Year, m.AlarmTime.Month, m.AlarmTime.Day, m.AlarmTime.Hour, m.AlarmTime.Minute, 0) == currentTime);
        }

        public async Task<List<AlarmClockInfo>> GetAlarmClocksAsync()
        {
            return await _database.Table<AlarmClockInfo>().ToListAsync();
        }

        public async Task<AlarmClockInfo> GetAlarmClockAsync(int id)
        {
            return await _database.Table<AlarmClockInfo>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<int> SaveAlarmClockAsync(AlarmClockInfo model)
        {
            if (model.ID != 0)
            {
                return await _database.UpdateAsync(model);
            }
            else
            {
                return await _database.InsertAsync(model);
            }
        }

        public async Task<int> DeleteAlarmClockAsync(AlarmClockInfo model)
        {
            return await _database.DeleteAsync(model);
        }
    }
}
