using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUsers.Models.DomainModels;
using WebUsers.Models.ResponseModels;

namespace WebUsers.Services
{
    public interface IUserService
    {
        Task<IList<UserSimpleResponse>> GetUsers();
        Task<IList<UserSimpleResponse>> GetFriends(Guid userId);
    }
}