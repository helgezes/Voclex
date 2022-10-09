using Application.DataAccess;
using Application.Models;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public sealed class ApplicationDbContext : DbContext, IDbContext
    {
        public DbSet<TermsDictionary> TermsDictionaries => Set<TermsDictionary>();

        public DbSet<Term> Terms => Set<Term>();

        public DbSet<Definition> Definitions => Set<Definition>();

        public DbSet<User> Users => Set<User>();

        public DbSet<DictionaryItemProgress> DictionaryItemProgresses => 
            Set<DictionaryItemProgress>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=TestDB;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DictionaryConfiguration());
            modelBuilder.ApplyConfiguration(new DictionaryItemConfiguration());
            modelBuilder.ApplyConfiguration(new DefinitionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new DictionaryItemProgressConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
