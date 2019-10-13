using System;

namespace Web.Users.Models
{
    public class BillRequest
    {
        public User User { get; set; }
        public Decimal Amount { get; set; }
    }
}