using GraphFlix.DTOs;
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
        private IMovieManager _movieManager { get; }

        public MovieController(IMovieManager movieManager)
        {
            _movieManager = movieManager;
        }

        //// GET: api/<MovieController>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
        //{
        //    return StatusCode(StatusCodes.Status200OK, await _movieManager.GetMovies());
        //}

		// GET: api/<MovieController>
		[HttpGet]
        [Route("recommended")]
        [ActionName("Index")]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
		{
			return StatusCode(StatusCodes.Status200OK, await _movieManager.GetMovies());
		}

		// GET api/<MovieController>/5
		[HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> Get(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, await _movieManager.GetMovie(id));
        }

        // POST api/<MovieController>
        [HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<MovieDto>> Post([FromBody] MovieDto model)
        {
            if (ModelState.IsValid)
            {
                MovieDto createdModel = await _movieManager.CreateMovie(model);
                return StatusCode(StatusCodes.Status201Created, createdModel);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MovieDto>> Put(string id, [FromBody] MovieDto modelChanges)
        {
            if (ModelState.IsValid)
            {
                MovieDto updatedModel = await _movieManager.UpdateMovie(modelChanges);
                return StatusCode(StatusCodes.Status200OK, updatedModel);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            MovieDto deletedModel = await _movieManager.DeleteMovie(id);
            if (deletedModel == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status204NoContent);
        }
	}
}
