using Microsoft.EntityFrameworkCore;

namespace task_sr_2.Models
{
    public class DiaryDBContext : DbContext
    {
        public DbSet<DiaryEntry> DiaryEntries { get; set; }
        public DbSet<Photo> Pictures { get; set; }

        public DiaryDBContext(DbContextOptions<DiaryDBContext> options)
            : base(options)
        {
        }

     
    }
}
