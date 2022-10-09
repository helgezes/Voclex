using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    sealed class TermProgressConfiguration : IEntityTypeConfiguration<TermProgress>
    {
        public void Configure(EntityTypeBuilder<TermProgress> builder)
        {
            builder.HasOne(p => p.Term)
                .WithMany().HasForeignKey(p => p.TermId);
            builder.HasOne(p => p.User)
                .WithMany().HasForeignKey(p => p.UserId);
        }
    }
}
