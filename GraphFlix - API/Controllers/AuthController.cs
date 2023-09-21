using GraphFlix.DTOs;
using GraphFlix.Models;
using GraphFlix.Processors;
using Microsoft.AspNetCore.Mvc;

namespace GraphFlix.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		[HttpPost]
		public IActionResult Post([FromBody] LoginDto request)
		{
			// Check if username and password is valid through db instead
			if (request.Username == "Mikkel@mikkel" && request.Password == "Mikkel123")
			{
				Token token = AuthProcessor.Generate();

				return Ok(token);
			}
			else return BadRequest();
		}
	}
}
