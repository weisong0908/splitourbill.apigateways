using System;

namespace Web.Users.Models
{
    public class FriendRequest
    {
        public Guid RequestorId { get; set; }
        public Guid RequesteeId { get; set; }
    }
}