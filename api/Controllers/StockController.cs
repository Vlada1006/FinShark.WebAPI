using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public StockController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _db.Stocks.ToListAsync();
            var stockDTO = stocks.Select(s => s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            var stock = await _db.Stocks.FirstOrDefaultAsync(u => u.StockId == id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDTO stockDTO)
        {
            var stockModel = stockDTO.ToStockFromCreateDto();

            await _db.Stocks.AddAsync(stockModel);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.StockId }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO)
        {
            var stockModel = await _db.Stocks.FirstOrDefaultAsync(u => u.StockId == id);
            if (stockModel == null)
            {
                return NotFound();
            }

            stockModel.Symbol = updateDTO.Symbol;
            stockModel.CompanyName = updateDTO.CompanyName;
            stockModel.Purchase = updateDTO.Purchase;
            stockModel.LastDiv = updateDTO.LastDiv;
            stockModel.Industry = updateDTO.Industry;
            stockModel.MarketCap = updateDTO.MarketCap;

            await _db.SaveChangesAsync();

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = await _db.Stocks.FirstOrDefaultAsync(u => u.StockId == id);
            if (stock == null)
            {
                return NotFound();
            }

            _db.Stocks.Remove(stock);
            await _db.SaveChangesAsync();

            return Ok(stock);
        }
    }
}