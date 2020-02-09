using System;
using System.Text.Json.Serialization;

namespace WebUsers.Models.ResponseModels
{
    public class User
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; }
    }
}