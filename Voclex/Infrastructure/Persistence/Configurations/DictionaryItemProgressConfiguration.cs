using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    sealed class DictionaryItemProgressConfiguration : IEntityTypeConfiguration<DictionaryItemProgress>
    {
        public void Configure(EntityTypeBuilder<DictionaryItemProgress> builder)
        {
            builder.HasOne<DictionaryItem>().WithMany().HasForeignKey(i => i.DictionaryItemId);
            builder.HasOne<User>().WithMany().HasForeignKey(i => i.UserId);
        }
    }
}
