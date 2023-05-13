using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    sealed class DictionaryConfiguration : IEntityTypeConfiguration<TermsDictionary>
    {
        public void Configure(EntityTypeBuilder<TermsDictionary> builder)
        {
            builder.Property(d => d.Title).HasMaxLength(100).IsRequired();

            builder.HasOne(d => d.User)
                .WithMany().HasForeignKey(d => d.UserId);
        }
    }
}
