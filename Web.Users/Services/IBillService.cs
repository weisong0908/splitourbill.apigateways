using System;
using System.Collections.Generic;
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
}