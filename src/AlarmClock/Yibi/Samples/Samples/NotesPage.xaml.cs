using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Yibi.Samples.Core.Models;

namespace Yibi.Samples.Core
{
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //var notes = new List<NoteInfo>();

            //var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");
            //foreach (var filename in files)
            //{
            //    notes.Add(new NoteInfo
            //    {
            //        Filename = filename,
            //        Text = File.ReadAllText(filename),
            //        Date = File.GetCreationTime(filename)
            //    });
            //}

            //listView.ItemsSource = notes
            //    .OrderBy(d => d.Date)
            //    .ToList();

            listView.ItemsSource = await App.NoteDatabase.GetNotesAsync();
        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NoteEntryPage
            {
                BindingContext = new NoteInfo()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //if (e.SelectedItem != null)
            //{
            //    await Navigation.PushAsync(new NoteEntryPage
            //    {
            //        BindingContext = e.SelectedItem as NoteInfo
            //    });
            //}

            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new NoteEntryPage
                {
                    BindingContext = e.SelectedItem as NoteInfo
                });
            }

        }
    }
}