using System;
using System.Collections.Generic;
using System.Linq;
using Web.Users.Models;
using Web.Users.Models.RequestModels;

namespace Web.Users.Services
{
    public interface IBillService
    {
        Bill GetBill(Guid id);
        IList<Bill> GetBills(int count);
        Bill UpdateBill(BillUpdateRequest billUpdateRequest);
        IList<Bill> AddBill(BillAddRequest billAddRequest);
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

        public Bill UpdateBill(BillUpdateRequest billUpdateRequest)
        {
            var billToBeUpdated = _bills.SingleOrDefault(b => b.Id == billUpdateRequest.Id);
            _bills.Remove(billToBeUpdated);
            _bills.Add(MapBillUpdateRequestToBill(billUpdateRequest));

            return _bills.SingleOrDefault(b => b.Id == billUpdateRequest.Id);
        }

        public IList<Bill> AddBill(BillAddRequest billAddRequest)
        {
            _bills.Add(MapBillAddRequestToBill(billAddRequest));
            return _bills;
        }

        private Bill MapBillAddRequestToBill(BillAddRequest billAddRequest)
        {
            var bill = new Bill();
            bill.Id = Guid.NewGuid();
            bill.DateTime = billAddRequest.DateTime;
            bill.Purpose = billAddRequest.Purpose;
            bill.Remarks = billAddRequest.Remarks;
            bill.Requestor = billAddRequest.Requestor;
            bill.Requests = billAddRequest.Requests;
            bill.TotalAmount = decimal.Parse(billAddRequest.TotalAmount);

            return bill;
        }

        private Bill MapBillUpdateRequestToBill(BillUpdateRequest billUpdateRequest)
        {
            var bill = new Bill();
            bill.Id = Guid.NewGuid();
            bill.DateTime = billUpdateRequest.DateTime;
            bill.Purpose = billUpdateRequest.Purpose;
            bill.Remarks = billUpdateRequest.Remarks;
            bill.Requestor = billUpdateRequest.Requestor;
            bill.Requests = billUpdateRequest.Requests;
            bill.TotalAmount = decimal.Parse(billUpdateRequest.TotalAmount);

            return bill;
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