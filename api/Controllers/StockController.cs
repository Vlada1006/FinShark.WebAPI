using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllStocks()
        {
            var stocks = _db.Stocks.ToList()
            .Select(s => s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetStockById([FromRoute] int id)
        {
            var stock = _db.Stocks.FirstOrDefault(u => u.StockId == id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult CreateStock([FromBody] CreateStockRequestDTO stockDTO)
        {
            var stockModel = stockDTO.ToStockFromCreateDto();

            _db.Stocks.Add(stockModel);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.StockId }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO)
        {
            var stockModel = _db.Stocks.FirstOrDefault(u => u.StockId == id);
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

            _db.SaveChanges();

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteStock([FromRoute] int id)
        {
            var stock = _db.Stocks.FirstOrDefault(u => u.StockId == id);
            if (stock == null)
            {
                return NotFound();
            }

            _db.Stocks.Remove(stock);
            _db.SaveChanges();

            return Ok(stock);
        }
    }
}