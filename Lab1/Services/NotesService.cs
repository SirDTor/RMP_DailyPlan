using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab1.Services
{
    public class NotesService
    {
        private readonly string _filePath;

        public NotesService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "notes.json");
        }

        public async Task<List<Note>> LoadNotesAsync()
        {
            if (!File.Exists(_filePath))
                return new List<Note>();

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Note>>(json) ?? new List<Note>();
        }

        public async Task SaveNotesAsync(List<Note> notes)
        {
            var json = JsonSerializer.Serialize(notes, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}
