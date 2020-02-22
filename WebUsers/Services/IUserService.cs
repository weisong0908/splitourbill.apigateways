using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUsers.Models.DomainModels;
using WebUsers.Models.RequestModels;
using WebUsers.Models.ResponseModels;

namespace WebUsers.Services
{
    public interface IUserService
    {
        Task<IList<UserSimpleResponse>> GetUsers();
        Task<UserSimpleResponse> GetUser(Guid userId);
        Task<IList<UserSimpleResponse>> GetFriends(Guid userId);
        Task<IList<UserSimpleResponse>> GetFriendRequests(Guid userId);
        Task<Relationship> GetRelationship(Guid relationshipId);
        Task<Relationship> SendFriendRequest(Guid requestorId, Guid requesteeId);
        Task<User> CreateUser(NewUserCreationRequest newUserCreationRequest);
    }
}