using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebUsers.Models;
using WebUsers.Models.ResponseModels;

namespace WebUsers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;

        public UsersController(IMapper mapper, IHttpClientFactory clientFactory)
        {
            _mapper = mapper;
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var client = _clientFactory.CreateClient("UserService");
            var response = await client.GetAsync("Users");

            response.EnsureSuccessStatusCode();

            var users = JsonSerializer.Deserialize<IList<User>>(await response.Content.ReadAsStringAsync());

            return Ok(_mapper.Map<IList<UserSimpleResponse>>(users));
        }
    }
}