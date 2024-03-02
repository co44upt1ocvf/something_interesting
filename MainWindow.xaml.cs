using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Note
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public List<NoteParameter> notes = new List<NoteParameter>();
        private string fullNote = "notes.json";

        public MainWindow()
        {
            InitializeComponent();
            Load();

            today.SelectedDateFormat = DatePickerFormat.Long;
            today.SelectedDate = DateTime.Now;
        }

        private void Load()
        {
            if (File.Exists(fullNote))
            {
                notes = JsonDE_SE.Deserialize<List<NoteParameter>>(fullNote);
            }
        }

        private void Save()
        {
            JsonDE_SE.Serialize(notes, fullNote);
        }

        private void Update()
        {
            DateTime selectedDate = today.SelectedDate ?? DateTime.Today;
            var notesForSelectedDate = notes.Where(n => n.todayDay.Date == selectedDate.Date).ToList();
            allNotes.ItemsSource = notesForSelectedDate;
        }

        private void date(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void add(object sender, RoutedEventArgs e)
        {
            var newNote = new NoteParameter
            {
                Title = noteName.Text,
                Description = description.Text,
                todayDay = today.SelectedDate ?? DateTime.Today
            };

            notes.Add(newNote);
            Save();
            Update();

            noteName.Clear();
            description.Clear();
        }

        private void edit(object sender, RoutedEventArgs e)
        {
            var selectedNote = allNotes.SelectedItem as NoteParameter;
            if (selectedNote != null)
            {
                selectedNote.Title = noteName.Text;
                selectedNote.Description = description.Text;
                Save();
                Update();
            }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            var selectedNote = allNotes.SelectedItem as NoteParameter;
            if (selectedNote != null)
            {
                notes.Remove(selectedNote);
                Save();
                Update();

                noteName.Clear();
                description.Clear();
            }
        }

        private void view(object sender, RoutedEventArgs e)
        {
            var selectedNote = allNotes.SelectedItem as NoteParameter;
            if (selectedNote != null)
            {
                noteName.Text = selectedNote.Title;
                description.Text = selectedNote.Description;
            }
        }
    }
}
