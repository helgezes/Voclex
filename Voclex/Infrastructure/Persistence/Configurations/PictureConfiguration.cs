using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    sealed class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.Property(d => d.Path).HasMaxLength(200).IsRequired();

            builder.HasOne(d => d.Term)
                .WithMany().HasForeignKey(i => i.TermId);
        }
    }
}
