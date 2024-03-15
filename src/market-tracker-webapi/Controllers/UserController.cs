﻿using market_tracker_webapi.Application.Models;
using market_tracker_webapi.Application.Repositories.User;
using Microsoft.AspNetCore.Mvc;

namespace market_tracker_webapi.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {   
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserData>> GetUserAsync(int id)
        {
            _logger.LogDebug($"Call {nameof(GetUserAsync)} with {id}");

            var user = await _userRepository.GetUserAsync(id);
            return user is null ? NotFound("User not found!") : Ok(user);
        }
    }
}
