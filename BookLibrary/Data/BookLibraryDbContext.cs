using Data.Configurations;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class BookLibraryDbContext : DbContext
    {
        public DbSet<DbAuthor> Authors { get; set; }
        public DbSet<DbBook> Books { get; set; }
        public DbSet<DbUser> Users { get; set; }

        public BookLibraryDbContext()
        {
        }

        public BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AuthorConfiguration());
            builder.ApplyConfiguration(new BookConfiguration());
            ExampleDataSet.Seed(builder);
        }
    }
}
