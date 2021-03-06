using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using WebUsers.Models.DomainModels;
using WebUsers.Models.RequestModels;
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

        public async Task<UserSimpleResponse> GetUser(Guid userId)
        {
            var client = _clientFactory.CreateClient("UserService");
            var response = await client.GetAsync("users/" + userId);

            response.EnsureSuccessStatusCode();

            var user = JsonSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync());

            return _mapper.Map<UserSimpleResponse>(user);
        }

        public async Task<IList<UserSimpleResponse>> GetFriendRequests(Guid userId)
        {
            var client = _clientFactory.CreateClient("UserService");
            var response = await client.GetAsync("relationships/user/" + userId);
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync();
            var relationships = JsonSerializer
                .Deserialize<IList<Relationship>>(await response.Content.ReadAsStringAsync())
                .Where(r => r.RelationshipType == "friend")
                .Where(r => r.Status == "requested");

            var requestors = new List<UserSimpleResponse>();
            foreach (var relationship in relationships)
            {
                if (relationship.Requestee == userId)
                    requestors.Add(new UserSimpleResponse()
                    {
                        Id = relationship.Requestor
                    });
            }

            response = await client.GetAsync("users");
            response.EnsureSuccessStatusCode();
            var users = JsonSerializer.Deserialize<IList<User>>(await response.Content.ReadAsStringAsync());

            foreach (var requestor in requestors)
            {
                requestor.Username = users.FirstOrDefault(user => user.Id == requestor.Id).Username;
            }

            return requestors;
        }

        public async Task<Relationship> GetRelationship(Guid relationshipId)
        {
            var client = _clientFactory.CreateClient("UserService");
            var response = await client.GetAsync("relationships/" + relationshipId);
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync();
            var relationship = JsonSerializer.Deserialize<Relationship>(await response.Content.ReadAsStringAsync());

            return relationship;
        }

        public async Task<Relationship> SendFriendRequest(Guid requestorId, Guid requesteeId)
        {
            var relationship = new Relationship()
            {
                Requestor = requestorId,
                Requestee = requesteeId,
                RelationshipType = "friend",
                Status = "requested"
            };

            var client = _clientFactory.CreateClient("UserService");
            var content = new StringContent(JsonSerializer.Serialize<Relationship>(relationship), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("relationships", content);

            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<Relationship>(await response.Content.ReadAsStringAsync());
        }

        public async Task<User> CreateUser(NewUserCreationRequest newUserCreationRequest)
        {
            var client = _clientFactory.CreateClient("UserService");
            var content = new StringContent(JsonSerializer.Serialize<NewUserCreationRequest>(newUserCreationRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("users", content);

            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync());
        }
    }
}