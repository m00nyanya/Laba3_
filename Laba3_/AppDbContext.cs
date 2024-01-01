using Microsoft.EntityFrameworkCore;


namespace Laba3_
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public string DbPath { get; }

        public AppDbContext(string dbPath)
        {
            DbPath = dbPath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
    }
}