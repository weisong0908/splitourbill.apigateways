using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using WebUsers.Models.DomainModels;
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
            var response = await client.GetAsync("users");

            response.EnsureSuccessStatusCode();

            var users = JsonSerializer.Deserialize<IList<User>>(await response.Content.ReadAsStringAsync());

            return _mapper.Map<IList<UserSimpleResponse>>(users);
        }

        public async Task<IList<UserSimpleResponse>> GetFriends(Guid userId)
        {
            var client = _clientFactory.CreateClient("UserService");
            var response = await client.GetAsync("relationships/user/" + userId);
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync();
            var relationships = JsonSerializer
                .Deserialize<IList<Relationship>>(await response.Content.ReadAsStringAsync())
                .Where(r => r.RelationshipType == "friend");

            var friends = new List<UserSimpleResponse>();
            foreach (var relationship in relationships)
            {
                if (relationship.Requestor == userId)
                    friends.Add(new UserSimpleResponse()
                    {
                        Id = relationship.Requestee
                    });
                else
                    friends.Add(new UserSimpleResponse()
                    {
                        Id = relationship.Requestor
                    });
            }

            response = await client.GetAsync("users");
            response.EnsureSuccessStatusCode();
            var users = JsonSerializer.Deserialize<IList<User>>(await response.Content.ReadAsStringAsync());

            foreach (var friend in friends)
            {
                friend.Username = users.FirstOrDefault(user => user.Id == friend.Id).Username;
            }

            return friends;
        }
    }
}