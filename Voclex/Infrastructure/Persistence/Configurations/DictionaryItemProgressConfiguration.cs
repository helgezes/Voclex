using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    sealed class DictionaryItemProgressConfiguration : IEntityTypeConfiguration<DictionaryItemProgress>
    {
        public void Configure(EntityTypeBuilder<DictionaryItemProgress> builder)
        {
            builder.HasOne(p => p.DictionaryItem)
                .WithMany().HasForeignKey(p => p.DictionaryItemId);
            builder.HasOne(p => p.User)
                .WithMany().HasForeignKey(p => p.UserId);
        }
    }
}
