using GraphFlix.DTOs;
using GraphFlix.Managers;
using GraphFlix.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GraphFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private IMovieRepository _movieRepository { get; }

        public GenreController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // GET: api/<MovieController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
        {
            return StatusCode(StatusCodes.Status200OK, await _movieRepository.GetAll());
        }
    }
}
