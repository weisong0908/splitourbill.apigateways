using System;
using System.Collections.Generic;
using Web.Users.Models.RequestModels;
using Web.Users.Models.ResponseModels;

namespace Web.Users.Services
{
    public interface IUserService
    {
        IList<UserSimpleResponse> GetFriends();
        UserSimpleResponse GetFriend(Guid id);
        UserSimpleResponse AuthenticateUser(UserLoginRequest userLoginRequest);
        UserSimpleResponse SignUpUser(UserSignUpRequest userSignUpRequest);
    }
}