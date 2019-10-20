using System;
using System.Text.Json.Serialization;
using Web.Users.JsonConverter;

namespace Web.Users.Models
{
    public class BillRequest
    {
        public User User { get; set; }
        [JsonConverter(typeof(DecimalToStringConverter))]
        public Decimal Amount { get; set; }
    }
}