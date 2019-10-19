using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Web.Users.Models;
using Web.Users.Models.RequestModels;
using Web.Users.Services;

namespace Web.Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillController : ControllerBase
    {
        private readonly IBillService billService;
        public BillController(IBillService billService)
        {
            this.billService = billService;
        }

        [HttpGet("/bills/{count}")]
        public IActionResult GetBills(int count)
        {
            var bills = billService.GetBills(count);

            if (bills.Count == 0)
                return NotFound();
            return Ok(bills);
        }

        [HttpGet("{id}")]
        public IActionResult GetBill(Guid id)
        {
            var bill = billService.GetBill(id);

            return Ok(bill);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBill(Guid id, [FromBody]BillUpdateRequest billUpdateRequest)
        {
            if (id != billUpdateRequest.Id)
                return BadRequest();

            var updatedBill = billService.UpdateBill(billUpdateRequest);

            return Ok(updatedBill);
        }

        [HttpPost]
        public IActionResult AddBill([FromBody] BillAddRequest billAddRequest)
        {
            var bills = billService.AddBill(billAddRequest);

            return Ok(bills);
        }
    }
}