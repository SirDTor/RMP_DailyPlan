using Lab1.Services;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Lab1
{
    public partial class MainPage : ContentPage
    {
        private readonly NotesDatabaseService _databaseService;

        private readonly NotesService _notesService = new NotesService();
        
        public ObservableCollection<Note> Notes { get; set; } = new();

        private List<Note> _allNotes = new();

        public MainPage(NotesDatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadNotes();
        }

        private async Task LoadNotes()
        {
            _allNotes = await _databaseService.GetNotesAsync();
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
            await Navigation.PushAsync(new EditNotePage(null, _databaseService));
        }

        private async void OnEditNoteSwiped(object sender, EventArgs e)
        {
            if (sender is SwipeItem swipeItem && swipeItem.CommandParameter is Note noteToEdit)
            {
                await Navigation.PushAsync(new EditNotePage(noteToEdit, _databaseService));
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
                    await _databaseService.DeleteNoteAsync(noteToDelete);
                    await LoadNotes();
                }
            }
        }
        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

    }

}
