using GraphFlix.DTOs;using GraphFlix.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraphFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendController : Controller
    {
        private IMovieRepository _movieRepository { get; }

        public RecommendController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // GET: api/<RecommendController>
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
        {
            // TODO: Get user from token
            int userId = 0;

            return StatusCode(StatusCodes.Status200OK, await _movieRepository.GetRecommendedMovies(userId));
        }
    }
}
