using GraphFlix.DTOs;
using GraphFlix.Managers;
using Microsoft.AspNetCore.Mvc;

namespace GraphFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private IMovieManager _movieManager { get; }

        public GenreController(IMovieManager movieManager)
        {
            _movieManager = movieManager;
        }

        // GET: api/<MovieController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
        {
            return StatusCode(StatusCodes.Status200OK, await _movieManager.GetMovies());
        }
    }
}
