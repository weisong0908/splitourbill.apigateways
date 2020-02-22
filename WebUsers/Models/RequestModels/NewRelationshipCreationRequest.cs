using System;

namespace WebUsers.Models.RequestModels
{
    public class NewRelationshipCreationRequest
    {
        public Guid RequestorId { get; set; }
        public Guid RequesteeId { get; set; }
    }
}