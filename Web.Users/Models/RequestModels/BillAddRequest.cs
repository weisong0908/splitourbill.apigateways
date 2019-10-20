using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Web.Users.JsonConverter;

namespace Web.Users.Models.RequestModels
{
    public class BillAddRequest
    {
        public User Requestor { get; set; }
        public DateTime DateTime { get; set; }
        public string Purpose { get; set; }
        public string Remarks { get; set; }
        [JsonConverter(typeof(DecimalToStringConverter))]
        public decimal TotalAmount { get; set; }
        public IList<BillRequest> Requests { get; set; }

        public BillAddRequest()
        {
            Requests = new List<BillRequest>();
        }
    }
}