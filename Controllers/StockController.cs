using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Mappers; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Dtos.Stock;
using api.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace api.Controllers
{
    // This controller handles all stock-related operations
    // It uses the ApplicationDBContext to interact with the database
    // and performs CRUD operations on the Stock entity
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET method to retrieve all non-deleted stocks with filtering and sorting
        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetStocks([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _context.Stocks
                .Where(s => !s.IsDeleted)
                .Include(c => c.Comments)
                    .ThenInclude(a => a.AppUser)
                .AsQueryable()
                .Filter(query)
                .Sort(query)
                .Select(s => s.ToDto())
                .ToListAsync();

            return Ok(stocks);
        }

        // POST method to create a new stock
        [HttpPost("create")]
        public async Task<IActionResult> CreateStock([FromBody] StockCreateDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = stockDto.ToModel();

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStocks), new { id = stock.Id }, new
            {
                message = "Stock created successfully",
                stock = stock.ToDto()
            });
        }

        // PUT method to fully update a stock
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] StockUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
                return NotFound(new { message = "Stock not found" });

            stock.UpdateFromDto(updateDto);

            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Stock updated successfully",
                stock = stock.ToDto()
            });
        }

        // PATCH method to partially update a stock
        [HttpPatch("patch/{id}")]
        public async Task<IActionResult> PatchStock(int id, [FromBody] StockUpdateDto patchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
                return NotFound(new { message = $"Stock with id {id} not found" });

            stock.UpdateFromDto(patchDto);

            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Stock partially updated",
                stock = stock.ToDto()
            });
        }

        // DELETE method to permanently delete a stock
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
                return NotFound(new { message = $"Stock with id {id} not found" });

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Stock deleted successfully",
                deletedId = id
            });
        }

        // PATCH method to soft-delete a stock
        [HttpPatch("soft-delete/{id:int}")]
        public async Task<IActionResult> SoftDeleteStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
                return NotFound(new { message = $"Stock with id {id} not found" });

            if (stock.IsDeleted)
                return BadRequest(new { message = "Stock is already soft-deleted" });

            stock.IsDeleted = true;
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Stock soft-deleted (marked as inactive)",
                stockId = stock.Id
            });
        }

        // PATCH method to restore a soft-deleted stock
        [HttpPatch("undo-soft-delete/{id:int}")]
        public async Task<IActionResult> UndoSoftDelete(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
                return NotFound(new { message = $"Stock with id {id} not found" });

            if (!stock.IsDeleted)
                return BadRequest(new { message = "Stock is not soft-deleted" });

            stock.IsDeleted = false;
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Stock restored successfully",
                restoredId = stock.Id
            });
        }

        // GET all stocks including soft-deleted ones
        [HttpGet("get-all-stocks")]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _context.Stocks
                .Select(s => s.ToDto())
                .ToListAsync();

            return Ok(stocks);
        }
    }
}
