using Microsoft.EntityFrameworkCore;
using task_sr_2.Models;

namespace task_sr_2.Repo
{
    public class DiaryRepository : IDiaryRepository
    {
        private readonly DiaryDBContext _context;

        public DiaryRepository(DiaryDBContext context)
        {
            _context = context;
        }

        public List<DiaryEntry> GetRecentEntries()
        {

            return _context.DiaryEntries
                .Include(e=>e.Pictures)
                .OrderByDescending(e => e.DateTime)
                .ToList();
        }

        public void AddEntry(DiaryEntry entry)
        {
            _context.DiaryEntries.Add(entry);
            UpdateEntry();
        }

        public DiaryEntry GetEntryById(int entryId)
        {
            return _context.DiaryEntries.FirstOrDefault(e => e.Id == entryId);
        }

        public void UpdateEntry()
        {
            _context.SaveChanges();
        }

        public void DeleteEntry(int entryId)
        {
            var entry = GetEntryById(entryId);
            _context.DiaryEntries.Remove(entry);
            UpdateEntry();

        }

        public async Task< byte[]> UploadPhoto(IFormFile file)
        {
            byte[] imageData = null ; 
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                     imageData = memoryStream.ToArray();                
                }

            }
                return imageData;
        }

        public void UpdateLastNEWentry()
        {
            var lastNewEntery = _context.DiaryEntries.OrderByDescending(e => e.DateTime).FirstOrDefault(e => e.Status == 0);
            if (lastNewEntery != null)
            {
                lastNewEntery.Status = 1;
                _context.SaveChanges();
            }
        }
    }

}
