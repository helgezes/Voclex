using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    sealed class TermConfiguration : IEntityTypeConfiguration<Term>
    {
        public void Configure(EntityTypeBuilder<Term> builder)
        {
            builder.Property(d => d.Value).HasMaxLength(600).IsRequired();

            builder.HasOne(i => i.TermsDictionary).WithMany().HasForeignKey(i => i.TermsDictionaryId);
        }
    }
}
