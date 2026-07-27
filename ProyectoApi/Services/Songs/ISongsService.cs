#nullable enable
using ProyectoApi.Entities;

namespace ProyectoApi.Services.Songs
{
    public interface ISongsService
    {
        Task<IEnumerable<Song>> GetSongsAsync();
        Task<Song?> GetSongAsync(int id);
    }
}
