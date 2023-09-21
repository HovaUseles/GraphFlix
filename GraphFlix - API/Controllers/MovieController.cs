using GraphFlix.DTOs;
using GraphFlix.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraphFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private IMovieRepository _movieRepository { get; }

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // GET: api/<MovieController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
        {
            return StatusCode(StatusCodes.Status200OK, await _movieRepository.GetAll());
        }

		// GET api/<MovieController>/5
		[HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> Get(int id)
        {
            return StatusCode(StatusCodes.Status200OK, await _movieRepository.GetById(id));
        }

        // POST api/<MovieController>
        [HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult> Post([FromBody] MovieDto model)
        {
            if (ModelState.IsValid)
            {
                await _movieRepository.Create(model);
                //TODO request new added movie
                return StatusCode(StatusCodes.Status201Created);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] MovieDto modelChanges)
        {
            if (ModelState.IsValid)
            {
                await _movieRepository.Update(id, modelChanges);
                return StatusCode(StatusCodes.Status200OK);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _movieRepository.Delete(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
	}
}
