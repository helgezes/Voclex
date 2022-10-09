using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    sealed class DefinitionConfiguration : IEntityTypeConfiguration<Definition>
    {
        public void Configure(EntityTypeBuilder<Definition> builder)
        {
            builder.Property(d => d.Value).HasMaxLength(1000).IsRequired();

            builder.HasOne(d => d.Term)
                .WithMany().HasForeignKey(i => i.TermId);
        }
    }
}
