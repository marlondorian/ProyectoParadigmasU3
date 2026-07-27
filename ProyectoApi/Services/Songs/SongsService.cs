#nullable enable
using ProyectoApi.Database;
using ProyectoApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProyectoApi.Services.Songs
{
    public class SongsService : ISongsService
    {
        private readonly MusicStoreDbContext _context;

        public SongsService(MusicStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Song>> GetSongsAsync()
        {
            return await _context.Songs.ToListAsync();
        }

        public async Task<Song?> GetSongAsync(int id)
        {
            return await _context.Songs.FindAsync(id);
        }
    }
}
