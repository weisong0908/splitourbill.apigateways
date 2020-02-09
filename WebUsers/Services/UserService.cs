using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using WebUsers.Models;
using WebUsers.Models.ResponseModels;

namespace WebUsers.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;

        public UserService(IMapper mapper, IHttpClientFactory clientFactory)
        {
            _mapper = mapper;
            _clientFactory = clientFactory;
        }

        public async Task<IList<UserSimpleResponse>> GetUsers()
        {
            var client = _clientFactory.CreateClient("UserService");
            var response = await client.GetAsync("Users");

            response.EnsureSuccessStatusCode();

            var users = JsonSerializer.Deserialize<IList<User>>(await response.Content.ReadAsStringAsync());

            return _mapper.Map<IList<UserSimpleResponse>>(users);
        }
    }
}