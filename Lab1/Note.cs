using SQLite;

namespace Lab1
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Parse(Preferences.Get("CurrentDate", DateTime.Now.ToString("yyyy-MM-dd")));
        
        public DateTime LastEditedDate { get; set; } = DateTime.Parse(Preferences.Get("CurrentDate", DateTime.Now.ToString("yyyy-MM-dd")));

        public DateTime ScheduledDate { get; set; } = DateTime.Parse(Preferences.Get("CurrentDate", DateTime.Now.ToString("yyyy-MM-dd")));
    }
}
