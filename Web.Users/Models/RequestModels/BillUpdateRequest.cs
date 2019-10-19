using System;
using System.Collections.Generic;

namespace Web.Users.Models.RequestModels
{
    public class BillUpdateRequest
    {
        public Guid Id { get; set; }
        public User Requestor { get; set; }
        public DateTime DateTime { get; set; }
        public String Purpose { get; set; }
        public String Remarks { get; set; }
        public string TotalAmount { get; set; }
        public IList<BillRequest> Requests { get; set; }

        public BillUpdateRequest()
        {
            Requests = new List<BillRequest>();
        }
    }
}