using Microsoft.EntityFrameworkCore;

namespace ReactRandomJokes.Data
{
   public class RJDataContext : DbContext
        {
            private string _connectionString;

            public RJDataContext(string connectionString)
            {
                _connectionString = connectionString;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<UserLikedJokes>()
                .HasKey(qt => new { qt.UserId, qt.JokeId });
          
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Joke> Jokes { get; set; }
            public DbSet<User> Users { get; set; }
            public DbSet<UserLikedJokes> UserLikedJokes { get; set; }
        }

    }


