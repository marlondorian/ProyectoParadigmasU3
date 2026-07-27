using ProyectoApi.Entities;
using ProyectoApi.Services.Songs;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly ISongsService _songsService;

        public SongsController(ISongsService songsService)
        {
            _songsService = songsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            var songs = await _songsService.GetSongsAsync();
            return Ok(songs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            var song = await _songsService.GetSongAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }
    }
}
