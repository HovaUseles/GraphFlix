﻿using GraphFlix.DTOs;

using GraphFlix.Models;
using GraphFlix.Processors;
using GraphFlix.Repositories;
using GraphFlix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraphFlix.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private IUserRepository _userRepository { get; }
		private IHashingService _hashingService { get; }
		private ITokenService _tokenService { get; }

        public AuthController(
			IUserRepository userRepository, 
			IHashingService hashingService, 
			ITokenService tokenService)
        {
            _userRepository = userRepository;
            _hashingService = hashingService;
            _tokenService = tokenService;
        }

		[HttpPost("[action]")]
		[Authorize(Roles = "Customer")]
		public async Task<ActionResult> VerifyToken()
		{
			return Ok();
		}

        [HttpPost("[action]")]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto request)
		{
			if(!ModelState.IsValid)
			{
				return StatusCode(StatusCodes.Status400BadRequest, "Invalid request data");
			}

			string? salt = await _userRepository.GetUserSalt(request.Username);
			if(salt == null)
			{
				return StatusCode(StatusCodes.Status400BadRequest, "User could not be found");
			}

			string passwordHash = _hashingService.HashPassword(request.Password, salt);

			if(await _userRepository.VerifyLogin(request.Username, passwordHash))
			{
				UserDto user = await _userRepository.GetByUsername(request.Username);
				TokenDto token = _tokenService.CreateToken(user);
                return StatusCode(StatusCodes.Status200OK, token);
			}
			
			// Login not verified
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid request data");

        }

        [HttpPost("[action]")]
		public async Task<ActionResult> Register([FromBody] LoginDto request)
		{
			if (!ModelState.IsValid)
			{
				return StatusCode(StatusCodes.Status400BadRequest, "Invalid request data");
			}

			await _userRepository.Create(request);

			return StatusCode(StatusCodes.Status200OK);
		}
	}
}
