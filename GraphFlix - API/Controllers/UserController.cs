using GraphFlix.DTOs;using GraphFlix.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraphFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
		private IUserRepository _userRepository { get; }

		public UserController(IUserRepository _userRepository)
		{
			this._userRepository = _userRepository;
		}

		// GET: api/<UserController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserDto>>> Get()
		{
			return StatusCode(StatusCodes.Status200OK, await _userRepository.GetAll());
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<UserDto>> Get(int id)
		{
			UserDto model = await _userRepository.GetById(id);
			if (model == null)
			{
				return StatusCode(StatusCodes.Status404NotFound);
			}
			return StatusCode(StatusCodes.Status200OK, model);
		}

		// PUT api/<UserController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, [FromBody] UserDto modelChanges)
		{
			if (ModelState.IsValid)
			{
				await _userRepository.Update(id, modelChanges);
				return StatusCode(StatusCodes.Status200OK);
			}
			return StatusCode(StatusCodes.Status400BadRequest);
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			await _userRepository.Delete(id);
			return StatusCode(StatusCodes.Status204NoContent);
		}

	}
}
