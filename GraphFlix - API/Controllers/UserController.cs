using GraphFlix.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraphFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

		public static string Name { get => nameof(UserController).Replace("Controller", ""); }
		private IUserManager _userManager { get; }

		public UserController(IUserManager _userManager)
		{
			this._userManager = _userManager;
		}

		// GET: api/<UserController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserDto>>> Get()
		{
			return StatusCode(StatusCodes.Status200OK, await _userManager.GetUsers());
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<UserDto>> Get(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				return StatusCode(StatusCodes.Status400BadRequest);
			}
			UserDto model = await _userManager.GetUser(id);
			if (model == null)
			{
				return StatusCode(StatusCodes.Status404NotFound);
			}
			return StatusCode(StatusCodes.Status200OK, model);
		}

		// POST api/<UserController>
		[HttpPost]
		public async Task<ActionResult<UserDto>> Post([FromBody] UserDto model)
		{
			if (ModelState.IsValid)
			{
				UserDto createdModel = await _userManager.CreateUser(model);
				return StatusCode(StatusCodes.Status201Created, createdModel);
			}
			return StatusCode(StatusCodes.Status400BadRequest);
		}

		// PUT api/<UserController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<UserDto>> Put(string id, [FromBody] UserDto modelChanges)
		{
			if (ModelState.IsValid)
			{
				UserDto updatedModel = await _userManager.UpdateUser(modelChanges);
				return StatusCode(StatusCodes.Status200OK, updatedModel);
			}
			return StatusCode(StatusCodes.Status400BadRequest);
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				return StatusCode(StatusCodes.Status400BadRequest);
			}
			UserDto deletedModel = await _userManager.DeleteUser(id);
			return StatusCode(StatusCodes.Status204NoContent);
		}

	}
}
