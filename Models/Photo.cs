using System.Text.Json.Serialization;

namespace task_sr_2.Models
{
    public class Photo
    {

        public int Id { get; set; }
        public byte[] PhotoData { get; set; }
        public int DiaryEntryId { get; set; }
        [JsonIgnore]
        public DiaryEntry DiaryEntry { get; set; }
    }
}
