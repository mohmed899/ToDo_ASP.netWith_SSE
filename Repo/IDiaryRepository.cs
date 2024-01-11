using task_sr_2.Models;

namespace task_sr_2.Repo
{
    public interface IDiaryRepository
    {
        List<DiaryEntry> GetRecentEntries();
        void AddEntry(DiaryEntry entry);
        void UpdateEntry();
        void DeleteEntry(int entryId);
        Task<byte[]> UploadPhoto(IFormFile file);
        DiaryEntry GetEntryById(int entryId);
        void UpdateLastNEWentry();


    }
}
