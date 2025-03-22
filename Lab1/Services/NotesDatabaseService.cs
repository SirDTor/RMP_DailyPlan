using SQLite;

namespace Lab1.Services;

public class NotesDatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    public NotesDatabaseService()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db");
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Note>().Wait();
    }

    // Получение всех заметок
    public Task<List<Note>> GetNotesAsync() => _database.Table<Note>().ToListAsync();

    // Получение заметки по ID
    public Task<Note> GetNoteByIdAsync(int id) => _database.Table<Note>().Where(n => n.Id == id).FirstOrDefaultAsync();

    // Добавление или обновление заметки
    public Task<int> SaveNoteAsync(Note note)
    {
        if (note.Id != 0)
        {
            note.LastEditedDate = DateTime.Now;  // Обновление времени редактирования
            return _database.UpdateAsync(note);
        }
        else
        {
            note.CreatedDate = DateTime.Now;
            note.LastEditedDate = DateTime.Now;
            return _database.InsertAsync(note);
        }
    }

    // Удаление заметки
    public Task<int> DeleteNoteAsync(Note note) => _database.DeleteAsync(note);
}
