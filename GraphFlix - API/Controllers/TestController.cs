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

    }
}
