using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _db;
        public StockRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _db.Stocks.Include(u => u.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _db.Stocks.Include(u => u.Comments).FirstOrDefaultAsync(u => u.StockId == id);
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _db.Stocks.AddAsync(stockModel);
            await _db.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockDTO)
        {
            var existingStock = await _db.Stocks.FirstOrDefaultAsync(u => u.StockId == id);
            if (existingStock == null)
            {
                return null;
            }

            existingStock.Symbol = stockDTO.Symbol;
            existingStock.CompanyName = stockDTO.CompanyName;
            existingStock.Purchase = stockDTO.Purchase;
            existingStock.LastDiv = stockDTO.LastDiv;
            existingStock.Industry = stockDTO.Industry;
            existingStock.MarketCap = stockDTO.MarketCap;

            await _db.SaveChangesAsync();
            return existingStock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _db.Stocks.FirstOrDefaultAsync(u => u.StockId == id);
            if (stockModel == null)
            {
                return null;
            }

            _db.Stocks.Remove(stockModel);
            await _db.SaveChangesAsync();
            return stockModel;
        }
    }
}