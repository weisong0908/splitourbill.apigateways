using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Web.Users.Models;
using Web.Users.Models.RequestModels;
using Web.Users.Models.ResponseModels;

namespace Web.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private IList<User> _users;

        public UserService(IMapper mapper)
        {
            _users = CreateFakeUsers();
            this.mapper = mapper;
        }

        public IList<UserSimpleResponse> GetFriends()
        {
            var friends = new List<UserSimpleResponse>();
            foreach (var user in _users)
                friends.Add(mapper.Map<UserSimpleResponse>(user));

            return friends;
        }

        public UserSimpleResponse GetFriend(Guid id)
        {
            var friend = _users.SingleOrDefault(u => u.Id == id);

            return mapper.Map<UserSimpleResponse>(friend);
        }

        public UserSimpleResponse AuthenticateUser(UserLoginRequest userLoginRequest)
        {
            var user = _users.SingleOrDefault(u => u.Username == userLoginRequest.Username && u.Password == userLoginRequest.Password);

            return mapper.Map<UserSimpleResponse>(user);
        }

        public UserSimpleResponse SignUpUser(UserSignUpRequest userSignUpRequest)
        {
            var user = mapper.Map<User>(userSignUpRequest);
            _users.Add(user);

            return mapper.Map<UserSimpleResponse>(_users.SingleOrDefault(u => u.Id == user.Id));
        }

        private IList<User> CreateFakeUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Id = Guid.NewGuid(),
                    Username = "WS",
                    Password = "WS"
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Username = "User 2",
                    Password = "User 2"
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Username = "User 3",
                    Password = "User 3"
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Username = "User 4",
                    Password = "User 4"
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Username = "User 5",
                    Password = "User 5"
                }
            };

            return users;
        }
    }
}