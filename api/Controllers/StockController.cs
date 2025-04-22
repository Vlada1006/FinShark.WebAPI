using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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
            var stocks = _db.Stocks.ToList();
            return Ok(stocks);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetStockById(int id)
        {
            var stock = _db.Stocks.FirstOrDefault(u => u.StockId == id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }
    }
}