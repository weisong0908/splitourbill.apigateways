using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Web.Users.Models;
using Web.Users.Models.RequestModels;

namespace Web.Users.Services
{
    public class BillService : IBillService
    {
        private IList<Bill> _bills;
        private readonly IMapper mapper;

        public BillService(IMapper mapper)
        {
            _bills = CreateFakeBills();
            this.mapper = mapper;
        }

        public IList<Bill> GetBills(int count)
        {
            return _bills.OrderBy(b => b.DateTime).Take(count).ToList();
        }

        public Bill GetBill(Guid id)
        {
            return _bills.Where(b => b.Id == id).SingleOrDefault();
        }

        public Bill UpdateBill(BillUpdateRequest billUpdateRequest)
        {
            var billToBeUpdated = _bills.SingleOrDefault(b => b.Id == billUpdateRequest.Id);
            _bills.Remove(billToBeUpdated);
            _bills.Add(mapper.Map<Bill>(billUpdateRequest));

            return _bills.SingleOrDefault(b => b.Id == billUpdateRequest.Id);
        }

        public IList<Bill> AddBill(BillAddRequest billAddRequest)
        {
            var bill = mapper.Map<Bill>(billAddRequest);

            _bills.Add(bill);
            return _bills;
        }

        private static List<Bill> CreateFakeBills()
        {
            var bill = new Bill();
            bill.Id = Guid.NewGuid();
            bill.Requestor = new User() { Id = Guid.NewGuid(), Username = "User 1" };
            bill.DateTime = DateTime.UtcNow;
            bill.Purpose = "Dinner";
            bill.Remarks = "nothing";
            bill.TotalAmount = 60;
            bill.Requests = new List<BillRequest>()
            {
                new BillRequest()
                {
                    User = new User(){Id = Guid.NewGuid(), Username = "User 2"},
                    Amount = 10
                },
                new BillRequest()
                {
                    User = new User(){Id = Guid.NewGuid(), Username = "User 3"},
                    Amount = 20
                },
                new BillRequest()
                {
                    User = new User(){Id = Guid.NewGuid(), Username = "User 4"},
                    Amount = 30
                }
            };

            var _bills = new List<Bill>();
            _bills.Add(bill);
            return _bills;
        }
    }
}