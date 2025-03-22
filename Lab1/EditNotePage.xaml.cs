using Lab1.Services;

namespace Lab1;

public partial class EditNotePage : ContentPage
{
    private readonly NotesDatabaseService _databaseService;

    private readonly NotesService _notesService = new NotesService();
    
    private readonly Note _note;

    public EditNotePage(Note? note, NotesDatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;

        _note = note ?? new Note();

        TitleEntry.Text = _note.Title;
        ContentEditor.Text = _note.Content;
        DatePicker.Date = _note.ScheduledDate.Date;
        TimePicker.Time = _note.ScheduledDate.TimeOfDay;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _note.Title = TitleEntry.Text;
        _note.Content = ContentEditor.Text;
        _note.ScheduledDate = DatePicker.Date + TimePicker.Time;

        await _databaseService.SaveNoteAsync(_note);
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}