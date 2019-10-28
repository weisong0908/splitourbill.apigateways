using System;

namespace Web.Users.Models.ResponseModels
{
    public class FriendRequestSimpleResponse
    {
        public Guid Id { get; set; }
        public string RequestorUsername { get; set; }
    }
}