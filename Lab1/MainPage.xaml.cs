using System.Collections.ObjectModel;
using System.Text.Json;

namespace Lab1
{
    public partial class MainPage : ContentPage
    {
        private readonly NotesService _notesService = new NotesService();
        public ObservableCollection<Note> Notes { get; set; } = new();

        private List<Note> _allNotes = new();

        public MainPage()
        {
            InitializeComponent();
            LoadNotes();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadNotes(); // Перезагрузка данных после возврата
        }

        private async Task LoadNotes()
        {
            _allNotes = await _notesService.LoadNotesAsync();
            Notes.Clear();
            foreach (var note in _allNotes)
            {
                Notes.Add(note);
            }
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue?.ToLower() ?? string.Empty;

            var filteredNotes = _allNotes
                .Where(note => note.Title.ToLower().Contains(searchText) ||
                               note.Content.ToLower().Contains(searchText))
                .ToList();

            Notes.Clear();
            foreach (var note in filteredNotes)
            {
                Notes.Add(note);
            }
        }

        private async void OnAddNoteClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditNotePage(null));
        }

        private async void OnEditNoteSwiped(object sender, EventArgs e)
        {
            if (sender is SwipeItem swipeItem && swipeItem.CommandParameter is Note noteToEdit)
            {
                await Navigation.PushAsync(new EditNotePage(noteToEdit));
            }
        }

        private async void OnDeleteNoteSwiped(object sender, EventArgs e)
        {
            if (sender is SwipeItem swipeItem && swipeItem.CommandParameter is Note noteToDelete)
            {
                bool confirm = await DisplayAlert(
                    "Удаление заметки",
                    $"Вы уверены, что хотите удалить заметку \"{noteToDelete.Title}\"?",
                    "Да", "Нет");

                if (confirm)
                {
                    var notes = await _notesService.LoadNotesAsync();

                    // Удаляем заметку по её ID
                    notes.RemoveAll(n => n.Id == noteToDelete.Id);

                    await _notesService.SaveNotesAsync(notes);

                    // Обновление интерфейса
                    await LoadNotes();
                }
            }
        }
    }

}
