using System.Text.Json.Serialization;

namespace task_sr_2.Models
{
    public class DiaryEntry
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public List<Photo> Pictures { get; set; } = new List<Photo>();
    }

}
