using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<DbUser>
    {
        public void Configure(EntityTypeBuilder<DbUser> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Login)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(x => x.CanChangeEntities)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
