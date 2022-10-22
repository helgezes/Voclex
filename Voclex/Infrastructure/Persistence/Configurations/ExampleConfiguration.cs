using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    sealed class ExampleConfiguration : IEntityTypeConfiguration<Example>
    {
        public void Configure(EntityTypeBuilder<Example> builder)
        {
            builder.Property(d => d.Value).HasMaxLength(2000).IsRequired();

            builder.HasOne(d => d.Term)
                .WithMany().HasForeignKey(i => i.TermId);
        }
    }
}
