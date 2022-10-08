using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    sealed class DictionaryItemConfiguration : IEntityTypeConfiguration<DictionaryItem>
    {
        public void Configure(EntityTypeBuilder<DictionaryItem> builder)
        {
            builder.Property(d => d.Value).HasMaxLength(600).IsRequired();

            builder.HasOne<Dictionary>().WithMany().HasForeignKey(i => i.DictionaryId);
        }
    }
}
