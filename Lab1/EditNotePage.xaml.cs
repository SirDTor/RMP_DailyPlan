namespace Lab1;

public partial class EditNotePage : ContentPage
{
    private readonly NotesService _notesService = new NotesService();
    private readonly Note _note;

    public EditNotePage(Note? note)
    {
        InitializeComponent();

        if (note == null)
        {
            _note = new Note();
        }
        else
        {
            _note = note;  
            TitleEntry.Text = _note.Title;
            ContentEditor.Text = _note.Content;
            DatePicker.Date = _note.Date.Date;
            TimePicker.Time = _note.Date.TimeOfDay;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _note.Title = TitleEntry.Text;
        _note.Content = ContentEditor.Text;
        _note.Date = DatePicker.Date + TimePicker.Time;

        var notes = await _notesService.LoadNotesAsync();

        var existingNote = notes.FirstOrDefault(n => n.Id == _note.Id);
        if (existingNote != null)
        {
            notes[notes.IndexOf(existingNote)] = _note;
        }
        else
        {
            notes.Add(_note);
        }

        await _notesService.SaveNotesAsync(notes);
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}