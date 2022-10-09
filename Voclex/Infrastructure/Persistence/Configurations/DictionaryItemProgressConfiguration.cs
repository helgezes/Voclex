using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    sealed class DictionaryItemProgressConfiguration : IEntityTypeConfiguration<DictionaryItemProgress>
    {
        public void Configure(EntityTypeBuilder<DictionaryItemProgress> builder)
        {
            builder.HasOne(p => p.Term)
                .WithMany().HasForeignKey(p => p.TermId);
            builder.HasOne(p => p.User)
                .WithMany().HasForeignKey(p => p.UserId);
        }
    }
}
