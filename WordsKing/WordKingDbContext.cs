using Microsoft.EntityFrameworkCore;
using WordsKing.Entities;

namespace WordsKing
{
    public class WordKingDbContext : DbContext
    {
        public DbSet<PassedWord> PassedWords { get; set; }

        public WordKingDbContext(DbContextOptions<WordKingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PassedWord>().HasKey(x => x.Id);
        }
    }
}
