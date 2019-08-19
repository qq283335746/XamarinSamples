using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yibi.Samples.Core.Models;

namespace Yibi.Samples.Core.Datas
{
    public class NoteDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public NoteDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<NoteInfo>().Wait();
        }

        public async Task<List<NoteInfo>> GetNotesAsync()
        {
            return await _database.Table<NoteInfo>().ToListAsync();
        }

        public async Task<NoteInfo> GetNoteAsync(int id)
        {
            return await _database.Table<NoteInfo>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<int> SaveNoteAsync(NoteInfo note)
        {
            if (note.ID != 0)
            {
                return await _database.UpdateAsync(note);
            }
            else
            {
                return await _database.InsertAsync(note);
            }
        }

        public async Task<int> DeleteNoteAsync(NoteInfo note)
        {
            return await _database.DeleteAsync(note);
        }
    }
}
