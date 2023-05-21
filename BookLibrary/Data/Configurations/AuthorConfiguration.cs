using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<DbAuthor>
    {
        public void Configure(EntityTypeBuilder<DbAuthor> builder)
        {
            builder.ToTable("Authors");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(2000);
        }
    }
}
