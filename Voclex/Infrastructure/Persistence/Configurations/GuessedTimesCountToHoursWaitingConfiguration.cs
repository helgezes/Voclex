using Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public sealed class GuessedTimesCountToHoursWaitingConfiguration : IEntityTypeConfiguration<GuessedTimesCountToHoursWaiting>
    {
        private readonly GuessedTimesCountToHoursWaiting[] defaultGuessedTimesCountToHoursWaiting =
        {
            new (0, 0),
            new (1, 12),
            new (2, 24),
            new (3, 24 * 5),
            new (4, 24 * 14),
            new (5, 24 * 45),
            new (6, 24 * 4 * 30),
        };

        public void Configure(EntityTypeBuilder<GuessedTimesCountToHoursWaiting> builder)
        {
            builder.HasKey(g => g.GuessedTimesCount);

            builder.HasData(defaultGuessedTimesCountToHoursWaiting);
        }
    }
}
