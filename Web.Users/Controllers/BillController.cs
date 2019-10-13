using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Web.Users.Models;
using Web.Users.Services;

namespace Web.Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillController : ControllerBase
    {
        private readonly BillService billService;
        public BillController()
        {
            this.billService = new BillService();
        }

        [HttpGet("/bills/{count}")]
        public IActionResult GetBills(int count)
        {
            var bills = billService.GetBills(count);

            if (bills.Count == 0)
                return NotFound();
            return Ok(bills);
        }
    }
}