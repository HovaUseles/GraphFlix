using GraphFlix.DTOs;
using GraphFlix.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraphFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendController : Controller
    {
        private IMovieManager _movieManager { get; }

        public RecommendController(IMovieManager movieManager)
        {
            _movieManager = movieManager;
        }

        // GET: api/<RecommendController>
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
        {
            return StatusCode(StatusCodes.Status200OK, await _movieManager.GetMovies());
        }
    }
}
