using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _db;
        public PortfolioRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio)
        {
            await _db.Portfolios.AddAsync(portfolio);
            await _db.SaveChangesAsync();
            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _db.Portfolios.Where(u => u.AppUserId == user.Id)
            .Select(stock => new Stock
            {
                StockId = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap
            }).ToListAsync();
        }
    }
}