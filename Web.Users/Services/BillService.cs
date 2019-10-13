using System;
using System.Collections.Generic;
using System.Linq;
using Web.Users.Models;

namespace Web.Users.Services
{
    public class BillService
    {
        public IList<Bill> GetBills(int count)
        {
            var bill = new Bill();
            bill.Id = new Guid();
            bill.Requestor = new User() { Id = new Guid(), Username = "User 1" };
            bill.DateTime = DateTime.UtcNow;
            bill.Purpose = "Dinner";
            bill.Remarks = "nothing";
            bill.TotalAmount = 60;
            bill.Requests = new List<BillRequest>()
            {
                new BillRequest()
                {
                    User = new User(){Id = new Guid(), Username = "User 2"},
                    Amount = 10
                },
                new BillRequest()
                {
                    User = new User(){Id = new Guid(), Username = "User 3"},
                    Amount = 20
                },
                new BillRequest()
                {
                    User = new User(){Id = new Guid(), Username = "User 4"},
                    Amount = 30
                }
            };

            var bills = new List<Bill>();
            bills.Add(bill);

            return bills.OrderBy(b => b.DateTime).Take(count).ToList();
        }
    }
}