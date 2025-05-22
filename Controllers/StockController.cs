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

        //GET method to retrieve all stocks
        // This method will return a list of all stocks in the database
        [HttpGet("get")]
        public async Task<IActionResult> GetStocks()
        {
            var stocks = await _context.Stocks
                .Where(s => !s.IsDeleted) // 🧼 Show only active stocks
                .Select(s => s.ToDto())
                .ToListAsync();

            return Ok(stocks);
        }


        //POST method to create a new stock
        // This method will create a new stock object in the database
        [HttpPost("create")]
        public async Task<IActionResult> CreateStock([FromBody] StockCreateDto stockDto)
        {
            var stock = stockDto.ToModel(); // Convert DTO to Stock

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStocks), new { id = stock.Id }, new
            {
                message = "Stock created successfully",
                stock = stock.ToDto()
            });
        }

        //PUT method to update all fields
        // This method will replace the entire stock object with the new one
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] StockUpdateDto updateDto)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
                return NotFound(new { message = "Stock not found" });

            // Use mapper to apply updates
            stock.UpdateFromDto(updateDto);

            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Stock updated successfully",
                stock = stock.ToDto()
            });
        }

        //Patch method to update only the provided fields
        // This method will only update the fields that are provided in the request body
        [HttpPatch("patch/{id}")]
        public async Task<IActionResult> PatchStock(int id, [FromBody] StockUpdateDto patchDto)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
                return NotFound(new { message = $"Stock with id {id} not found" });

            // Only update provided fields
            stock.UpdateFromDto(patchDto);

            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Stock partially updated",
                stock = stock.ToDto()
            });
        }

        //DELETE method to delete a stock
        // This method will delete the stock object from the database
        [HttpDelete("delete/{id}")]
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

        // Soft delete method to mark a stock as deleted
        // This method will set the IsDeleted property to true
        [HttpPatch("soft-delete/{id}")]
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

        // Undo soft delete method to restore a soft-deleted stock
        // This method will set the IsDeleted property to false
        [HttpPatch("undo-soft-delete/{id}")]
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

        //GET all stocks including soft-deleted ones
        // This method will return a list of all stocks in the database, including soft-deleted ones
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _context.Stocks
                .Select(s => s.ToDto())
                .ToListAsync();

            return Ok(stocks);
        }
        
    }
}
