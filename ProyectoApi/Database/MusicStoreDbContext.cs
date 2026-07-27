using ProyectoApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProyectoApi.Database
{
    public class MusicStoreDbContext : DbContext
    {
        public MusicStoreDbContext(DbContextOptions<MusicStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Songs
            modelBuilder.Entity<Song>().HasData(
                new Song { Id = 1, Title = "Bohemian Rhapsody", Album = "A Night at the Opera", Artist = "Queen", Genre = "Rock", Price = 1.99m, ImageUrl = "https://images.unsplash.com/photo-1514525253161-7a46d19cd819?w=500&q=80" },
                new Song { Id = 2, Title = "Billie Jean", Album = "Thriller", Artist = "Michael Jackson", Genre = "Pop", Price = 1.49m, ImageUrl = "https://images.unsplash.com/photo-1619983081563-430f63602796?w=500&q=80" },
                new Song { Id = 3, Title = "Hotel California", Album = "Hotel California", Artist = "Eagles", Genre = "Rock", Price = 1.29m, ImageUrl = "https://images.unsplash.com/photo-1598387993441-a364f854c3e1?w=500&q=80" },
                new Song { Id = 4, Title = "Shape of You", Album = "Divide", Artist = "Ed Sheeran", Genre = "Pop", Price = 0.99m, ImageUrl = "https://images.unsplash.com/photo-1614613535308-eb5fbd3d2c17?w=500&q=80" },
                new Song { Id = 5, Title = "Blinding Lights", Album = "After Hours", Artist = "The Weeknd", Genre = "Synth-pop", Price = 1.99m, ImageUrl = "https://images.unsplash.com/photo-1493225457124-a1a2a5956093?w=500&q=80" },
                new Song { Id = 6, Title = "Stairway to Heaven", Album = "Led Zeppelin IV", Artist = "Led Zeppelin", Genre = "Rock", Price = 1.50m, ImageUrl = "https://images.unsplash.com/photo-1511671782779-c97d3d27a1d4?w=500&q=80" }
            );
        }
    }
}
