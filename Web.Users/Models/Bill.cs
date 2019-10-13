using System;
using System.Collections.Generic;

namespace Web.Users.Models
{
    public class Bill
    {
        public Guid Id { get; set; }
        public User Requestor { get; set; }
        public DateTime DateTime { get; set; }
        public String Purpose { get; set; }
        public String Remarks { get; set; }
        public Decimal TotalAmount { get; set; }
        public IList<BillRequest> Requests { get; set; }

        public Bill()
        {
            Requests = new List<BillRequest>();
        }
    }
}