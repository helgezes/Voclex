using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(d => d.Name).HasMaxLength(100).IsRequired();
            builder.Property(d => d.HashedPassword).HasMaxLength(100).IsRequired();
            builder.HasIndex(u => u.Name).IsUnique(true);
        }
    }
}
