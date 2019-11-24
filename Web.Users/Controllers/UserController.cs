using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Users.Models;
using Web.Users.Models.RequestModels;
using Web.Users.Services;

namespace Web.Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("/users")]
        public IActionResult GetUsers()
        {
            var users = userService.GetUsers();

            return Ok(users);
        }

        [HttpPost("/friend")]
        public IActionResult SendFriendRequest([FromBody]FriendRequest friendRequest)
        {
            userService.AddFriendRequest(friendRequest.RequestorId, friendRequest.RequesteeId);

            return Ok();
        }

        [HttpGet("/friendRequests/{requesteeId}")]
        public IActionResult GetFriendRequests(Guid requesteeId)
        {
            var friendRequests = userService.GetFriendRequests(requesteeId);

            return Ok(friendRequests);
        }

        [HttpGet("/friends")]
        public IActionResult GetFriends()
        {
            var friends = userService.GetFriends();

            return Ok(friends);
        }

        [HttpGet("/friend/{id}")]
        public IActionResult GetFriend(Guid id)
        {
            var friend = userService.GetFriend(id);

            if (friend == null)
                return NotFound();
            return Ok(friend);
        }

        [HttpPost("/login")]
        public IActionResult Login([FromBody] UserLoginRequest userLoginRequest)
        {
            var user = userService.AuthenticateUser(userLoginRequest);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("/signup")]
        public IActionResult SignUp(UserSignUpRequest userSignUpRequest)
        {
            var user = userService.SignUpUser(userSignUpRequest);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}