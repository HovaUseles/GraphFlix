using GraphFlix.DTOs;
using GraphFlix.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraphFlix.Controllers
{
    public class AuthController : Controller
    {
		[Route("api/[controller]")]
		[ApiController]
		public class JwtController : ControllerBase
		{
			[HttpPost]
			public IActionResult Post([FromBody] UserDto request)
			{
				var result = new List<KeyValuePair<string, string>>();

				// Check if username and password is valid through db instead
				if (request.Username == "Mikkel" && request.Password == "Mikkel123")
				{
					Token token = AuthProcessor.Generate();

					return Ok(token);
				}
				else return BadRequest();
			}
		}
	}
}
