using GraphFlix.Database;
using GraphFlix.Models;
using GraphFlix.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraphFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly MovieRepository _movieRepository;
        public TestController(MovieRepository repository)
        {
            _movieRepository = repository;
        }
        [HttpGet("Test")]
        public async Task<Dictionary<long,IReadOnlyList<string>>> TestAsync()
        {
            return await _movieRepository.GetNodesAsync();
        }
        [HttpGet("GetMovies")]
        public async Task<Dictionary<long, IReadOnlyList<string>>> GetMovies()
        {
            return await _movieRepository.GetMoviesAsync();
        }
        [HttpPost("CreateMovie")]
        public async Task Create([FromBody] Movie movie)
        {
            await _movieRepository.CreateMovie(movie);
        }
    }
}
