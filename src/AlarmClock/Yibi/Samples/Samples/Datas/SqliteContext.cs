using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yibi.Samples.Core.Models;

namespace Yibi.Samples.Core.Datas
{
    public class SqliteContext
    {
        readonly SQLiteAsyncConnection _database;

        public SqliteContext(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<AlarmClockInfo>().Wait();
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
