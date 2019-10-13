using System;
using System.Collections.Generic;
using System.Linq;
using Web.Users.Models;

namespace Web.Users.Services
{
    public interface IBillService
    {
        Bill GetBill(Guid id);
        IList<Bill> GetBills(int count);
        Bill UpdateBill(Bill bill);
        IList<Bill> AddBill(Bill bill);
    }

    public class BillService : IBillService
    {
        private IList<Bill> _bills;

        public BillService()
        {
            _bills = CreateFakeBills();
        }

        public IList<Bill> GetBills(int count)
        {
            return _bills.OrderBy(b => b.DateTime).Take(count).ToList();
        }

        public Bill GetBill(Guid id)
        {
            return _bills.Where(b => b.Id == id).SingleOrDefault();
        }

        public Bill UpdateBill(Bill bill)
        {
            var billToBeUpdated = _bills.SingleOrDefault(b => b.Id == bill.Id);
            _bills.Remove(billToBeUpdated);
            _bills.Add(bill);

            return _bills.SingleOrDefault(b => b.Id == bill.Id);
        }

        public IList<Bill> AddBill(Bill bill)
        {
            _bills.Add(bill);
            return _bills;
        }

        private static List<Bill> CreateFakeBills()
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

            var _bills = new List<Bill>();
            _bills.Add(bill);
            return _bills;
        }
    }
}