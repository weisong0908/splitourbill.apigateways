using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebUsers.Models.RequestModels;
using WebUsers.Services;

namespace WebUsers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        [HttpGet("{userId}/friends")]
        public async Task<IActionResult> GetFriends(Guid userId)
        {
            return Ok(await _userService.GetFriends(userId));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            return Ok(await _userService.GetUser(userId));
        }

        [HttpGet("{userId}/friendRequests")]
        public async Task<IActionResult> GetFriendRequests(Guid userId)
        {
            return Ok(await _userService.GetFriendRequests(userId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] NewUserCreationRequest newUserCreationRequest)
        {
            var user = await _userService.CreateUser(newUserCreationRequest);

            return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
        }
    }
}