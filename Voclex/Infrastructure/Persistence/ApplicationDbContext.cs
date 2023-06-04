using Application.DataAccess;
using Application.Models;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public sealed class ApplicationDbContext : DbContext, IDbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options) { }

		public DbSet<TermsDictionary> TermsDictionaries => Set<TermsDictionary>();

        public DbSet<Term> Terms => Set<Term>();

        public DbSet<Definition> Definitions => Set<Definition>();

        public DbSet<Example> Examples => Set<Example>();

        public DbSet<Picture> Pictures => Set<Picture>();

        public DbSet<User> Users => Set<User>();

        public DbSet<TermProgress> TermProgresses => 
            Set<TermProgress>();

        public DbSet<GuessedTimesCountToHoursWaiting> GuessedTimesCountToHoursWaiting =>
            Set<GuessedTimesCountToHoursWaiting>();

        public DbSet<ExceptionLog> ExceptionLogs => Set<ExceptionLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DictionaryConfiguration());
            modelBuilder.ApplyConfiguration(new TermConfiguration());
            modelBuilder.ApplyConfiguration(new DefinitionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TermProgressConfiguration());
            modelBuilder.ApplyConfiguration(new ExampleConfiguration());
            modelBuilder.ApplyConfiguration(new PictureConfiguration());
            modelBuilder.ApplyConfiguration(new GuessedTimesCountToHoursWaitingConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
