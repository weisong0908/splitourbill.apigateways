using System;
using Microsoft.AspNetCore.Mvc;
using Web.Users.Models.RequestModels;
using Web.Users.Services;

namespace Web.Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
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