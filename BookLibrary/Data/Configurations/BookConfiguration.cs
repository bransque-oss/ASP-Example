using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<DbBook>
    {
        public void Configure(EntityTypeBuilder<DbBook> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1500);
            builder.Property(x => x.Isbn)
                .HasMaxLength(20);
            builder.HasOne(b => b.Author)
                .WithMany(b => b.Books)
                .HasForeignKey(b => b.AuthorId)
                .IsRequired();
        }
    }
}
