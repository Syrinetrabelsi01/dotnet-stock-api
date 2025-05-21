using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetStocks()
        {
            var stocks = await _context.Stocks.ToListAsync();
            return Ok(stocks);
        }

        [HttpPost("create")]
public async Task<IActionResult> CreateStock([FromBody] Stock stock)
{
    _context.Stocks.Add(stock);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetStocks), new { id = stock.Id }, new
    {
        message = "Stock created successfully",
        stock
    });
}

    }
}