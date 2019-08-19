using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Yibi.Samples.Core.Models;

namespace Yibi.Samples.Core
{
    public partial class NoteEntryPage : ContentPage
    {
        public NoteEntryPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            //var note = (NoteInfo)BindingContext;

            //if (string.IsNullOrWhiteSpace(note.Filename))
            //{
            //    // Save
            //    var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
            //    File.WriteAllText(filename, note.Text);
            //}
            //else
            //{
            //    // Update
            //    File.WriteAllText(note.Filename, note.Text);
            //}

            var note = (NoteInfo)BindingContext;
            note.Date = DateTime.UtcNow;
            await App.NoteDatabase.SaveNoteAsync(note);

            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            //var note = (NoteInfo)BindingContext;

            //if (File.Exists(note.Filename))
            //{
            //    File.Delete(note.Filename);
            //}

            //await Navigation.PopAsync();

            var note = (NoteInfo)BindingContext;
            await App.NoteDatabase.DeleteNoteAsync(note);
            await Navigation.PopAsync();
        }
    }
}